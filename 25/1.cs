
Dictionary<string, HashSet<string>> graph = [];
var data = File.ReadAllLines("input.txt");
foreach (string line in data)
{
    var nodes = line.Trim().Split(':');
    var left = nodes[0].Replace(" ", "");
    var right = nodes[1].Split(' ').Skip(1).ToArray();
    foreach (var node in right)
    {
        if (!graph.ContainsKey(left))
        {
            graph[left] = [];
        }
        graph[left].Add(node);
        if (!graph.ContainsKey(node))
        {
            graph[node] = [];
        }
        graph[node].Add(left);
    }
}

var group2 = 0;
var firstNode = graph.First().Key;

foreach (var x in graph.Keys.Skip(1))
{
    int connections = 0;
    HashSet<string> usedNodes = [];
    usedNodes.Add(firstNode);
    foreach (var y in graph[firstNode])
    {
        if (x == y)
        {
            connections += 1;
            continue;
        }
        HashSet<string> seen = [];
        Queue<(string, HashSet<string>)> queue = [];
        queue.Enqueue((y, new HashSet<string> { y }));
        bool found = false;
        while (queue.Count > 0 && !found && connections < 4)
        {
            var compPath = queue.Dequeue();
            string comp = compPath.Item1;
            var path = compPath.Item2;
            foreach (var c in graph[comp])
            {
                if (x == c)
                {
                    connections += 1;
                    usedNodes.UnionWith(path);
                    found = true;
                    break;
                }
                else if (!seen.Contains(c) && !path.Contains(c) && !usedNodes.Contains(c))
                {
                    var pathcopy = path.ToHashSet();
                    pathcopy.Add(c);
                    queue.Enqueue((c, pathcopy));
                    seen.Add(c);
                }
            }
        }
    }
    if (connections <= 3)
    {
        group2 += 1;
    }
}

Console.WriteLine((graph.Keys.Count - group2) * group2);
