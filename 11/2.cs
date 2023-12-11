using System.Data;
using System.Text;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var newLines = new List<string>();
var columns = new List<int>();
var rows = new List<int>();

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

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i].Where(x => x == '.').Count() == lines[i].Length)
    {
        rows.Add(i);
    }
    newLines.Add(lines[i]);

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
long totalsteps = 0;
foreach (var x in listOfNodes)
{
    foreach (var y in listOfNodes)
        if (x != y && !pairs.Contains((x.Location, y.Location)) && !pairs.Contains((y.Location, x.Location)))
        {
            var xx = Math.Max(x.Location.Item2, y.Location.Item2);
            var yy = Math.Min(x.Location.Item2, y.Location.Item2);
            var zz = Math.Max(x.Location.Item1, y.Location.Item1);
            var vv = Math.Min(x.Location.Item1, y.Location.Item1);

            long columncount = columns.Where(x => x > yy && x < xx).Count();
            long rowcount = rows.Where(x => x > vv && x < zz).Count();

            pairs.Add((x.Location, y.Location));


            totalsteps += Math.Abs(xx - yy) + Math.Abs(zz - vv);
           totalsteps += (columncount*1000000)-columncount + (rowcount*1000000)-rowcount;

        }
}

Console.WriteLine(totalsteps);

class Node((int, int) _location)
{
    public int Id { get; set; }
    public (int, int) Location { get; set; } = _location;

}
