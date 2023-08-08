
namespace Fountain;

public class World
{
    private RoomTypes[,] _roomMap;
    private Entity[,] _entityMap;
    private int _worldSize;
    private NewPlayer _player;
    
    private int _playerLocX;
    private int _playerLocY;


    public RoomTypes[,] RoomMap => _roomMap;
    public Entity[,] EntityMap => _entityMap;
    public NewPlayer Player => _player;

    public World(int worldSize, NewPlayer player)
    {
        player.EntityMoved += OnEntityMovement;

        _worldSize = worldSize;
        _player = player;
        _playerLocX = 0;
        _playerLocY = 0;

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

        _roomMap[0, 0] = RoomTypes.Entrance;
        _entityMap[0, 0] = _player;

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
                    _entityMap[x, y] = new Empty(new NewCoord(x, y));
                }
            }
        }
    }

    public void OnEntityMovement()
    {
        UpdateEntityMap();
    }

    public void UpdateEntityMap()
    {
        int x = _player.Position.X;
        int y = _player.Position.Y;

        if (y - 1 < 0 )
        {
            _player.Position.Y = 0;
            y = _player.Position.Y;
        }
        else if (y > _worldSize)
        {
            _player.Position.Y = _worldSize - 1;
            y = _player.Position.Y;
        }

        if (x - 1 < 0)
        {
            _player.Position.X = 0;
            x = _player.Position.X;
        }
        else if (x > _worldSize)
        {
            _player.Position.X = _worldSize - 1;
            x = _player.Position.X;
        }

        _entityMap[_playerLocX, _playerLocY] = new Empty(_playerLocX, _playerLocY);

        _playerLocX = x;
        _playerLocY = y;

        _entityMap[x, y] = _player;
    }
}
