var data = File.ReadAllText("input.txt").Split("\n");

long x = 0, y = 0;
List<(long, long)> list = [];
long perimeter = 0;

foreach (var line in data)
{

    var dig = line.Split(" ");

    var hex = dig[2].TrimStart('(').TrimEnd(')');
    var count = long.Parse(hex[1..^1], System.Globalization.NumberStyles.HexNumber);
    var direction = hex.Last();

    for (int i = 0; i < count; i++)
    {
        if (direction == '0')
            y++;
        if (direction == '2')
            y--;
        if (direction == '3')
            x--;
        if (direction == '1')
            x++;
    }
    perimeter += count;

    list.Add((x, y));

}
var n = list.Count;
long sum = 0;

for (int i = 0; i < n - 1; i++)
{
    sum += list[i].Item1 * list[i + 1].Item2 - list[i + 1].Item1 * list[i].Item2;

}

sum = Math.Abs(sum + list[n - 1].Item1 * list[0].Item2 - list[0].Item1 * list[n - 1].Item2) / 2;

Console.WriteLine(sum + perimeter / 2 + 1);
