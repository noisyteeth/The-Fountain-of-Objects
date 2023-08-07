
namespace Fountain;

public abstract class Entity
{
    //public event Action? Death;
    public abstract event Action? EntityMoved;

    private NewCoord _position;
    private bool _dead;

    public abstract void TeleportEntity(NewCoord coord);
    public abstract void MoveByOffset(params NewCoord[] steps);
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

    public override void TeleportEntity(NewCoord coord)
    {
        _position = coord;
        EntityMoved();
    }

    public override void MoveByOffset(params NewCoord[] steps)
    {
        foreach (NewCoord step in steps)
        {
            _position += step;
        }
        
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
