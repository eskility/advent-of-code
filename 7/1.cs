
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var listOfHands = new List<Hand>();





foreach (var line in lines)
{
    var split = line.Split(" ");
    var cardsData = split[0].ToList();
    var bid = int.Parse(split[1]);
    var cards = new List<Card>();
    foreach (var x in cardsData)
    {
        cards.Add(new Card(x));
    }

    listOfHands.Add(new Hand(cards, bid));
}
var bidsSummed = 0;
var listsorted = new List<Hand>();
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.HighCard).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.OnePair).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.TwoPairs).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.ThreeOfAKind).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.FullHouse).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.FourOfAKind).ToList()));
listsorted.AddRange(SelectionSort(listOfHands.Where(x => x.Strength == Hand.Set.FiveOfAKind).ToList()));

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
            var counter = 0;
            while (hands[j].Cards[counter].Rank == hands[smallestIndex].Cards[counter].Rank)
                counter++;

            if (hands[j].Cards[counter].Rank < hands[smallestIndex].Cards[counter].Rank)
                smallestIndex = j;
        }

        (hands[i], hands[smallestIndex]) = (hands[smallestIndex], hands[i]);
    }
    return hands;
}




class Card(char x)
{
    public int Rank { get; set; } = CardLabelToRank(x);
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


class Hand
{
    public int Rank { get; set; }
    public Set Strength { get; set; }
    public List<Card> Cards { get; set; }
    public int Bid { get; set; }

    public Hand(List<Card> _cards, int _bid)
    {
        Bid = _bid;
        Cards = _cards;
        var countBySet = _cards.GroupBy(x => x.Rank).ToList();

        if (countBySet.Where(x => x.Count() == 5).Any())
        {

            Strength = Set.FiveOfAKind;
        }
        else if (countBySet.Where(x => x.Count() == 4).Any())
        {

            Strength = Set.FourOfAKind;
        }

        else if (countBySet.Where(x => x.Count() == 3).Any())
        {
            {

                if (countBySet.Where(x => x.Count() == 2).Any())
                {
                    Strength = Set.FullHouse;

                }

                else
                    Strength = Set.ThreeOfAKind;
            }
        }
        else if (countBySet.Where(x => x.Count() == 2).Count() == 2)
        {

            Strength = Set.TwoPairs;
        }
        else if (countBySet.Where(x => x.Count() == 2).Any())
        {

            Strength = Set.OnePair;
        }

        else
            Strength = Set.HighCard;
    }

    public enum Set
    {
        FiveOfAKind = 70000, FourOfAKind = 60000, FullHouse = 50000, ThreeOfAKind = 40000, TwoPairs = 30000, OnePair = 20000, HighCard = 10000
    }
}

