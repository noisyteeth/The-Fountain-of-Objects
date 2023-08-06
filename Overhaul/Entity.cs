
namespace Fountain;

public abstract class Entity
{
    //public event Action? Death;
    public abstract event Action? EntityMoved;

    private NewCoord _position;
    private bool _dead;

    public abstract void MoveEntity(NewCoord coord);
    public abstract void MoveEntity(params NewCoord[] steps);
    public abstract void MoveEntity(params Direction[] steps);
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

    public override void MoveEntity(NewCoord coord)
    {
        _position = coord;
    }

    public override void MoveEntity(params NewCoord[] steps)
    {
        foreach (NewCoord step in steps)
        {
            _position += step;
        }
    }

    public override void MoveEntity(params Direction[] steps)
    {
        foreach (Direction step in steps)
        {
            _position += step;
        }
    }
}
