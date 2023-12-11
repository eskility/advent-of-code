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

var listOfNodes = new List<Node>();
var starCounter = 1;
for (int row = 0; row < newLines.Count; row++)
{
    for (int column = 0; column < newLines[row].Length; column++)
    {

        var node = new Node((row, column));

      if (newLines[row][column] == '#')
        {
            node.Id = starCounter++;
            listOfNodes.Add(node);
        }
    
    }
}

var pairs = new List<((int, int), (int, int))>();
var totalsteps = 0;
foreach (var x in listOfNodes)
{
    foreach (var y in listOfNodes)
        if (x != y && !pairs.Contains((x.Location, y.Location)) && !pairs.Contains((y.Location, x.Location)))
        {
            pairs.Add((x.Location, y.Location));
            totalsteps += Math.Abs(x.Location.Item1 - y.Location.Item1) + Math.Abs(x.Location.Item2 - y.Location.Item2);
        }

}

Console.WriteLine(totalsteps);


class Node((int, int) _location)
{
    public int Id { get; set; }
    public (int, int) Location { get; set; } = _location;
    
}
