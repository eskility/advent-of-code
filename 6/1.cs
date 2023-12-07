using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var time = Regex.Matches(lines[0], @"\d+").Select(x => int.Parse(x.Value)).ToList();
var distance = Regex.Matches(lines[1], @"\d+").Select(x => int.Parse(x.Value)).ToList();
var races = new Race[time.Count];
var raceBoat = new Boat();

for (int i = 0; i < races.Length; i++)
{
    races[i] = new Race(time[i], distance[i]);
}

foreach (var race in races)
{
    for (int i = 0; i < race.Duration; i++)
    {
        raceBoat.Button(i);
        raceBoat.Race(race);
    }
}

Console.Write(races.Select(x => x.WaysToBeatTheRecord).Aggregate((x, y) => x * y));

class Race(int _time, int _distance)
{
    public int RecordDistance { get; set; } = _distance;
    public int Duration { get; set; } = _time;
    public int WaysToBeatTheRecord { get; set; }
}
class Boat()
{
    public int Speed { get; set; }
    public int ButtonHeld { get; set; }
    public int DistanceCovered { get; set; }

    public void Button(int duration)
    {
        ButtonHeld = duration;
        Speed = duration;
    }
    public void Race(Race race)
    {
        DistanceCovered = (race.Duration - ButtonHeld) * Speed;
        if (DistanceCovered > race.RecordDistance)
            race.WaysToBeatTheRecord++;
    }

}