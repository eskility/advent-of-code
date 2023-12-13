using System.Text;

var file = File.ReadAllText("input.txt");
var input = file.Split("\n\n");
var sum = 0;

for (int i = 0; i < input.Length; i++)
{
    var rows = input[i].Split("\n");
    List<string> columns = [];

    for (int column = 0; column < rows[0].Length; column++)
    {
        var sb = new StringBuilder();

        for (int row = 0; row < rows.Length; row++)
        {
            sb.Append(rows[row][column]);
        }
        columns.Add(sb.ToString());
    }

    sum += FindReflection(rows) * 100;
    sum += FindReflection(columns.ToArray());
}

Console.WriteLine(sum);

static int FindReflection(string[] data)
{
    for (int x = 0; x < data.Length - 1; x++)
    {
        var differences = 0;
        int a = x, b = x + 1;
        while (a >= 0 && b <= data.Length - 1)
        {
            for (int c = 0; c < data[0].Length; c++)
            {
                if (data[a][c] != data[b][c])
                    differences++;
            }
            a--; b++;
        }

        if (differences == 1)
        {
            return x + 1;
        }
    }
    return 0;
}
