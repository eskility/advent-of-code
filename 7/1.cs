var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var listOfHands = new List<Hand>();

foreach (var line in lines)
{
    var split = line.Split(" ");
    var cardData = split[0].ToList();
    var bid = int.Parse(split[1]);
    var cards = new List<int>();
    foreach (var x in cardData)
    {
        var cards = Hand.CardLabelToRank(x);
        cards.Add(card);
    }
    listOfHands.Add(new Hand(cards, bid));
}

var bidsSummed = 0;
var listsorted = SelectionSort(listOfHands);
var rank = 1;

foreach (var hand in listsorted)
{
    bidsSummed += hand.Bid * rank;
    rank++;
}

Console.WriteLine(bidsSummed);



static List<Hand> SelectionSort(List<Hand> hands)
{
    int n = hands.Count;
    for (int i = 0; i < n - 1; i++)
    {
        int smallestIndex = i;
        for (int j = i + 1; j < n; j++)
        {
            if (hands[j].Strength == hands[smallestIndex].Strength)
            {
                var counter = 0;
                while (hands[j].Cards[counter] == hands[smallestIndex].Cards[counter])
                    counter++;
                if (hands[j].Cards[counter] < hands[smallestIndex].Cards[counter])
                    smallestIndex = j;
            }
            else
            {
                if (hands[j].Strength < hands[smallestIndex].Strength)
                    smallestIndex = j;
            }
        }
        (hands[i], hands[smallestIndex]) = (hands[smallestIndex], hands[i]);
    }
    return hands;
}
class Hand
{
    public int Rank { get; set; }
    public Set Strength { get; set; }
    public List<int> Cards { get; set; }
    public int Bid { get; set; }

    public Hand(List<int> _cards, int _bid)
    {
        Bid = _bid;
        Cards = _cards;
        var cardArray = new int[15];

        foreach (var card in Cards)
            cardArray[card]++;

        if (cardArray.Max() == 5)
            Strength = Set.FiveOfAKind;

        else if (cardArray.Max() == 4)
            Strength = Set.FourOfAKind;

        else if (cardArray.Max() == 3)
        {
            if (cardArray.Where(i => i == 2).Any())
                Strength = Set.FullHouse;
            else
                Strength = Set.ThreeOfAKind;
        }

        else if (cardArray.Where(i => i == 2).Count() == 2)
            Strength = Set.TwoPairs;

        else if (cardArray.Max() == 2)
            Strength = Set.OnePair;
        else
            Strength = Set.HighCard;
    }
    public enum Set
    {
        HighCard, OnePair, TwoPairs, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind
    }
    public static int CardLabelToRank(char x)
    {
        if (char.IsDigit(x))
            return x - '0';

        else
        {
            if (x == 'A')
                return 14;
            if (x == 'K')
                return 13;
            if (x == 'Q')
                return 12;
            if (x == 'J')
                return 11;
            if (x == 'T')
                return 10;
        }
        return 0;
    }
}
