
namespace Fountain;

public class World
{
    private Room[,] _rooms;
    private int _worldSize;
}

public class Room
{
    public Entity Entity { get; set; }
    public RoomTypes RoomType { get; set; }
}
