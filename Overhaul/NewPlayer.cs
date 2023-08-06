
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
