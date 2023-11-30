using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Fictoria.Planning.Semantic;

[Serializable]
public class Network
{
    public Dictionary<string, Dictionary<string, double>> Weights = new ();
    //TODO replace Pairs by iterating through Weights for DOT rendering
    public HashSet<string> Pairs = new();
    public HashSet<string> Terms = new();

    public static Network Load(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Network>(json);
    }

    public void Add(string first, string second, double weight)
    {
        if (!Weights.ContainsKey(first))
        {
            Weights[first] = new Dictionary<string, double>();
        }
        if (!Weights.ContainsKey(second))
        {
            Weights[second] = new Dictionary<string, double>();
        }

        Weights[first][second] = weight;
        Weights[second][first] = weight;
        Pairs.Add($"{first},{second}");
        Terms.Add(first);
        Terms.Add(second);
    }

    public bool TryGet(string first, string second, out double weight)
    {
        if (Weights.TryGetValue(first, out var found))
        {
            if (found.TryGetValue(second, out weight))
            {
                return true;
            }
        }

        weight = 0.0;
        return false;
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
            var tokens = pair.Split(",");
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