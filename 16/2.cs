var data = File.ReadAllText("input.txt").Split("\n");


var result = new List<int>();
var startingPoints = new List<(int, int)>();

for (int row = 0; row < data.Length; row++)
{
    startingPoints.Add((row, 0));
    startingPoints.Add((row, data[0].Length - 1));
}
for (int column = 0; column < data[0].Length; column++)
{
    startingPoints.Add((0, column));
    startingPoints.Add((data.Length - 1, column));
}

foreach (var point in startingPoints)
{
    var traveled = new List<((int, int), Direction)>();
    var energized = new List<(int, int)>();
    Scan((point), traveled, energized, Direction.East, data);
    result.Add(energized.Count);
}



Console.WriteLine(result.Max());

static void Scan((int, int) location, List<((int, int), Direction)> traveled, List<(int, int)> energized, Direction direction, string[] data)
{

    if (!traveled.Contains(((location.Item1, location.Item2), direction)) && location.Item1 >= 0 && location.Item2 >= 0
    && location.Item1 < data.Length && location.Item2! < data[location.Item1].Length)
    {
        traveled.Add(((location.Item1, location.Item2), direction));
        if (!energized.Contains(location))
            energized.Add(location);

        if (data[location.Item1][location.Item2] == '.')
        {
            if (direction == Direction.North)
                Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);
            else if (direction == Direction.South)
                Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);
            else if (direction == Direction.West)
                Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);
            else if (direction == Direction.East)
                Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);
        }
        else if (data[location.Item1][location.Item2] == '-')
        {
            if (direction == Direction.North || direction == Direction.South)
            {
                Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);
                Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);
            }
            else if (direction == Direction.West)
                Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);
            else if (direction == Direction.East)
                Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);
        }
        else if (data[location.Item1][location.Item2] == '|')
        {
            if (direction == Direction.West || direction == Direction.East)
            {
                Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);
                Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);
            }
            else if (direction == Direction.North)
                Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);
            else if (direction == Direction.South)
                Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);
        }
        else if (data[location.Item1][location.Item2] == '\\')
        {
            if (direction == Direction.North)
                Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);
            else if (direction == Direction.South)
                Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);
            else if (direction == Direction.West)
                Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);
            else if (direction == Direction.East)
                Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);
        }
        else if (data[location.Item1][location.Item2] == '/')
        {
            if (direction == Direction.North)
                Scan((location.Item1, location.Item2 + 1), traveled, energized, Direction.East, data);
            else if (direction == Direction.South)
                Scan((location.Item1, location.Item2 - 1), traveled, energized, Direction.West, data);
            else if (direction == Direction.East)
                Scan((location.Item1 - 1, location.Item2), traveled, energized, Direction.North, data);
            else if (direction == Direction.West)
                Scan((location.Item1 + 1, location.Item2), traveled, energized, Direction.South, data);
        }
    }
}

enum Direction
{
    North, South, West, East
}
