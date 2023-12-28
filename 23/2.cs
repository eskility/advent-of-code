
using System.Data;

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
            {
                graph[(row, column)] = [];
            }
            foreach (var connection in connections)
                if (!graph[(row, column)].Contains((connection.Item1, connection.Item2, 0)))
                    graph[(row, column)].Add((connection.Item1, connection.Item2, 0));

        }

    }
}



var corridors = graph.Where(n => n.Value.Count == 2)
    .Select(node => node.Key)
    .ToList();
foreach (var (r, c) in corridors)
{
    var neighbors = graph[(r, c)];
    var (r1, c1, d1) = neighbors[0];
    var (r2, c2, d2) = neighbors[1];
    var n1 = graph[(r1, c1)];
    var i = n1.FindIndex(item => (item.Item1, item.Item2) == (r, c));
    if (i != -1)
    {
        n1[i] = (r2, c2, d1 + d2);
    }
    var n2 = graph[(r2, c2)];
    i = n2.FindIndex(item => (item.Item1, item.Item2) == (r, c));
    if (i != -1)
    {
        n2[i] = (r1, c1, d1 + d2);
    }
}
List<int> paths = [];
var seen = new List<(int, int)>();
seen.Add((0, 1));
df(0, seen, (0, 1), (data.Length - 1, data.Length - 2), graph, paths);
DFS(0, seen, (0, 1), (data.Length - 1, data.Length - 2), graph, paths);


void DFS(int depth, List<(int, int)> seen, (int, int) location, (int, int) target, Dictionary<(int, int), List<(int, int, int)>> graph, List<int> paths)
{
    if (location == target)
    {

    }
    else
    {

        foreach (var direction in graph[location])
        {
            if (!seen.Contains((direction.Item1, direction.Item2)))
            {
                seen.Add((direction.Item1, direction.Item2));
                DFS(depth + 1, seen.ToList(), (direction.Item1, direction.Item2), target, graph, paths);

            }
        }
    }
}
Console.WriteLine(paths.Max());

void df(int depth, List<(int, int)> seen, (int, int) location, (int, int) target, Dictionary<(int, int), List<(int, int, int)>> graph, List<int> paths)
{
    if (location == target)
        paths.Add(depth);
    else
    {

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
            && direction.Item2 < data[0].Length && data[direction.Item1][direction.Item2] != '#'
            && !seen.Contains(direction))
            {
                seen.Add(direction);
                        graph[location].Add((direction.Item1,direction.Item2,()
                //make the seen list unique for every path by copying it using ToList()
                df(depth + 1, seen.ToList(), direction, target, graph, paths);
            }
        }
    }
}
