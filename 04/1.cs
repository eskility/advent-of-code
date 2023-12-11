using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
int totalPoints = 0;

foreach (var line in file)
{
    var lineWithoutGameHeader = line[(line.LastIndexOf(':') + 1)..];
    var numbers = lineWithoutGameHeader.Split("|");
    var elfNumbers = Regex.Matches(numbers[0], @"\d+").Select(x => x.Value).ToList();
    var lotteryNumbers = Regex.Matches(numbers[1], @"\d+").Select(x => x.Value).ToList();
    var matches = lotteryNumbers.Intersect(elfNumbers).Count();
    totalPoints += (int)Math.Pow(2,matches-1);
}
Console.WriteLine(totalPoints);

