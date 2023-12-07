var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var time = long.Parse(lines[0].Split(":")[1].Replace(" ", "")); 
var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", "")); 
var race = new Race(time, distance);
var raceBoat = new Boat();

for (long i = 0; i < race.Duration; i++)
{
    raceBoat.Button(i);
    raceBoat.Race(race);
}

Console.Write(race.WaysToBeatTheRecord);
class Race(long _time, long _distance)
{
    public long RecordDistance { get; set; } = _distance;
    public long Duration { get; set; } = _time;
    public long WaysToBeatTheRecord { get; set; }
}
class Boat()
{
    public long Speed { get; set; }
    public long ButtonHeld { get; set; }
    public long DistanceCovered { get; set; }

    public void Button(long duration)
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