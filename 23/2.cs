var data = File.ReadAllText("input.txt").Split("\n");
var graph = new Dictionary<(int, int), List<(int, int, int)>>();

for (int row = 0; row < data.Length; row++)
{
    for (int column = 0; column < data[row].Length; column++)
    {
        if (data[row][column] != '#')
        {
            var connections = new List<(int, int)>();
            if (row > 0 && data[row - 1][column] != '#')
                connections.Add((row - 1, column));
            if (row + 1 < data.Length && data[row + 1][column] != '#')
                connections.Add((row + 1, column));
            if (column + 1 < data[row].Length && data[row][column + 1] != '#')
                connections.Add((row, column + 1));
            if (column > 0 && data[row][column - 1] != '#')
                connections.Add((row, column - 1));


            if (!graph.ContainsKey((row, column)))
                graph[(row, column)] = [];

            foreach (var connection in connections)
                if (!graph[(row, column)].Contains((connection.Item1, connection.Item2, 1)))
                    graph[(row, column)].Add((connection.Item1, connection.Item2, 1));
        }

    }
}

//combine all the nodes in a corridor together by using edge contraction.
// then we can run DFS on just the junctions and add the depth of the corridors between them
foreach (var (r, c) in graph
    .Where(n => n.Value.Count == 2)
    .Select(node => node.Key))
{
    var neighbors = graph[(r, c)];
    var (r1, c1, d1) = neighbors[0];
    var (r2, c2, d2) = neighbors[1];
   
    var n1 = graph[(r1, c1)];
    var i = n1.FindIndex(x => (x.Item1, x.Item2) == (r, c));
    n1[i] = (r2, c2, d1 + d2);

    var n2 = graph[(r2, c2)];
    i = n2.FindIndex(x => (x.Item1, x.Item2) == (r, c));
    n2[i] = (r1, c1, d1 + d2);
}

List<int> totalDepths = [];
var seen = new List<(int, int)>();

DFS(0, seen, (0, 1), (data.Length - 1, data.Length - 2), graph, totalDepths);

void DFS(int depth, List<(int, int)> seen, (int, int) location, (int, int) target, Dictionary<(int, int), List<(int, int, int)>> graph, List<int> paths)
{
    if (location == target)
    {
        paths.Add(depth);
    }
    else if (!seen.Contains(location))
    {
        seen.Add(location);
        foreach (var direction in graph[location])
        {
            DFS(depth + direction.Item3, seen.ToList(), (direction.Item1, direction.Item2), target, graph, paths);
        }
    }
}
Console.WriteLine(totalDepths.Max());
