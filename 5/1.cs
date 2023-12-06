using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");

var listOfSeeds = new List<Seed>();
var seeddict = new Dictionary<long, Seed>();

var currentOperation = "";

foreach (var line in file)
{
    if (line.Contains(':'))
    {
        var operation = line.Split(":");
        currentOperation = operation[0];
    }
    var matches = Regex.Matches(line, @"\d+").Select(x => long.Parse(x.Value)).ToList();

    if (currentOperation == "seeds")
    {
        foreach (var match in matches)
            seeddict.Add(match, new Seed(match));
    }
    if (currentOperation == "seed-to-soil map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];
            foreach (var seed in seeddict.Values)
            {
                var remaining = seed.SeedId - source % sourcerange;
                if (seed.SeedId >= source && seed.SeedId <= sourcerange)
                {
                    seed.SoilId = destination + remaining;
                }
                else
                    seed.SoilId = seed.SeedId;
            }
        }
    }
    if (currentOperation == "soil-to-fertilizer map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.SoilId == 0)
                    seed.SoilId = seed.SeedId;
                var remaining = seed.SoilId - source % sourcerange;
                if (seed.SoilId >= source && seed.SoilId <= sourcerange)
                {
                    seed.FertilizerId = destination + remaining;
                }
            }
        }
    }
    if (currentOperation == "fertilizer-to-water map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.FertilizerId == 0)
                    seed.FertilizerId = seed.SoilId;
                var remaining = seed.FertilizerId - source % sourcerange;
                if (seed.FertilizerId >= source && seed.FertilizerId <= sourcerange)
                {
                    seed.WaterId = destination + remaining;
                }

            }
        }
    }

    if (currentOperation == "water-to-light map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.WaterId == 0)
                    seed.WaterId = seed.FertilizerId;
                var remaining = seed.WaterId - source % sourcerange;
                if (seed.WaterId >= source && seed.WaterId <= sourcerange)
                {
                    seed.LightId = destination + remaining;
                }

            }
        }
    }
    if (currentOperation == "light-to-temperature map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.LightId == 0)
                    seed.LightId = seed.WaterId;
                var remaining = seed.LightId - source % sourcerange;
                if (seed.LightId >= source && seed.LightId <= sourcerange)
                {
                    seed.TemperatureId = destination + remaining;
                }
            }
        }

    }
    if (currentOperation == "temperature-to-humidity map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.TemperatureId == 0)
                    seed.TemperatureId = seed.LightId;
                var remaining = seed.TemperatureId - source % sourcerange;
                if (seed.TemperatureId >= source && seed.TemperatureId <= sourcerange)
                {
                    seed.HumidityId = destination + remaining;
                }
            }
        }
    }
    if (currentOperation == "humidity-to-location map")
    {
        if (matches.Count > 0)
        {
            var destination = matches[0];
            var source = matches[1];
            var sourcerange = source + matches[2];
            var destinationrange = destination + matches[2];

            foreach (var seed in seeddict.Values)
            {
                if (seed.HumidityId == 0)
                    seed.HumidityId = seed.TemperatureId;
                var remaining = seed.HumidityId - source % sourcerange;
                if (seed.HumidityId >= source && seed.HumidityId <= sourcerange)
                {
                    seed.LocationId = destination + remaining;
                }
            }
        }
    }
}
foreach (var seed in seeddict.Values)
{
    if (seed.LocationId == 0)
        seed.LocationId = seed.HumidityId;
}

Console.WriteLine(seeddict.Values.Select(x => x.LocationId).Min());




class Seed(long _seedid)
{
    public long SeedId { get; set; } = _seedid;
    public long FertilizerId { get; set; }
    public long SoilId { get; set; }
    public long WaterId { get; set; }
    public long LightId { get; set; }
    public long TemperatureId { get; set; }
    public long HumidityId { get; set; }
    public long LocationId { get; set; }

}
