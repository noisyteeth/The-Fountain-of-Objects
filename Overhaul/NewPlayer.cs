
namespace Fountain;

public class NewPlayer : IEntity
{
    public event Action? Death;

    public NewCoord Position { get; set; }
    public bool Dead { get; set; }

    public void MoveEntity(NewCoord offset)
    {
        Position += offset;
    }
}
