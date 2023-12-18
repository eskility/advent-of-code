var data = File.ReadAllText("input.txt").Split("\n");

int x = 0, y = 0;
List<(int, int)> list = [];
var perimeter = 0;

foreach (var line in data)
{
    var dig = line.Split(" ");
    var count = int.Parse(dig[1]);

    for (int i = 0; i < count; i++)
    {
        if (dig[0] == "R")
            y++;
        if (dig[0] == "L")
            y--;
        if (dig[0] == "U")
            x--;
        if (dig[0] == "D")
            x++;
    }
    perimeter += count;

    list.Add((x, y));

}
var n = list.Count;
var sum = 0;

for (int i = 0; i < n - 1; i++)
{
    sum += list[i].Item1 * list[i + 1].Item2 - list[i + 1].Item1 * list[i].Item2;

}

sum = Math.Abs(sum + list[n - 1].Item1 * list[0].Item2 - list[0].Item1 * list[n - 1].Item2) / 2;

Console.WriteLine(sum + perimeter / 2 + 1);
