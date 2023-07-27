
Console.WriteLine($"Welcome to the Fountain of Objects.");
Console.WriteLine($"Choose a world size between small, medium, and large.");

string? input;
int worldSize;

while (true)
{
    input = Console.ReadLine();

    if (input == "small")
    {
        worldSize = 4;
        break;
    }
    else if (input == "medium")
    {
        worldSize = 6;
        break;
    }
    else if (input == "large")
    {
        worldSize = 8;
        break;
    }
    else
    {
        Console.WriteLine("Invalid input.");
        continue;
    }
}

Map map = new Map(worldSize);
Player player = new Player();
Game game = new(map, player);
map.LoadMap();

Console.Clear();

DateTime start = DateTime.Now;

while (true)
{
    game.CheckWin();
    player.Death(map);
    game.CheckAdjacentPit();
    game.Prompts();

    if (game.Win)
    {
        TimeSpan timeSpent = DateTime.Now - start;
        Console.WriteLine($"Time spent in the cavern: {timeSpent.Minutes}m {timeSpent.Seconds}s");
        Console.ReadKey();
        break;
    }

    if (player.Dead)
    {
        Console.ReadKey();
        break;
    }

    game.ShowRoomsStatus();

    string? command = Console.ReadLine();

    switch (command)
    {
        case "move north":
            player.MovePlayer(Direction.North, map);
            break;
        case "move south":
            player.MovePlayer(Direction.South, map);
            break;
        case "move east":
            player.MovePlayer(Direction.East, map);
            break;
        case "move west":
            player.MovePlayer(Direction.West, map);
            break;
        case "enable fountain":
            game.EnableFountain();
            break;
    }

    map.UpdateLocationMap(player);

    Console.Clear();
}

public class Player
{
    private bool _dead;
    private Coord _playerCoord;

    public bool Dead => _dead;
    public Coord PlayerCoord => _playerCoord;
    public string PlayerCoordString => $"({PlayerCoord.X}, {PlayerCoord.Y})";

    public Player()
    {
        _dead = false;
        _playerCoord = new(0, 0);
    }

    public void MovePlayer(Direction direction, Map map)
    {
        switch (direction)
        {
            case Direction.North:
                if (PlayerCoord.Y - 1 < 0) return;
                PlayerCoord.Y--;
                break;
            case Direction.South:
                if (PlayerCoord.Y + 1 > map.GridSize - 1) return;
                PlayerCoord.Y++;
                break;
            case Direction.East:
                if (PlayerCoord.X + 1 > map.GridSize - 1) return;
                PlayerCoord.X++;
                break;
            case Direction.West:
                if (PlayerCoord.X - 1 < 0) return;
                PlayerCoord.X--;
                break;
        }
    }

    public void ShowPlayerCoord()
    {
        Console.WriteLine(PlayerCoordString);
    }

    public void Death(Map map)
    {
        if (map.PlayerRoom == RoomTypes.Pit) _dead = true;
    }
}

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

public class Game
{
    private bool _win;
    private bool _fountainEnabled;
    private bool _pitIsNear;
    private Map _map;
    private Player _player;

    public bool Win => _win;
    public bool FountainEnabled => _fountainEnabled;
    public bool PitIsNear => _pitIsNear;

    public Game(Map map, Player player)
    {
        _map = map;
        _player = player;
        _win = false;
        _fountainEnabled = false;
    }

    public void CheckAdjacentPit()
    {
        for (int x = 0; x < _map.GridSize; x++)
        {
            for (int y = 0; y < _map.GridSize; y++)
            {
                if (Coord.IsAdjacent(_player.PlayerCoord, x, y))
                {
                    if (_map.Rooms[x, y] == RoomTypes.Pit)
                    {
                        if (Coord.SameCoord(_player.PlayerCoord, x, y))
                        {
                            _pitIsNear = false;
                            return;
                        }

                        _pitIsNear = true;
                        return;
                    }
                }
            }
        }

        _pitIsNear = false;
    }

    public void EnableFountain()
    {
        if (_map.PlayerRoom == RoomTypes.FountainRoom && !_fountainEnabled) 
            _fountainEnabled = true;
    } 

    public void Prompts()
    {
        if (_map.PlayerRoom == RoomTypes.Entrance && !_win)
            Console.WriteLine("You see light coming from the cavern entrance.");

        if (_map.PlayerRoom == RoomTypes.FountainRoom)
        {
            if (_fountainEnabled)
                Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
            else
                Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
        }

        if (_pitIsNear && !_win && !_player.Dead)
            Console.WriteLine("You feel a draft. There is a pit in a nearby room.");

        if (_player.Dead)
            Console.WriteLine("You have fallen into a pit and DIED.");

        if (_win)
        {
            Console.WriteLine("You have enabled the Fountain of Objects and escaped with your life.");
            Console.WriteLine("You win!");
        }
    }

    public void ShowRoomsStatus()
    {
        Console.WriteLine();
        Console.Write($"Current Room: {_map.PlayerRoom}");

        for (int y = 0; y < _map.GridSize; y++)
        {

            Console.WriteLine();

            for (int x = 0; x < _map.GridSize; x++)
            {
                if (_map.PlayerLoc[x, y] == true) Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(String.Format("({0},{1})", x, y));
                Console.ResetColor();
                Console.Write(" | ");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Win = {_win}");
        Console.WriteLine($"Fountain Enabled = {_fountainEnabled}");
        Console.WriteLine($"Pit Is Near = {_pitIsNear}");
    }

    public void CheckWin()
    {
        if (_map.PlayerRoom == RoomTypes.Entrance && _fountainEnabled)
            _win = true;
    }
}

public class Coord
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coord(int row, int column)
    {
        X = row;
        Y = column;
    }

    public Coord()
    {
        X = 0;
        Y = 0;
    }

    public static bool IsAdjacent(Coord first, Coord second)
    {
        int rowDifference = Math.Abs(first.X - second.X);
        int columnDifference = Math.Abs(first.Y - second.Y);

        bool rowAdjacent = false;
        bool columnAdjacent = false;

        if (rowDifference <= 1 && rowDifference >= -1)
            rowAdjacent = true;
        if (columnDifference <= 1 && columnDifference >= -1)
            columnAdjacent = true;

        if (rowAdjacent && columnAdjacent) return true;

        return false;
    }

    public static bool IsAdjacent(Coord first, int x, int y)
    {
        int rowDifference = Math.Abs(first.X - x);
        int columnDifference = Math.Abs(first.Y - y);

        bool rowAdjacent = false;
        bool columnAdjacent = false;

        if (rowDifference <= 1 && rowDifference >= -1)
            rowAdjacent = true;
        if (columnDifference <= 1 && columnDifference >= -1)
            columnAdjacent = true;

        if (rowAdjacent && columnAdjacent) return true;

        return false;
    }

    public static bool SameCoord(Coord first, Coord second)
    {
        if (first.X == second.X && first.Y == second.Y) return true;
        return false;
    }

    public static bool SameCoord(Coord first, int x, int y)
    {
        if (first.X == x && first.Y == y) return true;
        return false;
    }
}

public enum RoomTypes { Normal, Entrance, FountainRoom, Pit }
public enum Direction { North, South, East, West }
