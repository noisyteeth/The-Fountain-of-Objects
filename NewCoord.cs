
namespace Fountain;

public record NewCoord(int X, int Y)
{
    public static NewCoord operator +(NewCoord a, NewCoord b)
        => new NewCoord(a.X + b.X, a.Y + b.Y);

    public static NewCoord operator +(NewCoord a, Direction dir)
    {
        return dir switch
        {
            Direction.North => new(a.X, a.Y - 1),
            Direction.South => new(a.X, a.Y + 1),
            Direction.East => new(a.X + 1, a.Y),
            Direction.West => new(a.X - 1, a.Y)
        };
    }

    public static NewCoord operator +(Direction dir, NewCoord a)
        => a + dir;
}
