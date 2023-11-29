using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Fictoria.Planning.Semantic;

[Serializable]
public class Network
{
    public Dictionary<string, double> Weights = new();
    public HashSet<string> Pairs = new();
    public HashSet<string> Terms = new();

    public static Network Load(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Network>(json);
    }

    public void Add(string first, string second, double weight)
    {
        var obverse = $"{first}<->{second}";
        var reverse = $"{second}<->{first}";
        Weights[obverse] = weight;
        Weights[reverse] = weight;
        Pairs.Add(obverse);
        Terms.Add(first);
        Terms.Add(second);
    }

    public bool TryGet(string first, string second, out double weight)
    {
        return Weights.TryGetValue($"{first}<->{second}", out weight);
    }

    public string RenderToDOT()
    {
        var output = new StringBuilder();
        output.AppendLine("graph semnet {");
        foreach (var t in Terms)
        {
            output.AppendLine($"  {t} [label={t}];");
        }
        foreach (var pair in Pairs)
        {
            var tokens = pair.Split("<->");
            var first = tokens[0];
            var second = tokens[1];
            TryGet(first, second, out var weight);
            var width = Math.Round(weight * 10.0);
            var renderWeight = weight.ToString(CultureInfo.InvariantCulture)[..4];
            output.AppendLine($"  {first} -- {second} [label={renderWeight} penwidth={width}];");
        }
        output.AppendLine("}");
        return output.ToString();
    }

    public void Save(string path)
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, json);
    }
}