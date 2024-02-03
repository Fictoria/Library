using Fictoria.Logic;
using Word2vec.Tools;
using Type = Fictoria.Logic.Type.Type;

namespace Fictoria.Planning.Semantic;

public class Analyzer
{
    public static Network Analyze(Program program)
    {
        var network = new Network();
        var terms = extractTerms(program);
        // TODO this should be part of a configuration
        var path = @"/Users/richardpianka/Downloads/GoogleNews-vectors-negative300.bin";
        var vocabulary = new Word2VecBinaryReader().Read(path);

        foreach (var first in terms)
        {
            foreach (var second in terms)
            {
                if (first == second)
                {
                    continue;
                }

                var alpha = vocabulary.GetRepresentationFor(first);
                var beta = vocabulary.GetRepresentationFor(second);
                var weight = alpha.GetCosineDistanceTo(beta).DistanceValue;
                if (weight > 0.10)
                {
                    network.Add(first, second, weight);
                }
            }
        }

        return network;
    }

    private static ISet<string> extractTerms(Program program)
    {
        var terms = new HashSet<string>();

        foreach (var t in program.Scope.Types.Values)
        {
            if (Type.BuiltIns.Contains(t))
            {
                continue;
            }

            terms.Add(t.Name);
        }

        foreach (var s in program.Scope.Schemata.Values)
        {
            terms.Add(s.Name);
        }

        foreach (var f in program.Scope.Functions.Values)
        {
            terms.Add(f.Name);
        }

        return terms;
    }
}