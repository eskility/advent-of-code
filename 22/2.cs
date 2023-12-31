//Based on HyperNeutrino's answer
var data = File.ReadAllText("input.txt").Split("\n");
List<int[]> bricks = [];

foreach (var line in data)
{
    bricks.Add(Array.ConvertAll(line.ToString().Replace("~", ",").Split(","), x => int.Parse(x)));
}

bricks = [.. bricks.OrderBy(x => x[2])];

foreach (var brick in bricks)
{
    var maximumZ = 1;
    var index = bricks.IndexOf(brick);
    for (int i = 0; i < index; i++)
        if (Overlapse(brick, bricks[i]))
        {
            maximumZ = Math.Max(maximumZ, bricks[i][5] + 1);
        }

    brick[5] -= brick[2] - maximumZ;
    brick[2] = maximumZ;
}

bricks = [.. bricks.OrderBy(x => x[2])];

Dictionary<int, List<int>> k_supports_v = [];
Dictionary<int, List<int>> v_supports_k = [];
for (int i = 0; i < bricks.Count; i++)
{
    k_supports_v.Add(i, []);
    v_supports_k.Add(i, []);
}

foreach (var upper in bricks)
{
    var upperindex = bricks.IndexOf(upper);
    foreach (var lower in bricks[..upperindex])
    {
        var lowerIndex = bricks.IndexOf(lower);
        if (Overlapse(lower, upper) && upper[2] == lower[5] + 1)
        {
            k_supports_v[lowerIndex].Add(upperindex);
            v_supports_k[upperindex].Add(lowerIndex);
        }
    }
}

var total = 0;
Queue<int> queue = [];

for (int i = 0; i < bricks.Count; i++)
{
    foreach (var j in k_supports_v[i])
        if (v_supports_k[j].Count == 1)
            queue.Enqueue(j);

    var falling = queue.ToHashSet();
    falling.Add(i);

    while (queue.Count > 0)
    {
        var j = queue.Dequeue();
        foreach (var k in k_supports_v[j].Except(falling))
        {
            if (v_supports_k[k].ToHashSet().IsSubsetOf(falling))
            {
                queue.Enqueue(k);
                falling.Add(k);
            }
        }
    }
    total += falling.Count - 1;
}


Console.WriteLine(total);

static bool Overlapse(int[] a, int[] b)
{
    return Math.Max(a[0], b[0]) <= Math.Min(a[3], b[3]) && Math.Max(a[1], b[1]) <= Math.Min(a[4], b[4]);
}
