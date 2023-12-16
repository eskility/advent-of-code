var data = File.ReadAllText("input.txt").Split("\n");
var traveled = new List<((int, int), Direction)>();
var energized = new List<(int, int)>();

Scan((0, 0), traveled, energized, Direction.East, data);

Console.WriteLine(energized.Count);

static void Scan((int, int) location, List<((int, int), Direction)> traveled, List<(int, int)> energized, Direction direction, string[] data)
{
    if (!traveled.Contains(((location.Item1, location.Item2), direction)) && location.Item1 >= 0 && location.Item2 >= 0
    && location.Item1 < data.Length && location.Item2! < data[location.Item1].Length)
    {
        traveled.Add(((location.Item1, location.Item2), direction));
        if (!energized.Contains(location))
            energized.Add(location);

        if (data[location.Item1][location.Item2] == '.' && direction == Direction.North
            || data[location.Item1][location.Item2] == '|' && direction == Direction.North
            || data[location.Item1][location.Item2] == '|' && direction == Direction.West
            || data[location.Item1][location.Item2] == '|' && direction == Direction.East
            || data[location.Item1][location.Item2] == '\\' && direction == Direction.West
            || data[location.Item1][location.Item2] == '/' && direction == Direction.East)
                {Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);}

        if (data[location.Item1][location.Item2] == '.' && direction == Direction.South
            || data[location.Item1][location.Item2] == '|' && direction == Direction.South
            || data[location.Item1][location.Item2] == '|' && direction == Direction.West
            || data[location.Item1][location.Item2] == '|' && direction == Direction.East
            || data[location.Item1][location.Item2] == '\\' && direction == Direction.East
            || data[location.Item1][location.Item2] == '/' && direction == Direction.West)
                {Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);}

        if (data[location.Item1][location.Item2] == '.' && direction == Direction.West
            || data[location.Item1][location.Item2] == '-' && direction == Direction.North
            || data[location.Item1][location.Item2] == '-' && direction == Direction.South
            || data[location.Item1][location.Item2] == '-' && direction == Direction.West
            || data[location.Item1][location.Item2] == '\\' && direction == Direction.North
            || data[location.Item1][location.Item2] == '/' && direction == Direction.South)
                {Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);}

        if (data[location.Item1][location.Item2] == '.' && direction == Direction.East
            || data[location.Item1][location.Item2] == '-' && direction == Direction.North
            || data[location.Item1][location.Item2] == '-' && direction == Direction.South
            || data[location.Item1][location.Item2] == '-' && direction == Direction.East
            || data[location.Item1][location.Item2] == '\\' && direction == Direction.South
            || data[location.Item1][location.Item2] == '/' && direction == Direction.North)
                {Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);}
    }
}


enum Direction
{
    North, South, West, East
}
