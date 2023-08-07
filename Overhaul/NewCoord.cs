
namespace Fountain;

public record NewCoord(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static NewCoord operator +(NewCoord a, NewCoord b)
        => new NewCoord(a.X + b.X, a.Y + b.Y);

    public static NewCoord operator -(NewCoord a, NewCoord b)
        => new NewCoord(a.X - b.X, a.Y - b.Y);
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

    public int? this[char index]
    {
        get
        {
            if (index == 'x') return X;
            else if (index == 'y') return Y;

            return null;
        }
    }
}
