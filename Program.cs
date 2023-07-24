﻿
Player newPlayer = new();
Cavern cavern = new();

while (true)
{
    cavern.UpdatePlayerPos(newPlayer);

    Console.WriteLine($"You are in the room at X={newPlayer.PlayerCoord.X} Y={newPlayer.PlayerCoord.Y}.");

    cavern.ShowRoomsStatus();

    cavern.CheckSpecialRoom();

    if (cavern.Win)
    {
        Console.ReadKey();
        break;
    }

    Console.WriteLine("What do you want to do?");

    string input = Console.ReadLine();
    switch (input)
    {
        case "move north":
            newPlayer.MovePlayer(input, cavern);
            break;
        case "move south":
            newPlayer.MovePlayer(input, cavern);
            break;
        case "move east":
            newPlayer.MovePlayer(input, cavern);
            break;
        case "move west":
            newPlayer.MovePlayer(input, cavern);
            break;
        case "enable fountain":
            cavern.EnableFountain(input);
            break;
    }

    Console.Clear();
}

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
        if (command == "move north")
        {
            if (PlayerCoord.Y - 1 < 0 ) return;
            PlayerCoord.Y--;
        }

        if (command == "move south")
        {
            if (PlayerCoord.Y + 1 > cavern.Grid.GetLength(0) - 1) return;
            PlayerCoord.Y++;
        }

        if (command == "move east")
        {
            if (PlayerCoord.X + 1 > cavern.Grid.GetLength(0) - 1) return;
            PlayerCoord.X++;
        }

        if (command == "move west")
        {
            if (PlayerCoord.X - 1 < 0) return;
            PlayerCoord.X--;
        }
    }

    public void ShowPlayerCoord()
    {
        Console.WriteLine(PlayerCoordString);
    }
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

    public bool[,] Grid => _grid;
    public bool FountainRoom => _fountainRoom;
    public bool FountainEnabled => _fountainEnabled;
    public bool Entrance => _entrance;
    public bool Win => _win;

    public Cavern()
    {
        _grid = new bool[4, 4];
    }

    public Cavern(int size)
    {
        _grid = new bool[size, size];
    }
    
    public void UpdatePlayerPos(Player player)
    {
        int xPos = player.PlayerCoord.X;
        int yPos = player.PlayerCoord.Y;

        for (int x = 0; x < _grid.GetLength(0); x++)
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                if (y == yPos && x == xPos) 
                    _grid[x, y] = true;
                else _grid[x, y] = false;

                if (xPos == 2 && yPos == 0) _fountainRoom = true;
                else _fountainRoom = false;

                if (xPos == 0 && yPos == 0)
                    _entrance = true;
                else _entrance = false;
            }
    }

    public void ShowRoomsStatus()
    {
        for (int x = 0; x < Grid.GetLength(0); x++)
            for (int y = 0; y < Grid.GetLength(1); y++)
                Console.WriteLine($"({x}, {y}) = {Grid[x, y]}");

        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"FountainRoom = {FountainRoom}");
        Console.WriteLine($"FountainEnabled = {FountainEnabled}");
        Console.WriteLine();
    }

    public void CheckSpecialRoom()
    {
        if (FountainRoom == true)
            if (FountainEnabled == false)
                Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
            else Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");

        if (Entrance == true)
        {
            if (FountainEnabled == true)
            {
                Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
                _win = true;
            }
            
            else Console.WriteLine("You see light coming from the cavern entrance.");
        }
    }

    public void EnableFountain(string command)
    {
        if (command == "enable fountain" && FountainEnabled == false)
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
