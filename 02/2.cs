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
        if (pick.Contains("red"))
            red = Math.Max(red, int.Parse(Regex.Match(pick, @"\d+[ ]red").Value.Replace(" red", "")));
        if (pick.Contains("blue"))
            blue = Math.Max(blue, int.Parse(Regex.Match(pick, @"\d+[ ]blue").Value.Replace(" blue", "")));
        if (pick.Contains("green"))
            green = Math.Max(green, int.Parse(Regex.Match(pick, @"\d+[ ]green").Value.Replace(" green", "")));
    }
    minimumSet += red * blue * green;
}
Console.WriteLine(minimumSet);
