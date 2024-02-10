using Fictoria.Logic;
using Fictoria.Planning.Planner;
using Fictoria.Planning.Semantic;

namespace Fictoria.Tests.Planning;

public class PlanningTests
{
    private readonly string code = """
                                   // basics
                                   human: object.
                                   thing: object.
                                   resource: thing.
                                   material: thing.
                                   building: thing.
                                   fire: thing.

                                   instantiate(self, human).

                                   // some types
                                   tree: resource.
                                   quarry: resource.
                                   wood: material.
                                   stone: material.
                                   campfire: building.
                                   storage: building.


                                   // input/output
                                   provides(r: +resource, m: +material).
                                   consumes(b: +building, m: +material).
                                   contains(b: +building, m: -material).

                                   provides(tree, wood).
                                   provides(quarry, stone).
                                   consumes(campfire, wood).
                                   consumes(storage, wood).
                                   consumes(storage, stone).

                                   // etc.
                                   searchable(t: thing).
                                   location(t: -thing, x: int, y: int).
                                   carrying(t: -thing).
                                   warm(h: human).

                                   // the world
                                   searchable(tree).
                                   searchable(quarry).
                                   searchable(campfire).
                                   searchable(storage).

                                   instantiate(tree1, tree).
                                   instantiate(cf1, campfire).

                                   location(tree1, 5, 2).
                                   location(cf1, 2, 3).

                                   // general
                                   search(t: thing) -> {
                                       "cost": 1000,
                                       "terms": ["location", str(t)],
                                       "space":
                                           &searchable(@all); all,
                                       "conditions":
                                           !location(t, _, _),
                                       "locals":
                                           name = str(t) + "_" + id(),
                                       "effects":
                                           instantiate(name, t).
                                           location(name, 0, 0).
                                   }.

                                   deposit(b: building, m: material) -> {
                                       "cost": 1,
                                       "terms": [str(b), str(m)],
                                       "space":
                                           instances(building) * instances(material),
                                       "conditions":
                                           location(b, _, _) and
                                           consumes(b, m) and
                                           !contains(b, m) and
                                           carrying(m),
                                       "locals": _,
                                       "effects":
                                           contains(b, m).
                                           ~carrying(_).
                                   }.

                                   drop(t: thing) -> {
                                       "cost": 1,
                                       "terms": ["carrying", str(t)],
                                       "space":
                                           &carrying(@c); c,
                                       "conditions":
                                           true; carrying(t), // TODO just `carrying(t)` is ambiguous...probably need to tweak the grammar
                                       "locals": _,
                                       "effects":
                                           ~carrying(t).
                                            // thing should now be at current location
                                   }.

                                   // campfire
                                   light(c: campfire) -> {
                                       "cost": 2,
                                       "terms": ["campfire", "wood", "fire"],
                                       "space":
                                           true; instances(campfire),
                                       "conditions":
                                           location(c, _, _) and
                                           contains(c, wood),
                                       "locals":
                                           name = str(c) + "_" + id(),
                                       "effects":
                                           instantiate(name, fire).
                                           location(name, 0, 0).
                                           ~contains(name, wood).
                                           // TODO need to destroy the wood instance and facts
                                   }.

                                   // homeostasis
                                   warm(f: fire) -> {
                                       "cost": 1, // TODO distance
                                       "terms": ["fire"],
                                       "space":
                                           &instance(@all, fire); all,
                                       "conditions":
                                           !warm(self) and
                                           location(f, _, _),
                                       "locals": _,
                                       "effects":
                                           warm(self).
                                   }.

                                   // material
                                   gather(m: material) -> {
                                       "cost": 2, // TODO distance
                                       "terms": ["carrying", str(m)],
                                       "space":
                                           true; instances(material),
                                       "conditions":
                                           !carrying(_) and
                                           location(m, _, _),
                                       "locals": _,
                                       "effects":
                                           carrying(m).
                                           ~location(m, _, _).
                                   }.

                                   // extract
                                   extract(r: resource, m: material) -> {
                                       "cost": 20, // TODO distance
                                       "terms": [str(r), str(m)],
                                       "space":
                                           instances(resource) * subtypes(material),
                                       "conditions":
                                           provides(r, m) and
                                           location(r, _, _) and
                                           !location(m, _, _),
                                       "locals":
                                           name = str(m) + "_" + id(),
                                       "effects":
                                           instantiate(name, m).
                                           location(name, 0, 0).
                                   }.
                                   """;

