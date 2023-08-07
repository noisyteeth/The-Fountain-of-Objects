
namespace Fountain;

public class World
{
    private RoomTypes[,] _roomMap;
    private Entity[,] _entityMap;
    private int _worldSize;

    public RoomTypes[,] RoomMap => _roomMap;
    public Entity[,] EntityMap => _entityMap;

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
        _roomMap[0, 0] = RoomTypes.Entrance;
        _entityMap[0, 0] = new NewPlayer();

        if (_worldSize == 4)
        {
            _roomMap[2, 0] = RoomTypes.FountainRoom;

            _roomMap[1, 0] = RoomTypes.Pit;
        }

        else if (_worldSize == 6)
        {
            _roomMap[3, 0] = RoomTypes.FountainRoom;

            _roomMap[2, 0] = RoomTypes.Pit;
            _roomMap[3, 1] = RoomTypes.Pit;
        }

        else if (_worldSize == 8)
        {
            _roomMap[4, 0] = RoomTypes.FountainRoom;

            _roomMap[2, 0] = RoomTypes.Pit;
            _roomMap[3, 1] = RoomTypes.Pit;
            _roomMap[4, 2] = RoomTypes.Pit;
            _roomMap[5, 3] = RoomTypes.Pit;
        }

        for (int x = 0; x < _worldSize; x++)
        {
            for (int y = 0; y < _worldSize; y++)
            {
                if (_entityMap[x, y] == null)
                {
                    _entityMap[x, y] = new Empty(new NewCoord(y, x));
                }
            }
        }
    }
}
