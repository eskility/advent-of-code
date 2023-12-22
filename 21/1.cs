var data = File.ReadAllText("input.txt").Split("\n");

var startIndex = (0, 0);
for (int row = 0; row < data.Length; row++)
{
    for (int column = 0; column < data.Length; column++)
    {
        if (data[row][column] == 'S')
            startIndex = (row, column);
    }
}

Queue<(int, int)> queue = [];
HashSet<(int, int)> searched = [];
HashSet<(int, int)> valid = [];

queue.Enqueue(startIndex);
var steps = 0;
while (queue.Count > 0 && steps <= 64)
{
    var queuesize = queue.Count;

    for (int i = 0; i < queuesize; i++)
    {
        var element = queue.Dequeue();
        if (!searched.Contains(element))
        {
            if (steps % 2 == 0)
                valid.Add(element);

            searched.Add(element);

            var directions = new List<(int, int)>
            {
                (element.Item1-1,element.Item2), (element.Item1+1,element.Item2),
                (element.Item1,element.Item2+1), (element.Item1,element.Item2-1)
            };

            foreach (var direction in directions)
            {
                if (direction.Item1 >= 0 && direction.Item2 >= 0 && direction.Item1 < data.Length && direction.Item2 < data[0].Length)
                    if (data[direction.Item1][direction.Item2] == '.' || data[direction.Item1][direction.Item2] == 'S')
                    {
                        queue.Enqueue(direction);
                    }
            }
        }
    }
    steps++;
}

Console.WriteLine(valid.Count);
