var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
var directions = lines[0].Where(char.IsLetter).ToList();
var graph = new Dictionary<string, List<string>>();

foreach (var line in lines.Skip(2))
{
    var nodeData = line.Replace(" ", "").Replace("(", "").Replace(")", "").Split("=");
    var node = nodeData[0];
    graph.Add(node, [.. nodeData[1].Split(",")]);
}
var aNodes = graph.Keys.Where(x => x.EndsWith('A'));

var listOfPaths = new List<long>();
foreach (var node in aNodes)
{
    GraphSearcher(node, 0, directions, 0, listOfPaths);
}
Console.WriteLine(Lcm(listOfPaths.ToArray()));

void GraphSearcher(string node, int counter, List<char> directions, int directionCounter, List<long> listOfPaths)
{
    if (node.EndsWith('Z'))
        listOfPaths.Add(counter);
    else
    {
        if (directionCounter == directions.Count)
            directionCounter = 0;

        if (directions[directionCounter] == 'L')
            GraphSearcher(graph[node][0], counter + 1, directions, directionCounter + 1, listOfPaths);
        else
            GraphSearcher(graph[node][1], counter + 1, directions, directionCounter + 1, listOfPaths);
    }
}

//gcd and lcd code from https://www.w3resource.com/csharp-exercises/math/csharp-math-exercise-20.php
static long Gcd(long n1, long n2)
{
    if (n2 == 0)
    {
        return n1;
    }
    else
    {
        return Gcd(n2, n1 % n2);
    }
}

static long Lcm(long[] numbers)
{
    return numbers.Aggregate((S, val) => S * val / Gcd(S, val));
}
