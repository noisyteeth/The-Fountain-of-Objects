
namespace Fountain;

public class World
{
    private RoomTypes[,] _roomMap;
    private Entity[,] _entityMap;
    private int _worldSize;

    public World(int worldSize)
    {
        _worldSize = worldSize;

        _roomMap = _worldSize switch
        {
            6 => new RoomTypes[6, 6],
            8 => new RoomTypes[8, 8],
            _ => new RoomTypes[4, 4]
        };

        _entityMap = _worldSize switch
        {
            6 => new Entity[6, 6],
            8 => new Entity[8, 8],
            _ => new Entity[4, 4]
        };
    }

    public void LoadMap()
    {
        for (int y = 0; y < _worldSize; y++)
        {
            for (int x = 0; x < _worldSize; x++)
            {
                if (x == 0 && y == 0)
                    _roomMap[0, 0] = RoomTypes.Entrance;
            }
        }
    }
}
