
namespace Fountain;

public abstract class Entity
{
    //public event Action? Death;
    public abstract event Action? EntityMoved;

    private NewCoord _position;
    private bool _dead;

    public abstract void Teleport(int x, int y);
    public abstract void MoveByOffset(int x, int y);
    public abstract void MoveByOffset(NewCoord coord);
    public abstract void MoveByDir(params Direction[] steps);
}

public class Empty : Entity
{
    public override event Action? EntityMoved;

    private NewCoord _position;
    private bool _dead;

    public Empty(NewCoord position)
    {
        _position = position;
    }

    public Empty(int x, int y)
    {
        _position = new NewCoord(x, y);
    }

    public override void Teleport(int x, int y)
    {
        _position.X = x;
        _position.Y = y;
        EntityMoved();
    }

    public override void MoveByOffset(int x, int y)
    {
        _position.X += x;
        _position.Y += y;
        
        EntityMoved();
    }

    public override void MoveByOffset(NewCoord coord)
    {
        _position += coord;

        EntityMoved();
    }

    public override void MoveByDir(params Direction[] steps)
    {
        foreach (Direction step in steps)
        {
            _position += step;
        }
        
        EntityMoved();
    }
}
