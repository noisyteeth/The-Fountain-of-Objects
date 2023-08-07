
namespace Fountain;

public class NewPlayer : Entity
{
    //public event Action? Death;
    public override event Action? EntityMoved;

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

    public NewPlayer(int x, int y)
    {
        _position = new NewCoord(x, y);
        _dead = false;
    }

    public override void TeleportEntity(int x, int y)
    {
        _position = new NewCoord(x, y);
        EntityMoved();
    }

    public override void MoveByOffset(int x, int y)
    {
        _position.X += x;
        _position.Y += y;

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
