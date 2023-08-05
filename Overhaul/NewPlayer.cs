
namespace Fountain;

public class NewPlayer : IEntity
{
    public event Action? Death;

    public NewCoord Position { get; set; }
    public bool Dead { get; set; }

    public NewPlayer(NewCoord position)
    {
        Position = position;
    }

    public NewPlayer()
    {
        Position = new(0, 0);
    }

    public void MoveEntity(NewCoord offset)
    {
        Position += offset;
    }
}
