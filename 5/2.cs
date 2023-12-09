//with thanks to https://www.youtube.com/watch?v=NmxHw_bHhGM
using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");
var text = file.Split("\n\n");
var inputs = Regex.Matches(text[0].Split(":")[1], @"\d+").Select(x => long.Parse(x.Value)).ToList();
var seeds = new Queue<(long, long)>();

for (int i = 0; i < inputs.Count; i += 2)
{
    seeds.Enqueue((inputs[i], inputs[i] + inputs[i + 1]));
}

foreach (var mapData in text.Skip(1))
{
    var allMaps = mapData.Split("\n");
    var map = new Queue<Map>();

    foreach (var m in allMaps.Skip(1))
    {
        var numbers = Regex.Matches(m, @"\d+").Select(x => long.Parse(x.Value)).ToList();
        map.Enqueue(new Map(numbers[0], numbers[1], numbers[2]));
    }

    var matches = new Queue<(long, long)>();
    while (seeds.Count > 0)
    {
        var x = seeds.Dequeue();
        long s = x.Item1, e = x.Item2;
        var resultFound = false;

        foreach (var m in map)
        {
            var overlapStart = Math.Max(s, m.Source);
            var overlapEnd = Math.Min(e, m.Source + m.Range);
            if (overlapStart < overlapEnd)
            {
                resultFound = true;
                matches.Enqueue((overlapStart - m.Source + m.Destination, overlapEnd - m.Source + m.Destination));
                if (overlapStart > s)
                {
                    seeds.Enqueue((s, overlapStart));
                }
                if (e > overlapEnd)
                {
                    seeds.Enqueue((overlapEnd, e));
                }
                break;
            }
        }
        if (!resultFound)
            matches.Enqueue((s, e));
    }
    seeds = matches;
}

Console.WriteLine(seeds.Min().Item1);

class Map(long _destination, long _source, long _range)
{
    public long Destination { get; set; } = _destination;
    public long Source { get; set; } = _source;
    public long Range { get; set; } = _range;
}
