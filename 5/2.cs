//with thanks to https://www.youtube.com/watch?v=NmxHw_bHhGM
using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");
var text = file.Split("\n\n");
var inputs = Regex.Matches(text[0].Split(":")[1], @"\d+").Select(x => long.Parse(x.Value)).ToList();
var seeds = new Queue<(long, long)>();

//loop through all the seeds and create a tuple that represent the seed start to seed end 
//for example the line seeds: 79 14 becomes 79,(79+14)
for (int i = 0; i < inputs.Count; i += 2)
{
    seeds.Enqueue((inputs[i], inputs[i] + inputs[i + 1]));
}

//now we just loop every maps, for example the seed to soil map
foreach (var mapData in text.Skip(1))
{
    var allMaps = mapData.Split("\n");
    var maps = new List<Map>();

    //find all the ranges in the current map and add them
    foreach (var m in allMaps.Skip(1))
    {
        var numbers = Regex.Matches(m, @"\d+").Select(x => long.Parse(x.Value)).ToList();
        maps.Add(new Map(numbers[0], numbers[1], numbers[2]));
    }

    //take each seed and check it against each map
    var matches = new List<(long, long)>();
    while (seeds.Count > 0)
    {
        var seed = seeds.Dequeue();
        //so for the seed example at the start, we again have start=79 and end =93
        long start = seed.Item1, end = seed.Item2;
        var resultFound = false;

        foreach (var map in maps)
        {
            //the start of the overlap will be the highest value between the start of the seed
            // and the souce of the map. For example when we check 79 against the map source 50
            // the intersection of them start at 79.
            var overlapStart = Math.Max(start, map.Source);
            //now we do the same with the end of the overlap. The seed 79 end is 93 and the 
            // map source+range is 98. Thus the overlaps end at 93. We now have our overlap which is
            // 79-93
            var overlapEnd = Math.Min(end, map.Source + map.Range);
            //there is only a match if the overlapStart is lower than the overlapEnd
            if (overlapStart < overlapEnd)
            {
                resultFound = true;
                //add the match of the overlap to the matches. The match check here uses the map to check the currect start value
                // and the map for the correct end value. 
                // For example in the 79 example with the map that is destination 52 and source 50 and range 48.
                // 79 has a destination of 81 since we see that the destination is 2 higher than source. 
                // we calculate this by doing the start of overlap 79 - source 50 + destination 52.
                //The same goes for the overlap end, which is 93. 93-50+52
                //we now know that the overlap was between 81-95.
                matches.Add((overlapStart - map.Source + map.Destination, overlapEnd - map.Source + map.Destination));

                //now if the overlap is more than the start of eed range itself
                // we need to add that portion to the queue of elements
                // to check
                if (overlapStart > start)
                {
                    seeds.Enqueue((start, overlapStart));
                }
                //the same goes for if the end of the seed range itself is higher than the overlap. 
                // Then we need to add that portion to check
                if (end > overlapEnd)
                {
                    seeds.Enqueue((overlapEnd, end));
                }
                //break since we have found our match in the map and don't need to keep searching.
                break;
            }
        }
        //if there was no overlaps at all we add the entire range. This is exactly like part 1
        // where the rule states that if there is no map, you use the same value as the seed itself.
        if (!resultFound)
            matches.Add((start, end));
    }
    //since we now have found the new range to search we clear the queue and do the next map.
    // iow, you move from for example seed to soil, clear the queue then add the ranges found to
    // be used against soil to fertilizer map.
    // fertilizer to the queue 
    seeds.Clear();
    foreach (var x in matches)
        seeds.Enqueue(x);
}

Console.WriteLine(seeds.Min().Item1);

class Map(long _destination, long _source, long _range)
{
    public long Destination { get; set; } = _destination;
    public long Source { get; set; } = _source;
    public long Range { get; set; } = _range;
}
