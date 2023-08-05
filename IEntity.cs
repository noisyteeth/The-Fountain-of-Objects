
namespace Fountain;

public interface IEntity
{
    public event Action? Death;

    public NewCoord Position { get; set; }
    public bool Dead { get; set; }

    public NewCoord MoveEntity(NewCoord position);
}
