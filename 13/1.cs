using System.Text;

var file = File.ReadAllText("input.txt");
var input = file.Split("\n\n");
var sum = 0;

for (int i = 0; i < input.Length; i++)
{
    var rows = input[i].Split("\n");
    var columns = new List<string>();

    for (int column = 0; column < rows[0].Length; column++)
    {
        var sb = new StringBuilder();

        for (int row = 0; row < rows.Length; row++)
        {
            sb.Append(rows[row][column]);
        }
        columns.Add(sb.ToString());
    }
    sum += FindReflection(columns.ToArray(), false);
    sum += FindReflection(rows, true);

}
Console.WriteLine(sum);

static int FindReflection(string[] data, bool horizontal)
{
    for (int x = 0; x < data.Length - 1; x++)
    {
        if (data[x] == data[x + 1])
        {
            var reflection = true;
            int y = x, z = x + 1;
            while (y >= 0 && z <= data.Length - 1)
            {
                if (data[y] != data[z])
                {
                    reflection = false;
                    break;
                }
                y--; z++;
            }
            if (reflection)
                if (horizontal)
                    return ((x + 1) * 100);
                else
                    return x + 1;
        }
    }
    return 0;
}
