//This is inspired by comments on reddit. Pick a random node and count the unique 
// paths between it and all the other nodes.  The reason why it works is 
// because we know nodes that have three or less unique paths between
// them and a random node must belong to another group than the random node.
// We use BFS to find the paths.

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
    var connections = 0;
    HashSet<string> paths = [];
    paths.Add(firstNode);

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
        while (queue.Count > 0)
        {
            var nodeAndPath = queue.Dequeue();
            var node = nodeAndPath.Item1;
            var path = nodeAndPath.Item2;
            foreach (var c in graph[node])
            {
                if (x == c)
                {
                    connections += 1;
                    paths.UnionWith(path);
                    queue.Clear();
                    break;
                }
                else if (!seen.Contains(c) && !path.Contains(c) && !paths.Contains(c))
                {
                    //It's important to make track of the path. We copy it so it stays unique for each
                    // item we place in the queue
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
