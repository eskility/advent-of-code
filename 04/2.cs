using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
var elfCards = new Dictionary<int, ScratchCard>();
var lotteryCards = new Dictionary<int, ScratchCard>();

foreach (var line in file)
{
    var gameId = int.Parse(Regex.Match(line, @"\d+").Value);
    var lineWithoutGameHeader = line[(line.LastIndexOf(':') + 1)..];
    var numbers = lineWithoutGameHeader.Split("|");
    var elfNumbers = Regex.Matches(numbers[0], @"\d+").Select(x => int.Parse(x.Value)).ToList();
    var lotteryNumbers = Regex.Matches(numbers[1], @"\d+").Select(x => int.Parse(x.Value)).ToList();
    var elfCard = new ScratchCard(elfNumbers, gameId, 1);
    var lotteryCard = new ScratchCard(lotteryNumbers, gameId, 1);
    elfCards.Add(gameId, elfCard);
    lotteryCards.Add(gameId, lotteryCard);
}
foreach (var card in elfCards)
{
    var lotteryNumbers = lotteryCards[card.Key].LotteryNumbers.Intersect(card.Value.LotteryNumbers).ToList();
    for (int i = 1; i <= lotteryNumbers.Count; i++)
    {
        elfCards[card.Key + i].Copies += elfCards[card.Key].Copies;
    }
}
Console.WriteLine(elfCards.Sum(x => x.Value.Copies));

class ScratchCard(List<int> _lotteryNumbers, int _gameId, int _copies)
{
    public int GameId { get; set; } = _gameId;
    public List<int> LotteryNumbers { get; set; } = _lotteryNumbers;
    public int Copies { get; set; } = _copies;
}
