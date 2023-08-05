
namespace Fountain;

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
