using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
double totalPoints = 0;

foreach (var line in file)
{
    var lineWithoutGameHeader = line[(line.LastIndexOf(':') + 1)..];
    var numbers = lineWithoutGameHeader.Split("|");
    var elfNumbers = Regex.Matches(numbers[0], @"\d+").Select(x => x.Value).ToList();
    var lotteryNumbers = Regex.Matches(numbers[1], @"\d+").Select(x => x.Value).ToList();
    var matches = lotteryNumbers.Where(x => elfNumbers.Contains(x)).ToList();
    var previous = 0;
    if (matches.Count > 0)
    {
        previous = 1;
        for (int i = 1; i < matches.Count; i++)
            previous *= 2;
    }
    totalPoints += previous;

}

Console.WriteLine(totalPoints);

