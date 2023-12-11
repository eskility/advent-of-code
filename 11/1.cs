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

var listOfNodes = new List<Node>();
var starCounter = 1;
for (int row = 0; row < lines.Length; row++)
{
    var starFound = false;
    for (int column = 0; column < lines[row].Length; column++)
    {
        if (lines[row][column] == '#')
        {
            var node = new Node((row, column));
            starFound = true;
            node.Id = starCounter++;
            listOfNodes.Add(node);
        }
    }
    if (!starFound)
        expandedRows.Add(row);
}

var pairs = new List<((int, int), (int, int))>();
long totalsteps = 0;
foreach (var x in listOfNodes)
{
    foreach (var y in listOfNodes)
        if (x != y && !pairs.Contains((x.Location, y.Location)) && !pairs.Contains((y.Location, x.Location)))
        {
            var a = Math.Min(x.Location.Item1, y.Location.Item1);
            var d = Math.Max(x.Location.Item2, y.Location.Item2);
            var b = Math.Max(x.Location.Item1, y.Location.Item1);
            var c = Math.Min(x.Location.Item2, y.Location.Item2);

            int e = expandedRows.Where(x => x > a && x < b).Count();
            int f = expandedColumns.Where(x => x > c && x < d).Count();

            totalsteps += Math.Abs(b - a) + Math.Abs(d - c);
            totalsteps += e + f;
            pairs.Add((x.Location, y.Location));
        }
}

Console.WriteLine(totalsteps);

class Node((int, int) _location)
{
    public int Id { get; set; }
    public (int, int) Location { get; set; } = _location;

}
