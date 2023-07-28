
using Fountain;

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

public enum RoomTypes { Normal, Entrance, FountainRoom, Pit }
public enum Direction { North, South, East, West }
