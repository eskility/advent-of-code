using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
int redLimit = 12, greenLimit = 13, blueLimit = 14;
var gameIdSum = 0;
foreach (var line in file)
{
    var gameId  = int.Parse(Regex.Match(line, @"\d+").Value);
    var picks = line.Split(";");

    foreach (var pick in picks)
    {
        var colors = pick.Split(",");
        int blue = 0, red = 0, green = 0;
        foreach (var c in colors)
        {
            if (c.Contains("red"))
            {
                red = int.Parse(Regex.Match(c, @"\d+[ ]red").Value.Replace(" red", ""));
            }

            if (c.Contains("blue"))
            {
                blue = int.Parse(Regex.Match(c, @"\d+[ ]blue").Value.Replace(" blue", ""));
            }

            if (c.Contains("green"))
            {
                green = int.Parse(Regex.Match(c, @"\d+[ ]green").Value.Replace(" green", ""));
            }
        }

        if (blue > blueLimit || red > redLimit || green > greenLimit)
        {
            gameId = 0;

        }

    }
    gameIdSum += gameId;
}
Console.WriteLine(gameIdSum);

