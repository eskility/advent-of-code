var lines = File.ReadAllLines("input.txt");
var listOfNumbers = new List<int>();

foreach (var line in lines)
{
    var first = line.Where(i => char.IsDigit(i)).First();
    var second = line.Where(i => char.IsDigit(i)).Last();
    listOfNumbers.Add(int.Parse(first.ToString() + second.ToString()));
}

Console.Write(listOfNumbers.Sum());

