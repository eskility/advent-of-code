var data = File.ReadAllText("input.txt").Split("\n");
var result = new List<int>();
var startingPoints = new List<(int, int)>();
var traveled = new List<((int, int), Direction)>();
var energized = new List<(int, int)>();

for (int row = 0; row < data.Length; row++)
{
    traveled.Clear();
    energized.Clear();
    Scan(row, 0, traveled, energized, Direction.East, data);
    result.Add(energized.Count);
    traveled.Clear();
    energized.Clear();
    Scan(row, data[row].Length - 1, traveled, energized, Direction.West, data);
    result.Add(energized.Count);
}
for (int column = 0; column < data[0].Length; column++)
{
    traveled.Clear();
    energized.Clear();
    Scan(0, column, traveled, energized, Direction.South, data);
    result.Add(energized.Count);
    traveled.Clear();
    energized.Clear();
    Scan(data.Length - 1, column, traveled, energized, Direction.North, data);
    result.Add(energized.Count);
}

Console.WriteLine(result.Max());

static void Scan(int x, int y, List<((int, int), Direction)> traveled, List<(int, int)> energized, Direction direction, string[] data)
{
    if (!traveled.Contains(((x, y), direction)) && x >= 0 && y >= 0
    && x < data.Length && y! < data[x].Length)
    {
        traveled.Add(((x, y), direction));
        if (!energized.Contains((x, y)))
            energized.Add((x, y));

        if (data[x][y] == '.' && direction == Direction.North
        || data[x][y] == '|' && direction == Direction.North
        || data[x][y] == '|' && direction == Direction.West
        || data[x][y] == '|' && direction == Direction.East
        || data[x][y] == '\\' && direction == Direction.West
        || data[x][y] == '/' && direction == Direction.East)
            Scan(x - 1, y, traveled, energized, Direction.North, data);

        if (data[x][y] == '.' && direction == Direction.South
        || data[x][y] == '|' && direction == Direction.South
        || data[x][y] == '|' && direction == Direction.West
        || data[x][y] == '|' && direction == Direction.East
        || data[x][y] == '\\' && direction == Direction.East
        || data[x][y] == '/' && direction == Direction.West)
            Scan(x + 1, y, traveled, energized, Direction.South, data);

        if (data[x][y] == '.' && direction == Direction.West
        || data[x][y] == '-' && direction == Direction.North
        || data[x][y] == '-' && direction == Direction.South
        || data[x][y] == '-' && direction == Direction.West
        || data[x][y] == '\\' && direction == Direction.North
        || data[x][y] == '/' && direction == Direction.South)
            Scan(x, y - 1, traveled, energized, Direction.West, data);

        if (data[x][y] == '.' && direction == Direction.East
        || data[x][y] == '-' && direction == Direction.North
        || data[x][y] == '-' && direction == Direction.South
        || data[x][y] == '-' && direction == Direction.East
        || data[x][y] == '\\' && direction == Direction.South
        || data[x][y] == '/' && direction == Direction.North)
            Scan(x, y + 1, traveled, energized, Direction.East, data);
    }
}


enum Direction
{
    North, South, West, East
}
