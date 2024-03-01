using Fictoria.Domain.Locality;
using RBush;

namespace Fictoria.Logic.Index;

public class SpatialIndex
{
    private readonly Dictionary<string, SpatialEntry> _cache = new();
    private readonly RBush<SpatialEntry> _tree = new();
    public string IdField { get; }
    public string XField { get; }
    public string YField { get; }

    public SpatialIndex(string idField, string xField, string yField)
    {
        IdField = idField;
        XField = xField;
        YField = yField;
    }

    public SpatialIndex(SpatialIndex other)
    {
        IdField = other.IdField;
        XField = other.XField;
        YField = other.YField;
        _tree.BulkLoad(_cache.Values);
    }

    public void Insert(string id, Point point)
    {
        if (_cache.ContainsKey(id))
        {
            // TODO throw something reasonable
            throw new Exception("");
        }
        var entry = new SpatialEntry(id, point);
        _cache[entry.Id] = entry;
        _tree.Insert(entry);
    }

    public void Remove(string id)
    {
        if (_cache.TryGetValue(id, out var entry))
        {
            _cache.Remove(entry.Id);
            _tree.Delete(entry);
        }
        else
        {
            // TODO throw something reasonable
            throw new Exception("");
        }
    }

    public void Update(string id, Point point)
    {
        Remove(id);
        Insert(id, point);
    }
}