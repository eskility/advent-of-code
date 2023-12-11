using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");

var text = file.Split("\n\n");
var intialSeeds = Regex.Matches(text[0].Split(":")[1], @"\d+").Select(x => long.Parse(x.Value)).ToList();

foreach (var mapData in text.Skip(1))
{
    var allMaps = mapData.Split("\n");
    var map = new List<Map>();

    foreach (var m in allMaps.Skip(1))
    {
        var numbers = Regex.Matches(m, @"\d+").Select(x => long.Parse(x.Value)).ToList();
        map.Add(new Map(numbers[0], numbers[1], numbers[2]));
    }
    var matches = new List<long>();
    foreach (var x in intialSeeds)
    {
        var resultFound = false;
        foreach (var m in map)
        {
            if (m.Source <= x && x < m.Source + m.Range)
            {
                resultFound = true;
                matches.Add(x - m.Source + m.Destination);
                break;
            }
        }
        if (!resultFound)

            matches.Add(x);
    }
    intialSeeds = matches;
}

Console.WriteLine(intialSeeds.Min());

class Map(long _destination, long _source, long _range)
{
    public long Destination { get; set; } = _destination;
    public long Source { get; set; } = _source;
    public long Range { get; set; } = _range;
}
