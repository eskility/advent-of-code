var data = File.ReadAllText("input.txt").Split("\n");
List<int> finishedSteps = [];
DFS(0, "", (0, 1), data, finishedSteps);

void DFS(int steps, string path, (int, int) location, string[] data, List<int> finishedSteps)
{
    var directions = new List<(int, int)>();

    if (data[location.Item1][location.Item2] != '#')
    {
        finishedSteps.Add(steps);
        path += location;
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
            if (direction.Item1 >= 0 && direction.Item1 < data.Length && direction.Item2 >= 0 && direction.Item2 < data[0].Length && !path.Contains("" + direction))
            
                    DFS(steps + 1, path.ToString(), direction, data, finishedSteps);
        }
    }
}

Console.WriteLine(finishedSteps.Max());
