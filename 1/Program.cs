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
    if (x == "qeightwo2xjvfkfiveone")
    { }
    var sb = new StringBuilder();
    var list = new List<string>();
    foreach (var c in x)
    {
        if (char.IsDigit(c))
        {
            list.Add(c.ToString());
            sb = sb.Clear();
        }
        else
        {
            sb.Append(c);
            if (!textToNumbers.Keys.Where(i => i.StartsWith(sb.ToString())).Any())
                sb.Clear();

            if (textToNumbers.ContainsKey(sb.ToString()))
            {
                list.Add(textToNumbers[sb.ToString()]);
                sb = sb.Clear();
            }
        }
    }
    listOfNumbers.Add(int.Parse(list.First()+list.Last()));
}

Console.Write(listOfNumbers.Sum());