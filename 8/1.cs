var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var directions = lines[0].Where(char.IsLetter).ToList();
var graph = new Dictionary<string, Node>();

foreach (var line in lines.Skip(2))
{
    var nodeData = line.Replace(" ", "").Replace("(", "").Replace(")", "").Split("=");
    var node = new Node(nodeData[0]);
    if (graph.ContainsKey(node.Id))
    {
        node = graph[node.Id];
    }
    else
        graph.Add(node.Id, node);

    graph[node.Id].Left = new Node(nodeData[1].Split(",")[0]);
    graph[node.Id].Right = new Node(nodeData[1].Split(",")[1]); 
}

GraphSearcher(graph["AAA"], 0, directions, 0);


void GraphSearcher(Node node, int counter, List<char> directions, int directionCounter)
{

    node = graph[node.Id];
    if (node.Id == "ZZZ")
        Console.WriteLine(counter);
    else 
    {
        if (directionCounter == directions.Count)
            directionCounter = 0;

        if (directions[directionCounter] == 'L')
            GraphSearcher(node.Left, counter + 1, directions, directionCounter + 1);
        else
            GraphSearcher(node.Right, counter + 1, directions, directionCounter + 1);
    }
}

class Node(string _id)
{
    public string Id { get; set; } = _id;
    public Node? Left { get; set; }
    public Node? Right { get; set; }

}
