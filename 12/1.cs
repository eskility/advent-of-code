using System.Globalization;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

var data = File.ReadAllText("input.txt").Split("\n");
var springs = new List<(string, List<int>)>();

foreach (var line in data)
{
    springs.Add((line.Split(" ")[0], Regex.Matches(line.Split(" ")[1], @"\d+").Select(x => int.Parse(x.Value)).ToList()));

}

var sum = 0;

foreach (var spring in springs)
{

    sum += GetCombinations(spring.Item1, spring.Item2.ToArray());
}

Console.WriteLine(sum);

static int GetCombinations(string springs, int[] nums)
{
    var total = 0;

    if (springs.Length == 0)
    {
        if (nums.Length == 0)
        {
            return 1;
        }
        else
        return 0;
    }
    if (nums.Length == 0)
    {
        if (springs.Contains('#'))
        {
            return 0;
        }
        else
            return 1;
    }
    if (springs.Length < nums.Sum() + nums.Length - 1)
    {
        return 0;
    }
    if (springs[0] == '.' || springs[0] == '?')
    {
        var s = new StringBuilder();
        foreach (var x in springs.Skip(1))
            s.Append(x);
        total += GetCombinations(s.ToString(), nums);
    }
    int n = nums[0];
    if (springs[0] == '#' || springs[0] == '?')
        if (!springs.Take(n).ToList().Contains('.') )
        if(springs.Length == n || springs[n] == '.' || springs[n] == '?')
        {
            var s = new StringBuilder();
            var x = springs.Skip(n + 1);
            foreach (var y in x)
                s.Append(y);
            total += GetCombinations(s.ToString(), nums.Skip(1).ToArray());
        }
    return total;
}
