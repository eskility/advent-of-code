using System.Data;
using System.Text;

var file = File.ReadAllText("input.txt");
var table = file.Split("\n");



Spin(table, 100000);

static void Spin(string[] table, long cycles)
{
    var dict = new Dictionary<(int, int), char>();
    for (int row = 0; row < table.Length; row++)
    {
        for (int column = 0; column < table[row].Length; column++)
        {
            dict.Add((row, column), table[row][column]);
        }
    }

    var seen = new Dictionary<Int32, Dictionary<(int, int), char>>();
    for (long i = 0; i < cycles; i++)
    {
        var sb = new StringBuilder();
        foreach (var x in dict)
            sb.Append(x.Value);


        var hash = sb.ToString().GetHashCode();

        if (seen.TryGetValue(hash, out Dictionary<(int, int), char>? value))
        {
            long first = seen.TakeWhile(x => x.Key != hash).Count();
            Spin(table, (1000000000 - first) % (i - first) + first);
            goto End;

        }

        foreach (var x in dict.Where(x => x.Value == 'O'))
        {
            var rowlocation = x.Key.Item1;
            var columnlocation = x.Key.Item2;

            var j = 0;
            for (int counter = rowlocation - 1; counter >= 0; counter--)
            {
                if (dict[(counter, columnlocation)] == '.')
                    j++;
                else
                    break;
            }
            rowlocation = rowlocation - j;
            (dict[(x.Key.Item1, x.Key.Item2)], dict[(rowlocation, columnlocation)]) = (dict[(rowlocation, columnlocation)], dict[(x.Key.Item1, x.Key.Item2)]);

        }


        foreach (var x in dict.Where(x => x.Value == 'O'))
        {
            var rowlocation = x.Key.Item1;
            var columnlocation = x.Key.Item2;
            var row = x.Key.Item1;
            var column = x.Key.Item2;

            var j = 0;
            for (int counter = columnlocation - 1; counter >= 0; counter--)
            {
                if (dict[(rowlocation, counter)] == '.')
                    j++;
                else
                    break;
            }
            columnlocation = columnlocation - j;
            (dict[(row, column)], dict[(rowlocation, columnlocation)]) = (dict[(rowlocation, columnlocation)], dict[(row, column)]);
        }



        foreach (var x in dict.Where(x => x.Value == 'O').OrderByDescending(x => x.Key))
        {
            var rowlocation = x.Key.Item1;
            var columnlocation = x.Key.Item2;
            var row = x.Key.Item1;
            var column = x.Key.Item2;
            var j = 0;
            for (int counter = rowlocation + 1; counter < table.Length; counter++)
            {
                if (dict[(counter, columnlocation)] == '.')
                    j++;
                else
                    break;
            }
            rowlocation = rowlocation + j;
            (dict[(row, column)], dict[(rowlocation, columnlocation)]) = (dict[(rowlocation, columnlocation)], dict[(row, column)]);
        }


        foreach (var x in dict.Where(x => x.Value == 'O').OrderByDescending(x => x.Key))
        {
            var rowlocation = x.Key.Item1;
            var columnlocation = x.Key.Item2;
            var row = x.Key.Item1;
            var column = x.Key.Item2;
            var j = 0;

            for (int counter = columnlocation + 1; counter < table[row].Length; counter++)
            {
                if (dict[(rowlocation, counter)] == '.')
                    j++;
                else
                    break;
            }
            columnlocation = columnlocation + j;
            (dict[(row, column)], dict[(rowlocation, columnlocation)]) = (dict[(rowlocation, columnlocation)], dict[(row, column)]);
        }
        if (!seen.ContainsKey(hash))
            seen.Add(hash, dict);

    }
    var sum = 0;

    for (int x = 0; x < table.Length; x++)
    {
        for (int y = 0; y < table[x].Length; y++)
        {
            if (dict[(x, y)] == 'O')
                sum += table[x].Length - x;
        }
    }

    Console.WriteLine(sum);
End:
    { }
}


