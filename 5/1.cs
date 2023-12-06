using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");

var blocks = file.Split("\n\n");

var seeds = Regex.Matches(blocks[0].Split(":")[1], @"\d+").Select(x => long.Parse(x.Value)).ToList();

for (int i = 1; i < blocks.Length; i++)
{
    var maps = new List<Map>();
    var lines = blocks[i].Split("\n");
    for (int line = 1; line < lines.Length; line++)
    {
        var numbers = Regex.Matches(lines[line], @"\d+").Select(x => long.Parse(x.Value)).ToList();
        maps.Add(new Map(numbers[0], numbers[1], numbers[2]));

    }

    var matches = new List<long>();
    foreach (var x in seeds)
    {
        var resultFound = false;
        foreach (var map in maps)
        {
            if (map.Source <= x && x < map.Source + map.Range)
            {
                resultFound = true;
                matches.Add(x - map.Source + map.Destination);
                break;
            }
        }
        if (!resultFound)

            matches.Add(x);
    }
    seeds = matches;
}

Console.WriteLine(seeds.Min());

class Map(long _destination, long _source, long _range)
{
    public long Destination { get; set; } = _destination;
    public long Source { get; set; } = _source;
    public long Range { get; set; } = _range;
}
