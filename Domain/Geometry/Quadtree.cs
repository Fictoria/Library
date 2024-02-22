namespace Fictoria.Domain.Geometry;

// adapted from https://github.com/BadEcho/core/blob/master/src/Game/Quadtree.cs
public class Quadtree
{
    private readonly Rectangle _bounds;
    private readonly int _capacity;
    private readonly int _depth;
    private readonly int _level;
    private Dictionary<string, QuadtreeEntry> _entries = new();
    private Quadtree? _topLeft, _topRight, _bottomLeft, _bottomRight;

    public Quadtree(Rectangle bounds, int capacity, int depth)
    {
        _bounds = bounds;
        _capacity = capacity;
        _depth = depth;
        _level = 0;
    }

    public void Insert(QuadtreeEntry entry)
    {
        Insert(entry.Id, entry.Point);
    }

    public void Insert(string id, Point point)
    {
        if (!_bounds.Contains(point))
        {
            return;
        }

        if (_entries.Count >= _capacity)
        {
            split();
        }

        if (selectChild(point, out var child))
        {
            child?.Insert(id, point);
        }
        else
        {
            _entries[id] = new QuadtreeEntry(id, point);
        }
    }

    public bool Remove(string id)
    {
        if (_entries.Remove(id))
        {
            if (count() <= _capacity)
            {
                combine();
            }
            return true;
        }

        if (isLeaf())
        {
            return false;
        }

        // TODO this isn't ideal
        return _topLeft?.Remove(id) ??
               _topRight?.Remove(id) ??
               _bottomLeft?.Remove(id) ??
               _bottomRight?.Remove(id) ?? false;
    }

    public void Update(string id, Point point)
    {
        Remove(id);
        Insert(id, point);
    }

    private int count()
    {
        if (isLeaf())
        {
            return _entries.Count;
        }
        return _topLeft?.count() ?? 0 +
            _topRight?.count() ?? 0 +
            _bottomLeft?.count() ?? 0 +
            _bottomRight?.count() ?? 0;
    }

    private bool isLeaf()
    {
        return _topLeft is null || _topRight is null || _bottomLeft is null || _bottomRight is null;
    }

    private void split()
    {
        if (!isLeaf() || _level + 1 > _depth)
        {
            return;
        }

        _topLeft = new Quadtree(
            new Rectangle(_bounds.Origin, _bounds.Width / 2, _bounds.Height / 2),
            _capacity,
            _depth);
        _topRight = new Quadtree(
            new Rectangle(new Point(_bounds.Center.X, _bounds.Origin.Y), _bounds.Width / 2, _bounds.Height / 2),
            _capacity,
            _depth);
        _bottomLeft = new Quadtree(
            new Rectangle(new Point(_bounds.Origin.X, _bounds.Center.Y), _bounds.Width / 2, _bounds.Height / 2),
            _capacity,
            _depth);
        _bottomRight = new Quadtree(
            new Rectangle(_bounds.Center, _bounds.Width / 2, _bounds.Height / 2),
            _capacity,
            _depth);

        foreach (var entry in _entries.Values)
        {
            Remove(entry.Id);
            Insert(entry);
        }
    }

    private void combine()
    {
        if (isLeaf())
        {
            return;
        }

        _entries = _entries
            .Concat(_topLeft!._entries)
            .Concat(_topRight!._entries)
            .Concat(_bottomLeft!._entries)
            .Concat(_bottomRight!._entries)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        _topLeft = _topRight = _bottomLeft = _bottomRight = null;
    }

    private bool selectChild(Point point, out Quadtree? child)
    {
        if (isLeaf())
        {
            child = null;
            return false;
        }

        if (_topLeft?._bounds.Contains(point) ?? false)
        {
            child = _topLeft;
            return true;
        }

        if (_topRight?._bounds.Contains(point) ?? false)
        {
            child = _topRight;
            return true;
        }

        if (_bottomLeft?._bounds.Contains(point) ?? false)
        {
            child = _bottomLeft;
            return true;
        }

        if (_bottomRight?._bounds.Contains(point) ?? false)
        {
            child = _bottomRight;
            return true;
        }

        child = null;
        return false;
    }
}