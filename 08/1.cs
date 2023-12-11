var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var directions = lines[0].Where(char.IsLetter).ToList();
var graph = new Dictionary<string, List<string>>();

foreach (var line in lines.Skip(2))
{
    var nodeData = line.Replace(" ", "").Replace("(", "").Replace(")", "").Split("=");
    var node = nodeData[0];
    graph.Add(node, [.. nodeData[1].Split(",")]);
}

GraphSearcher("AAA", 0, directions, 0);

void GraphSearcher(string node, int counter, List<char> directions, int directionCounter)
{
    if (node == "ZZZ")
        Console.WriteLine(counter);
    else
    {
        if (directionCounter == directions.Count)
            directionCounter = 0;
        if (directions[directionCounter] == 'L')
            node = graph[node][0];
        else
            node = graph[node][1];
        GraphSearcher(node, counter + 1, directions, directionCounter + 1);
    }
}
