using System.Text;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var newLines = new List<string>();
var columns = new List<int>();

for (int column = 0; column < lines[0].Length; column++)
{
    var emptyColumn = true;
    for (int row = 0; row < lines.Length; row++)
    {
        if (lines[row][column] != '.')
            emptyColumn = false;
    }
    if (emptyColumn)
        columns.Add(column);
}


foreach (var line in lines)
{
    var sb = new StringBuilder();
    sb.AppendLine(line);

    for (int i = 0; i < columns.Count; i++)
        sb.Insert(columns[i] + i, ".");
    newLines.Add(sb.ToString());
    if (line.Where(x => x == '.').Count() == line.Length)
    {
        newLines.Add(sb.ToString());
    }
}

var queue = new Queue<Node>();
var listOfNodes = new List<Node>();
var mapOfNodes = new Dictionary<(int, int), Node>();
var starCounter = 1;
for (int row = 0; row < newLines.Count; row++)
{
    for (int column = 0; column < newLines[row].Length; column++)
    {

        var node = new Node((row, column));

        if (row > 0)
            node.Connections.Add((row - 1, column));
        if (column > 1)
            node.Connections.Add((row, column - 1));
        if (row < newLines.Count - 1)
            node.Connections.Add((row + 1, column));
        if (column < newLines[row].Length - 1)
            node.Connections.Add((row, column + 1));

        if (newLines[row][column] == '#')
        {
            node.Id = starCounter++;
            listOfNodes.Add(node);
        }
        mapOfNodes.Add(node.Location, node);
    }
}

var pairs = new List<((int, int), (int, int))>();

foreach (var x in listOfNodes)
{
    foreach (var y in listOfNodes)
        if (x != y)
            pairs.Add((x.Location, y.Location));

}

var pairsFound = new List<(int?, int?)>();
var totalsteps = 0;

foreach (var pair in pairs)
{
    var source = mapOfNodes[pair.Item1];
    var target = mapOfNodes[pair.Item2];
    var searched = new HashSet<(int, int)>();
    var steps = 0;
    if (!pairsFound.Contains((source.Id, target.Id)) && !pairsFound.Contains((target.Id, source.Id)))
    {
        queue.Enqueue(mapOfNodes[pair.Item1]);
        while (queue.Count > 0)
        {
            var queuesize = queue.Count;
            for (int i = 0; i < queuesize; i++)
            {
                var node = queue.Dequeue();

                if (!searched.Contains(node.Location))
                {
                    searched.Add(node.Location);
                    if (node.Connections.Contains(target.Location))
                    {
                        totalsteps += steps + 1;
                        pairsFound.Add((target.Id, source.Id));
                        queue.Clear();
                        break;
                    }

                    else
                    {
                        foreach (var x in node.Connections)
                        {
                            if (!searched.Contains(x))
                                queue.Enqueue(mapOfNodes[x]);
                        }
                    }
                }
            }
            steps++;
        }
    }
}
Console.WriteLine(totalsteps);




class Node((int, int) _location)
{
    public int? Id { get; set; }
    public (int, int) Location { get; set; } = _location;
    public List<(int, int)> Connections { get; set; } = [];
}
