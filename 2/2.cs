using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
var minimumSet = 0;
foreach (var line in file)
{
    var gameId = int.Parse(Regex.Match(line, @"\d+").Value);
    var picks = line.Split(";");
    int blue = 0, red = 0, green = 0;
    foreach (var pick in picks)
    {
        var colors = pick.Split(",");

        foreach (var c in colors)
        {
            if (c.Contains("red"))
            {
                red = Math.Max(red, int.Parse(Regex.Match(c, @"\d+[ ]red").Value.Replace(" red", "")));
            }

            if (c.Contains("blue"))
            {
                blue = Math.Max(blue, int.Parse(Regex.Match(c, @"\d+[ ]blue").Value.Replace(" blue", "")));
            }

            if (c.Contains("green"))
            {
                green = Math.Max(green, int.Parse(Regex.Match(c, @"\d+[ ]green").Value.Replace(" green", "")));
            }
        }
    }
    minimumSet += red * blue * green;
}
Console.WriteLine(minimumSet);