    private readonly string semnet = """
                                     {"Weights":{"human":{"thing":0.13942695259622728,"resource":0.23983122459255124,"material":0.23103295340129643,"building":0.15239997364328542,"quarry":0.13328552370910407,"stone":0.14594590437935287,"instance":0.10366873175638493,"consumes":0.13267720194555663,"contains":0.11099592359375009,"carrying":0.13385447531403863},"thing":{"human":0.13942695259622728,"material":0.12960825635746062,"building":0.15309301988862162,"tree":0.12547846680317531,"campfire":0.10062362311432277,"instance":0.29950491378393296,"consumes":0.10269792260152473,"location":0.199034193225027},"resource":{"human":0.23983122459255124,"material":0.23541427416613467,"building":0.1531259211626136,"tree":0.1001798970131417,"quarry":0.22721721802269002,"wood":0.14169262509890365,"stone":0.16852199293698475,"storage":0.2130631194075026,"provides":0.2801529084742665,"consumes":0.15944411756549895,"contains":0.20226916311742316,"searchable":0.22121402761657355,"location":0.20469780987474365},"material":{"human":0.23103295340129643,"thing":0.12960825635746062,"resource":0.23541427416613467,"building":0.1874325560192054,"quarry":0.22344335474452381,"wood":0.2402413656591572,"stone":0.17188605437656782,"campfire":0.11311334337149137,"storage":0.19338050107981217,"instance":0.1351290945320312,"provides":0.10819558557871235,"contains":0.21857651488272184,"searchable":0.18131863687423216,"location":0.1293981343235317},"building":{"human":0.15239997364328542,"thing":0.15309301988862162,"resource":0.1531259211626136,"material":0.1874325560192054,"fire":0.2905071991352231,"tree":0.18649735178338145,"quarry":0.23506486802538903,"wood":0.26413805460984235,"stone":0.28801033418394073,"campfire":0.10609172173525666,"storage":0.21272712276350833,"location":0.2936233566539749,"carrying":0.12070953220250792},"quarry":{"human":0.13328552370910407,"resource":0.22721721802269002,"material":0.22344335474452381,"building":0.23506486802538903,"tree":0.1946351811194152,"wood":0.23815571747412556,"stone":0.3283219040097645,"campfire":0.1755156151052877,"location":0.26951949573104356},"stone":{"human":0.14594590437935287,"resource":0.16852199293698475,"material":0.17188605437656782,"building":0.28801033418394073,"tree":0.29699816311373606,"quarry":0.3283219040097645,"wood":0.4231521736940031,"campfire":0.10442391057620028,"instance":0.10556408013832082,"contains":0.1472395129411664,"location":0.10642577667435668},"instance":{"human":0.10366873175638493,"thing":0.29950491378393296,"material":0.1351290945320312,"stone":0.10556408013832082,"storage":0.1406062218584074,"consumes":0.10095109255536677,"contains":0.11108337527246523,"location":0.17538782426298768},"consumes":{"human":0.13267720194555663,"thing":0.10269792260152473,"resource":0.15944411756549895,"wood":0.156971407459935,"storage":0.21441208079477797,"instance":0.10095109255536677,"provides":0.3461223507206382,"contains":0.3903931274518851,"searchable":0.10046023066707326,"carrying":0.14180401316022695},"contains":{"human":0.11099592359375009,"resource":0.20226916311742316,"material":0.21857651488272184,"stone":0.1472395129411664,"storage":0.2069880914038735,"instance":0.11108337527246523,"provides":0.5754173470124677,"consumes":0.3903931274518851,"searchable":0.24906819954989695,"carrying":0.18672067774085382},"carrying":{"human":0.13385447531403863,"building":0.12070953220250792,"fire":0.11530416383730807,"storage":0.11492546031096951,"consumes":0.14180401316022695,"contains":0.18672067774085382},"tree":{"thing":0.12547846680317531,"resource":0.1001798970131417,"building":0.18649735178338145,"fire":0.21172848209922024,"quarry":0.1946351811194152,"wood":0.3803801661501389,"stone":0.29699816311373606,"campfire":0.28786069646804124,"location":0.14738195885384492},"campfire":{"thing":0.10062362311432277,"material":0.11311334337149137,"building":0.10609172173525666,"fire":0.35441413044780934,"tree":0.28786069646804124,"quarry":0.1755156151052877,"wood":0.26265662944195095,"stone":0.10442391057620028,"storage":0.1250138347187138,"warm":0.22977454031258207},"location":{"thing":0.199034193225027,"resource":0.20469780987474365,"material":0.1293981343235317,"building":0.2936233566539749,"fire":0.13844857052107268,"tree":0.14738195885384492,"quarry":0.26951949573104356,"stone":0.10642577667435668,"storage":0.24926983848857043,"instance":0.17538782426298768,"provides":0.16242193667390084,"searchable":0.1264977067739336},"wood":{"resource":0.14169262509890365,"material":0.2402413656591572,"building":0.26413805460984235,"fire":0.17866669906451915,"tree":0.3803801661501389,"quarry":0.23815571747412556,"stone":0.4231521736940031,"campfire":0.26265662944195095,"storage":0.17841272156165028,"consumes":0.156971407459935,"warm":0.11180452417780744},"storage":{"resource":0.2130631194075026,"material":0.19338050107981217,"building":0.21272712276350833,"fire":0.14693410776303917,"wood":0.17841272156165028,"campfire":0.1250138347187138,"instance":0.1406062218584074,"provides":0.165775650598454,"consumes":0.21441208079477797,"contains":0.2069880914038735,"searchable":0.16724773616209354,"location":0.24926983848857043,"carrying":0.11492546031096951,"warm":0.16076613566800488},"provides":{"resource":0.2801529084742665,"material":0.10819558557871235,"storage":0.165775650598454,"consumes":0.3461223507206382,"contains":0.5754173470124677,"searchable":0.26569594580738376,"location":0.16242193667390084},"searchable":{"resource":0.22121402761657355,"material":0.18131863687423216,"storage":0.16724773616209354,"provides":0.26569594580738376,"consumes":0.10046023066707326,"contains":0.24906819954989695,"location":0.1264977067739336},"fire":{"building":0.2905071991352231,"tree":0.21172848209922024,"wood":0.17866669906451915,"campfire":0.35441413044780934,"storage":0.14693410776303917,"location":0.13844857052107268,"carrying":0.11530416383730807,"warm":0.15696363197914584},"warm":{"fire":0.15696363197914584,"wood":0.11180452417780744,"campfire":0.22977454031258207,"storage":0.16076613566800488}},"Pairs":["human,thing","human,resource","human,material","human,building","human,quarry","human,stone","human,instance","human,consumes","human,contains","human,carrying","thing,human","thing,material","thing,building","thing,tree","thing,campfire","thing,instance","thing,consumes","thing,location","resource,human","resource,material","resource,building","resource,tree","resource,quarry","resource,wood","resource,stone","resource,storage","resource,provides","resource,consumes","resource,contains","resource,searchable","resource,location","material,human","material,thing","material,resource","material,building","material,quarry","material,wood","material,stone","material,campfire","material,storage","material,instance","material,provides","material,contains","material,searchable","material,location","building,human","building,thing","building,resource","building,material","building,fire","building,tree","building,quarry","building,wood","building,stone","building,campfire","building,storage","building,location","building,carrying","fire,building","fire,tree","fire,wood","fire,campfire","fire,storage","fire,location","fire,carrying","fire,warm","tree,thing","tree,resource","tree,building","tree,fire","tree,quarry","tree,wood","tree,stone","tree,campfire","tree,location","quarry,human","quarry,resource","quarry,material","quarry,building","quarry,tree","quarry,wood","quarry,stone","quarry,campfire","quarry,location","wood,resource","wood,material","wood,building","wood,fire","wood,tree","wood,quarry","wood,stone","wood,campfire","wood,storage","wood,consumes","wood,warm","stone,human","stone,resource","stone,material","stone,building","stone,tree","stone,quarry","stone,wood","stone,campfire","stone,instance","stone,contains","stone,location","campfire,thing","campfire,material","campfire,building","campfire,fire","campfire,tree","campfire,quarry","campfire,wood","campfire,stone","campfire,storage","campfire,warm","storage,resource","storage,material","storage,building","storage,fire","storage,wood","storage,campfire","storage,instance","storage,provides","storage,consumes","storage,contains","storage,searchable","storage,location","storage,carrying","storage,warm","instance,human","instance,thing","instance,material","instance,stone","instance,storage","instance,consumes","instance,contains","instance,location","provides,resource","provides,material","provides,storage","provides,consumes","provides,contains","provides,searchable","provides,location","consumes,human","consumes,thing","consumes,resource","consumes,wood","consumes,storage","consumes,instance","consumes,provides","consumes,contains","consumes,searchable","consumes,carrying","contains,human","contains,resource","contains,material","contains,stone","contains,storage","contains,instance","contains,provides","contains,consumes","contains,searchable","contains,carrying","searchable,resource","searchable,material","searchable,storage","searchable,provides","searchable,consumes","searchable,contains","searchable,location","location,thing","location,resource","location,material","location,building","location,fire","location,tree","location,quarry","location,stone","location,storage","location,instance","location,provides","location,searchable","carrying,human","carrying,building","carrying,fire","carrying,storage","carrying,consumes","carrying,contains","warm,fire","warm,wood","warm,campfire","warm,storage"],"Terms":["human","thing","resource","material","building","quarry","stone","instance","consumes","contains","carrying","tree","campfire","location","wood","storage","provides","searchable","fire","warm"]}
                                     """;

    [Test]
    public void DontBreak()
    {
        var program = Loader.Load(code);
        var planner = new Planner(program);
        var network = Network.LoadFromString(semnet);
        if (planner.ForwardSearch(program, network, "warm(self)", out var plan, out var debug))
        {
            Assert.That(plan.Steps, Has.Count.EqualTo(5));
            Assert.That(plan.Steps[0].Action.Name, Is.EqualTo("extract"));
            Assert.That(plan.Steps[1].Action.Name, Is.EqualTo("gather"));
            Assert.That(plan.Steps[2].Action.Name, Is.EqualTo("deposit"));
            Assert.That(plan.Steps[3].Action.Name, Is.EqualTo("light"));
            Assert.That(plan.Steps[4].Action.Name, Is.EqualTo("warm"));
            return;
        }

        Assert.True(false);
    }
}