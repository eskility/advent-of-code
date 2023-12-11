using System.Text.RegularExpressions;

var file = File.ReadLines("input.txt");
var rows = file.ToList();

var sum = 0;
var listOfNumbers = new List<Number>();
var listOfSymbols = new List<Symbol>();

for (int i = 0; i < rows.Count; i++)
{
    var numbers = Regex.Matches(rows[i], @"\d+");
    foreach (Match match in numbers)
    {
        listOfNumbers.Add(new Number(i, int.Parse(match.Value), match.Index, match.Index + match.Length));
    }

    var symbols = Regex.Matches(rows[i], ("[^.A-Za-z0-9 ]"));
    foreach (Match match in symbols)
    {
        listOfSymbols.Add(new Symbol(i, char.Parse(match.Value), match.Index));
    }
}

foreach (var number in listOfNumbers)
{
    var x = 0;
    foreach (var symbol in listOfSymbols)
    {
        if (number.Row == symbol.Row - 1 || number.Row == symbol.Row + 1 || number.Row == symbol.Row)
        {
            if (number.Index - 1 == symbol.Index || number.EndIndex == symbol.Index)
                x = number.Digits;
            else if (symbol.Index <= number.EndIndex && symbol.Index >= number.Index - 1)
                x = number.Digits;
        }
    }
    sum += x;
}
Console.WriteLine(sum);

class Number(int _row, int _digits, int _index, int _endIndex)
{
    public int Row { get; set; } = _row;
    public int Digits { get; set; } = _digits;
    public int Index { get; set; } = _index;
    public int EndIndex { get; set; } = _endIndex;
}
class Symbol(int _row, char _symbolType, int _index)
{
    public int Row { get; set; } = _row;
    public char SymbolType { get; set; } = _symbolType;
    public int Index { get; set; } = _index;
}


