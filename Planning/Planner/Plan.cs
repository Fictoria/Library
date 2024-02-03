using System.Text;
using Fictoria.Domain.Utilities;
using Fictoria.Logic;

namespace Fictoria.Planning.Planner;

public class Plan
{
    public Plan(Program state)
    {
        Id = Identifier.Random();
        State = state;
        Steps = new List<Step>();
    }

    public Plan(Program state, Plan parent, Step step)
    {
        Id = Identifier.Random();
        State = state;
        Parent = parent;
        Steps = new List<Step>(parent.Steps) { step };
    }

    public string Id { get; }
    public Program State { get; }
    public Plan? Parent { get; }
    public List<Step> Steps { get; }

    public string RenderToDOT(IList<Plan> debug)
    {
        var output = new StringBuilder();
        output.AppendLine("digraph debug {");
        foreach (var p in debug)
        {
            var label = p.Parent is null ? "initial" : p.Steps.Last().ToString();
            var color = Contains(p) ? " style=filled fillcolor=green" : "";
            output.AppendLine($"  \"{p.Id}\" [label=\"{label}\"{color}];");
            if (p.Parent is not null)
            {
                output.AppendLine($"  \"{p.Parent.Id}\" -> \"{p.Id}\";");
            }
        }

        output.AppendLine("}");
        return output.ToString();
    }

    public bool Contains(Plan other)
    {
        if (this == other)
        {
            return true;
        }

        if (Parent is not null)
        {
            return Parent.Contains(other);
        }

        return false;
    }
}