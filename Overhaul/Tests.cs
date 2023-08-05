
using Fountain;

NewPlayer player = new();
player.MoveEntity(Direction.South);

Console.WriteLine(player.Position.ToString());
