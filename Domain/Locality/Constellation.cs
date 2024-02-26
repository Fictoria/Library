using RBush;

namespace Fictoria.Domain.Locality;

// TODO everything about Location equality in RBush gives me the heebie jeebies
public class Constellation
{
    private readonly Dictionary<string, Location> _cache = new();
    private readonly RBush<Location> _tree = new();

    public int Count => _tree.Count;

    public void Insert(string id, double x, double y)
    {
        if (_cache.ContainsKey(id))
        {
            // TODO throw something reasonable
            throw new Exception("");
        }
        var location = new Location(id, x, y);
        Insert(location);
    }

    public void Insert(Location location)
    {
        _cache[location.Id] = location;
        _tree.Insert(location);
    }

    public void Remove(string id)
    {
        if (_cache.TryGetValue(id, out var location))
        {
            Remove(location);
        }
        else
        {
            // TODO throw something reasonable
            throw new Exception("");
        }
    }

    public void Remove(Location location)
    {
        _cache.Remove(location.Id);
        _tree.Delete(location);
    }

    public void Update(string id, double x, double y)
    {
        var location = new Location(id, x, y);
        Update(location);
    }

    public void Update(Location location)
    {
        Remove(location.Id);
        Insert(location);
    }

    public IReadOnlyList<Location> Search(double x, double y, double distance)
    {
        var half = distance / 2.0;
        var space = new Envelope(x - half, y - half, x + half, y + half);
        return _tree.Search(space);
    }
}