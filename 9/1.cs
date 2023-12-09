using System.Text.RegularExpressions;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var result = 0;
foreach (var line in lines)
{
    var numbers = new List<List<int>>
    {
        Regex.Matches(line, @"-?\d+").Select(x=> int.Parse(x.Value)).ToList()
    };
    numbers = FindDifferences(numbers);
    result += Extrapolate(numbers);
}
Console.Write(result);

static int Extrapolate(List<List<int>> differences)
{
    differences.Last().Add(0);
    for (int i = differences.Count - 2; i >= 0; i--)
    {
        differences[i].Add(differences[i + 1].Last() + differences[i].Last());
    }
    return differences[0].Last();
}

static List<List<int>> FindDifferences(List<List<int>> differences)
{
    var lastList = differences.Last();
    if (lastList.Where(i => i == 0).Count() == lastList.Count)
        return differences;
    var currentDifferences = new List<int>();
    for (int i = 0; i < lastList.Count - 1; i++)
    {
        currentDifferences.Add(lastList[i + 1] - lastList[i]);
    }
    differences.Add(currentDifferences);
    return FindDifferences(differences);
}
