var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var listOfHands = new List<Hand>();

foreach (var line in lines)
{
    var split = line.Split(" ");
    var cardsData = split[0].ToList();
    var bid = int.Parse(split[1]);
    var cards = new int[15];
    var cardsOrder = new List<int>();
    foreach (var x in cardsData)
    {
        var card = Hand.CardLabelToRank(x);
        cards[card]++;
        cardsOrder.Add(card);
    }
    listOfHands.Add(new Hand(cards, bid, cardsOrder));
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
                while (hands[j].CardsOrder[counter] == hands[smallestIndex].CardsOrder[counter])
                    counter++;
                if (hands[j].CardsOrder[counter] < hands[smallestIndex].CardsOrder[counter])
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
    public int[] Cards { get; set; }
    public List<int> CardsOrder { get; set; }
    public int Bid { get; set; }

    public Hand(int[] _cards, int _bid, List<int> _cardsOrder)
    {
        Bid = _bid;
        Cards = _cards;
        CardsOrder = _cardsOrder;
        var jokers = Cards[1];

        var currentBest = Set.HighCard;
        for (int i = 2; i < Cards.Length; i++)
        {
            if (Cards[i] + jokers == 5)
            {
                if (Set.FiveOfAKind > currentBest)
                    currentBest = Set.FiveOfAKind;
            }

            else if (Cards[i] + jokers == 4)
            {
                if (Set.FourOfAKind > currentBest)
                    currentBest = Set.FourOfAKind;
            }

            else if (Cards[i] + jokers == 3)
            {

                if (Set.ThreeOfAKind > currentBest)

                    currentBest = Set.ThreeOfAKind;
                var jokersRemaining = jokers - (3 - Cards[i]);
                for (int j = 2; j < Cards.Length; j++)
                {
                    if (j != 1 && i != j && Cards[j] + jokersRemaining == 2)
                    {
                        if (Set.FullHouse > currentBest)
                            currentBest = Set.FullHouse;
                    }
                }
            }
            else if (Cards[i] + jokers == 2)
            {
                if (Set.OnePair > currentBest)
                    currentBest = Set.OnePair;
                var jokersRemaining = jokers - (2 - Cards[i]);

                for (int j = 2; j < Cards.Length; j++)
                {
                    if (j != 1 && i != j && Cards[j] + jokersRemaining == 2)
                    {
                        if (Set.TwoPairs > currentBest)
                            currentBest = Set.TwoPairs;
                    }
                }
            }
        }
        Strength = currentBest;
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
                return 1;
            if (x == 'T')
                return 10;
        }
        return 0;
    }
}
