using System.Data;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var expandedColumns = new List<int>();
var expandedRows = new List<int>();

for (int column = 0; column < lines[0].Length; column++)
{
    var emptyColumn = true;
    for (int row = 0; row < lines.Length; row++)
    {
        if (lines[row][column] != '.')
            emptyColumn = false;
    }
    if (emptyColumn)
        expandedColumns.Add(column);
}

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i].Where(x => x == '.').Count() == lines[i].Length)
    {
        expandedRows.Add(i);
    }
}

var listOfNodes = new List<Node>();
var starCounter = 1;
for (int row = 0; row < lines.Length; row++)
{
    for (int column = 0; column < lines[row].Length; column++)
    {
        var node = new Node((row, column));

        if (lines[row][column] == '#')
        {
            node.Id = starCounter++;
            listOfNodes.Add(node);
        }
    }
}

var pairs = new List<((int, int), (int, int))>();
long totalsteps = 0;
foreach (var x in listOfNodes)
{
    foreach (var y in listOfNodes)
        if (x != y && !pairs.Contains((x.Location, y.Location)) && !pairs.Contains((y.Location, x.Location)))
        {
            var a = Math.Max(x.Location.Item2, y.Location.Item2);
            var b = Math.Min(x.Location.Item2, y.Location.Item2);
            var c = Math.Max(x.Location.Item1, y.Location.Item1);
            var d = Math.Min(x.Location.Item1, y.Location.Item1);
            long e = expandedColumns.Where(x => x > b && x < a).Count();
            long f = expandedRows.Where(x => x > d && x < c).Count();

            totalsteps += Math.Abs(a - b) + Math.Abs(c - d);
            totalsteps += (e * 1000000) - e + (f * 1000000) - f;
            pairs.Add((x.Location, y.Location));
        }
}

Console.WriteLine(totalsteps);

class Node((int, int) _location)
{
    public int Id { get; set; }
    public (int, int) Location { get; set; } = _location;

}
