
namespace Fountain;

public class NewPlayer : Entity
{
    //public event Action? Death;

    private NewCoord _position;
    private bool _dead;

    public NewCoord Position => _position;
    public bool Dead => _dead;

    public NewPlayer()
    {
        _position = new(0, 0);
        _dead = false;
    }

    public NewPlayer(NewCoord position)
    {
        _position = position;
        _dead = false;
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
