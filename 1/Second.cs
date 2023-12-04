using System.Text;

var lines = File.ReadAllLines("input.txt");
var listOfNumbers = new List<int>();
var textToNumbers = new Dictionary<string, string>
{
   { "one","1"},
   { "two","2"},
   { "three","3"},
   { "four","4"},
   { "five","5"},
   { "six","6"},
   { "seven","7"},
   { "eight","8"},
   { "nine","9"}
};

foreach (var x in lines)
{
    var sb = new StringBuilder();
    var list = new List<string>();
    var firstNumber = "";
    var secondnumber = "";
    for (int i = 0; i < x.Length; i++)
    {
        if (char.IsDigit(x[i]))
        {
            firstNumber = x[i].ToString();
            break;
        }
        else
        {
            sb.Append(x[i]);
            if (!textToNumbers.Keys.Where(i => i.StartsWith(sb.ToString())).Any())
            {
                if (sb.Length > 1)
                {
                    i-=1;
                }
                sb = sb.Clear();

            }
            if (textToNumbers.ContainsKey(sb.ToString()))
            {
                firstNumber = textToNumbers[sb.ToString()];
                sb.Clear();
                break;

            }
        }
    }
    sb = sb.Clear();
    for (int i = x.Length-1; i >= 0; i--)
    {
        if (char.IsDigit(x[i]))
        {
            secondnumber = x[i].ToString();
            break;
        }
        else
        {
            sb.Insert(0,x[i]);
            if (!textToNumbers.Keys.Where(i => i.EndsWith(sb.ToString())).Any())
            {
                if (sb.Length > 1)
                {
                   i+=1;
                }
                sb = sb.Clear();

            }
            if (textToNumbers.ContainsKey(sb.ToString()))
            {
                secondnumber = textToNumbers[sb.ToString()];
                sb.Clear();
                break;

            }
        }
    }
   
    listOfNumbers.Add(int.Parse(firstNumber + secondnumber));
}
Console.Write(listOfNumbers.Sum());






//Solution with using a smart string replace trick is actually so much easier:

var lines = File.ReadAllLines("input.txt");
var listOfNumbers = new List<int>();
var textToNumbers = new Dictionary<string, string>
{
   { "one","1"},
   { "two","2"},
   { "three","3"},
   { "four","4"},
   { "five","5"},
   { "six","6"},
   { "seven","7"},
   { "eight","8"},
   { "nine","9"}
};

foreach (var l in lines)
{
    var line = l;
    foreach(var x in textToNumbers)
    {
       line= line.Replace(x.Key,x.Key+x.Value+x.Key);
    }
    var first = line.Where(char.IsDigit).First();
    var second = line.Where(char.IsDigit).Last();
    listOfNumbers.Add(int.Parse(first.ToString() + second.ToString()));
}

Console.Write(listOfNumbers.Sum());
