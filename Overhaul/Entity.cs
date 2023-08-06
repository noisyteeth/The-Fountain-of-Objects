
namespace Fountain;

public abstract class Entity
{
    //public event Action? Death;

    private NewCoord _position;
    private bool _dead;

    // TODO:
    // Replace MoveEntity() with:
    // public abstract void MoveEntity(NewCoord coord); (for teleporting to a new location)
    // public abstract void MoveEntity(params NewCoord[] steps);
    // public abstract void MoveEntity(params Direction[] steps);

    public abstract void MoveEntity(NewCoord offset);
    public abstract void MoveEntity(Direction dir);
}

public class Empty : Entity
{
    private NewCoord _position;
    private bool _dead;

    public Empty(NewCoord position)
    {
        _position = position;
    }

    public override void MoveEntity(NewCoord offset)
    {
        _position += offset;
    }

    public override void MoveEntity(Direction dir)
    {
        _position += dir;
    }
}
