using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");

var text = file.Split("\n\n");
var inputs = Regex.Matches(text[0].Split(":")[1], @"\d+").Select(x => long.Parse(x.Value)).ToList();

var seeds = new List<(long, long)>();

for (int i = 0; i < inputs.Count; i += 2)
{
    seeds.Add((inputs[i], inputs[i] + inputs[i + 1]));
}

foreach (var mapData in text.Skip(1))
{
    var allMaps = mapData.Split("\n");
    var map = new List<Map>();

    foreach (var m in allMaps.Skip(1))
    {
        var numbers = Regex.Matches(m, @"\d+").Select(x => long.Parse(x.Value)).ToList();
        map.Add(new Map(numbers[0], numbers[1], numbers[2]));
    }
    var matches = new List<(long, long)>();
    while (seeds.Count > 0)
    {

        var x = seeds.Last();
        seeds.RemoveAt(seeds.Count - 1);
        long s = x.Item1, e = x.Item2;
        var resultFound = false;

        foreach (var m in map)
        {

            var overlapStart = Math.Max(s, m.b);
            var overlapEnd = Math.Min(e, m.b + m.c);
            if (overlapStart < overlapEnd)
            {
                resultFound = true;
                matches.Add((overlapStart - m.b + m.a, overlapEnd - m.b + m.a));
                if (overlapStart > s)
                {

                    seeds.Add((s, overlapStart));
                }
                if (e > overlapEnd)
                {

                    seeds.Add((overlapEnd, e));
                }

                break;
            }

        }
        if (!resultFound)
            matches.Add((s, e));
    }
    seeds = matches;
}

Console.WriteLine(seeds.Min());

class Map(long _destination, long _source, long _range)
{
    public long a { get; set; } = _destination;
    public long b { get; set; } = _source;
    public long c { get; set; } = _range;
}
