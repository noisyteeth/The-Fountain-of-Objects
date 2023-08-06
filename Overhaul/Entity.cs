
namespace Fountain;

public abstract class Entity
{
    //public event Action? Death;

    private NewCoord _position;
    private bool _dead;

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
