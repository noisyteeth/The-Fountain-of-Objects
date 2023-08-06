
namespace Fountain;

public abstract class Entity
{
    public event Action? Death;

    private NewCoord _position;
    private bool _dead;

    public abstract void MoveEntity(NewCoord offset);
    public abstract void MoveEntity(Direction dir);
}
