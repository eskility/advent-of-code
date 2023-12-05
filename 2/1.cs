using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
int redLimit = 12, greenLimit = 13, blueLimit = 14;
var gameIdSum = 0;
foreach (var line in file)
{
    var gameId = int.Parse(Regex.Match(Regex.Match(line, "(?<=Game\\s)[0-9]+").Value, @"\d+").Value);
    var picks = line.Split(";");

    foreach (var pick in picks)
    {
        int blue = 0, red = 0, green = 0;

        var dice = pick.Split(",");
        blue = int.TryParse(Regex.Match(Regex.Match(pick, "(\\d+)[^\\d]+blue").Value, @"\d+").Value, out blue) ? blue: default(int);
        red = int.TryParse(Regex.Match(Regex.Match(pick, "(\\d+)[^\\d]+red").Value, @"\d+").Value, out red)? red: default(int);;
        green = int.TryParse(Regex.Match(Regex.Match(pick, "(\\d+)[^\\d]+green").Value, @"\d+").Value, out green)? green: default(int);;

        if (blue > blueLimit || red > redLimit || green > greenLimit)
        {
            gameId = 0;

        }

    }
    gameIdSum += gameId;
}

Console.WriteLine(gameIdSum);

