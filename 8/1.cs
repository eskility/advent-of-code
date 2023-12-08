var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");

var instructions = lines[0].Where(char.IsLetter).ToList();
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

    var left = new Node(nodeData[1].Split(",")[0]);
    var right = new Node(nodeData[1].Split(",")[1]);
    graph[node.Id].Left = left;
    graph[node.Id].Right = right;

}

var stepCounter = 0;
var currentNode = graph["AAA"];
var i = 0;
while (currentNode.Id != "ZZZ")
{
    if (i == instructions.Count)
        i = 0;

    stepCounter++;

    if (instructions[i] == 'L')
        currentNode = graph[currentNode.Left.Id];
    else
        currentNode = graph[currentNode.Right.Id];


    i++;

}

Console.WriteLine(stepCounter);

class Node(string _id)
{

    public string Id { get; set; } = _id;
    public Node? Left { get; set; }
    public Node? Right { get; set; }

}
