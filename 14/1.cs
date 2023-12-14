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
        if (chars[row] == 'O')
        {
            var j = 0;
            for (int counter = row - 1; counter >= 0; counter--)
            {
                if (chars[counter] == '.')
                    j++;
                else
                    break;
            }
            (chars[row], chars[row - j]) = (chars[row - j], chars[row]);
        }
    }
    columns.Add(chars);
}
for (int x = 0; x < columns.Count; x++)
{
    for (int y = 0; y < columns[x].Length; y++)
    {
        if (columns[x][y] == 'O')
            sum += columns[x].Length - y;
    }
}
Console.WriteLine(sum);
