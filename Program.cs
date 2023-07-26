﻿
//////////////////////////
// CLASSES AND THE REST //
//////////////////////////
public class Player
{
    public bool Dead { get; private set; }
    public Coord PlayerCoord { get; private set; }
    public string PlayerCoordString => $"({PlayerCoord.X}, {PlayerCoord.Y})";

    public Player()
    {
        Dead = false;
        PlayerCoord = new(0, 0);
    }

    public void MovePlayer(string command, Cavern cavern)
    {
        switch (command)
        {
            case "move north":
                if (PlayerCoord.Y - 1 < 0) return;
                PlayerCoord.Y--;
                break;
            case "move south":
                if (PlayerCoord.Y + 1 > cavern.Grid.GetLength(0) - 1) return;
                PlayerCoord.Y++;
                break;
            case "move east":
                if (PlayerCoord.X + 1 > cavern.Grid.GetLength(0) - 1) return;
                PlayerCoord.X++;
                break;
            case "move west":
                if (PlayerCoord.X - 1 < 0) return;
                PlayerCoord.X--;
                break;
        }
    }

    public void ShowPlayerCoord()
    {
        Console.WriteLine(PlayerCoordString);
    }
}

public class Map
{
    private RoomTypes[,] _rooms;
    private bool[,] _playerLoc;
    private int _gridSize;
    private RoomTypes _playerRoom;

    public RoomTypes PlayerRoom => _playerRoom;
    
    public Map(int size)
    {
        _gridSize = size;
        _playerRoom = RoomTypes.Normal;

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

    }
} 

public class Game
{

}

public class Cavern
{
    // All indexes in _grid are false by default. After making a new
    // instance of Cavern, call UpdatePlayerPos to set the starting point.
    private bool[,] _grid;
    private bool _fountainRoom = false;
    private bool _fountainEnabled = false;
    private bool _win = false;
    private bool _entrance = false;

    private readonly int _gridSize;

    private Coord FountainCoord
    {
        get
        {
            if (_gridSize == 6) return new Coord(3, 0);
            else if (_gridSize == 8) return new Coord(4, 0);
            return new Coord(2, 0);
        }
    }

    public bool[,] Grid => _grid;
    public bool FountainRoom => _fountainRoom;
    public bool FountainEnabled => _fountainEnabled;
    public bool Entrance => _entrance;
    public bool Win => _win;
    public int GridSize => _gridSize;

    public Cavern()
    {
        _gridSize = 4;
        _grid = new bool[4, 4];
    }

    public Cavern(int gridSize)
    {
        _gridSize = gridSize;
        _grid = new bool[gridSize, gridSize];
    }
    
    public void UpdatePlayerPos(Player player)
    {
        int xPos = player.PlayerCoord.X;
        int yPos = player.PlayerCoord.Y;

        for (int x = 0; x < _grid.GetLength(0); x++)
        {
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                if (y == yPos && x == xPos)
                    _grid[x, y] = true;
                else _grid[x, y] = false;

                if (xPos == FountainCoord.X && yPos == FountainCoord.Y) _fountainRoom = true;
                else _fountainRoom = false;

                if (xPos == 0 && yPos == 0)
                    _entrance = true;
                else _entrance = false;
            }
        }
    }

    /// <summary>
    /// Shows the current location of the player within a grid representation of the world.
    /// Also shows conditions that have or haven't been fulfulled.
    /// Only for testing purposes.
    /// </summary>
    public void ShowRoomsStatus()
    {
        for (int y = 0; y < Grid.GetLength(0); y++)
        {
            Console.WriteLine();

            for (int x = 0; x < Grid.GetLength(1); x++)
            {
                if (Grid[x, y] == true) Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(String.Format("({0},{1})", x, y));
                Console.ResetColor();
                Console.Write(" | ");
            }
        }

        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine($"FountainRoom = {FountainRoom}");
        Console.WriteLine($"FountainEnabled = {FountainEnabled}");
        Console.WriteLine($"Entrance = {Entrance}");
        Console.WriteLine();
    }

    /// <summary>
    /// Check if a room is a "special room", i.e. the fountain room or the entrance.
    /// </summary>
    public void CheckSpecialRoom()
    {
        if (FountainRoom == true)
        {
            if (FountainEnabled == false)
                Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");

            else 
                Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
        }

        if (Entrance == true)
        {
            if (FountainEnabled == true)
            {
                _win = true;
                Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
            }
            
            else Console.WriteLine("You see light coming from the cavern entrance.");
        }
    }

    /// <summary>
    /// Enables the fountain if the player is in the fountain room and the fountain is disabled.
    /// </summary>
    /// <param name="command"></param>
    public void EnableFountain(string command)
    {
        if (command == "enable fountain" && FountainEnabled == false && FountainRoom == true)
            _fountainEnabled = true;
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
}

public enum RoomTypes { Normal, Entrance, FountainRoom, Pit, Exit }
