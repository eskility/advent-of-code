var listOfHashes = File.ReadAllText("input.txt").Split(",");
var sum = 0;

foreach (var x in listOfHashes)
{
    var hashSum = 0;
    for (int i = 0; i < x.Length; i++)
    {
        hashSum += x[i];
        hashSum *= 17;
        hashSum %= 256;
    }
    sum += hashSum;
}

Console.WriteLine(sum);
