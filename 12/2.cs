using System.Text;
using System.Text.RegularExpressions;

var data = File.ReadAllText("input.txt").Split("\n");
var springs = new List<(string, int[])>();

foreach (var line in data)
{
    var s = new StringBuilder();
    var n = new StringBuilder(); ;
    var list = new List<int>();
    for (int i = 0; i < 5; i++)
    {

        s.Append(line.Split(" ")[0]);
        list.AddRange(Regex.Matches(line.Split(" ")[1], @"\d+").Select(x => int.Parse(x.Value)).ToList());
        if (i != 4)
        {
            s.Append('?');
            n.Append(',');
        }
    }
    var sb = new StringBuilder();
    springs.Add((s.ToString(), list.ToArray()));

}



long sum = 0;

foreach (var spring in springs)
{
    var cache = new Dictionary<(string, string), long>();
    sum += count(spring.Item1, spring.Item2, cache);
}

Console.WriteLine(sum);
static long count(string cfg, int[] nums, Dictionary<(string, string), long> cache)
{
    if (cfg == "")
    {
        if (nums.Length == 0)
            return 1;
        else
            return 0;
    }


    if (nums.Length == 0)
        if (cfg.Contains('#'))
            return 0;
        else
        {
            return 1;
        }




    if (cache.ContainsKey((cfg, string.Join("", nums))))
        return cache[(cfg, string.Join("", nums))];

    long result = 0;


    if (cfg[0] == '.' || cfg[0] == '?')
        result += count(cfg[1..], nums, cache);




    if (cfg[0] == '#' || cfg[0] == '?')
    {
        if (nums[0] <= cfg.Length && !cfg.Substring(0, nums[0]).Contains(".") && (nums[0] == cfg.Length || cfg[nums[0]] != '#'))
        {

            result += count(cfg.Substring(Math.Min(nums[0] + 1, cfg.Length)), nums.Skip(1).ToArray(), cache);
        }
    }

    cache.Add((cfg, string.Join("", nums)), result);
    return result;

}

