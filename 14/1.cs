using System.Text;

var file = File.ReadAllText("input.txt");
var rows = file.Split("\n");
var sum = 0;
List<char[]> columns = [];



for (int column = 0; column < rows[0].Length; column++)
{
    var chars = new char[rows[0].Length];

    for (int row = 0; row < rows.Length; row++)
    {
        chars[row] = rows[row][column];
    }
    columns.Add(chars);
}

for (int x = 0; x < columns.Count; x++)
{
    for (int y = 0; y < columns[x].Length; y++)
    {
        var j = 0;

        if (columns[x][y] == 'O')
        {
            for (int counter = y - 1; counter >= 0; counter--)
            {
                if (columns[x][counter] == '.')
                    j++;
                else
                    break;
            }

            (columns[x][y], columns[x][y - j]) = (columns[x][y - j], columns[x][y]);
        }
    }

}
for (int x = 0; x < columns.Count; x++)
{
    for (int y = 0; y < columns[x].Length; y++)

    {
        if (columns[x][y] == 'O')
        {
            sum += columns[x].Length - y;
        }
    }
}
Console.WriteLine(sum);
