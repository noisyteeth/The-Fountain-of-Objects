
namespace Fountain;

public interface IEntity
{
    public Coord Position { get; set; }
    public bool Dead { get; set; }
}
