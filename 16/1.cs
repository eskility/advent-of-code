var data = File.ReadAllText("input.txt").Split("\n");
var traveled = new List<((int, int), Direction)>();
var energized = new List<(int, int)>();

Scan((0, 0), traveled, energized, Direction.East, data);

Console.WriteLine(energized.Count);

static void Scan(int x, int y, List<((int, int), Direction)> traveled, List<(int, int)> energized, Direction direction, string[] data)
{var data = File.ReadAllText("input.txt").Split("\n");
var traveled = new List<((int, int), Direction)>();
var energized = new List<(int, int)>();

Scan(0, 0, traveled, energized, Direction.East, data);

Console.WriteLine(energized.Count);

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
