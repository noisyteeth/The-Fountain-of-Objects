
namespace Fountain;

public class Map
{
    private RoomTypes[,] _rooms;    // represents the rooms of the world
    private bool[,] _playerLoc;     // represents the player's location within the world
    private int _gridSize;          // represents the size of the world
    private RoomTypes _playerRoom;  // represents the room that the player is currently in

    public RoomTypes[,] Rooms => _rooms;
    public bool[,] PlayerLoc => _playerLoc;
    public int GridSize => _gridSize;
    public RoomTypes PlayerRoom => _playerRoom;

    public Map(int size)
    {
        _gridSize = size;
        _playerRoom = RoomTypes.Entrance;

        _rooms = size switch
        {
            6 => new RoomTypes[6, 6],
            8 => new RoomTypes[8, 8],
            _ => new RoomTypes[4, 4]
        };

        _playerLoc = size switch
        {
            6 => new bool[6, 6],
            8 => new bool[8, 8],
            _ => new bool[4, 4]
        };
    }

    public void LoadMap()
    {
        _rooms[0, 0] = RoomTypes.Entrance;
        _playerLoc[0, 0] = true;

        if (_gridSize == 4)
        {
            _rooms[2, 0] = RoomTypes.FountainRoom;

            _rooms[1, 0] = RoomTypes.Pit;
        }

        else if (_gridSize == 6)
        {
            _rooms[3, 0] = RoomTypes.FountainRoom;

            _rooms[2, 0] = RoomTypes.Pit; 
            _rooms[3, 1] = RoomTypes.Pit;
        }

        else if (_gridSize == 8)
        {
            _rooms[4, 0] = RoomTypes.FountainRoom;

            _rooms[2, 0] = RoomTypes.Pit;
            _rooms[3, 1] = RoomTypes.Pit;
            _rooms[4, 2] = RoomTypes.Pit;
            _rooms[5, 3] = RoomTypes.Pit; 
        }
    }

    public void UpdateLocationMap(Player player)
    {
        int xPos = player.PlayerCoord.X;
        int yPos = player.PlayerCoord.Y;

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                if (y == yPos && x == xPos)
                {
                    _playerLoc[x, y] = true;
                    _playerRoom = _rooms[x, y];
                }
                else _playerLoc[x, y] = false;
            }
        }
    }
} 
