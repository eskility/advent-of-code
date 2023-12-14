using System.Data;

var file = File.ReadAllText("input.txt");
var table = file.Split("\n");

var dict = new Dictionary<(int, int), char>();
for (int row = 0; row < table.Length; row++)
{
    for (int column = 0; column < table[row].Length; column++)
    {
        dict.Add((row, column), table[row][column]);
    }
}
Spin(dict, table, 1000000000);

static void Spin(Dictionary<(int, int), char> dict, string[] table, int cycles)
{
    for (int i = 0; i < cycles; i++)
    {
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



        foreach (var x in dict.Where(x => x.Value == 'O'))
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


        foreach (var x in dict.Where(x => x.Value == 'O'))
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

    }
}

    


for (int row = 0; row < table.Length; row++)
{
    for (int column = 0; column < table[row].Length; column++)
    {
        Console.Write(dict[(row, column)]);
    }
    Console.WriteLine();
}
