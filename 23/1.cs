
var data = File.ReadAllText("input.txt").Split("\n");
List<int> totalDepths = [];

DFS(0, new List<(int, int)>(), (0, 1), (data.Length - 1, data[0].Length - 2), data, totalDepths);

Console.WriteLine(totalDepths.Max());

void DFS(int depth, List<(int, int)> seen, (int, int) location, (int, int) target, string[] data, List<int> totalDepths)
{
    if (location == target)
        totalDepths.Add(depth);
    else if (!seen.Contains(location))
    {
        seen.Add(location);

        var directions = new List<(int, int)>();
        if (data[location.Item1][location.Item2] == 'v')
        {
            directions.Add((location.Item1 + 1, location.Item2));
        }
        else if (data[location.Item1][location.Item2] == '^')
        {
            directions.Add((location.Item1 - 1, location.Item2));
        }
        else if (data[location.Item1][location.Item2] == '>')
        {
            directions.Add((location.Item1, location.Item2 + 1));
        }
        else if (data[location.Item1][location.Item2] == '<')
        {
            directions.Add((location.Item1, location.Item2 - 1));
        }

        else
        {
            directions.Add((location.Item1 - 1, location.Item2));
            directions.Add((location.Item1 + 1, location.Item2));
            directions.Add((location.Item1, location.Item2 + 1));
            directions.Add((location.Item1, location.Item2 - 1));
        }

        foreach (var direction in directions)
        {
            if (direction.Item1 >= 0 && direction.Item1 < data.Length && direction.Item2 >= 0
            && direction.Item2 < data[0].Length && data[direction.Item1][direction.Item2] != '#')
            {
                //make the seen list unique for every path by copying it using ToList()
                DFS(depth + 1, seen.ToList(), direction, target, data, totalDepths);
            }
        }
    }
}
