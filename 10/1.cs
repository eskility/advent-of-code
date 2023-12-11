//I had the correct code for so long, but didn't figure I needed to divide searched by 2.
//Thanks to https://www.youtube.com/watch?v=r3i3XE9H4uw
var file = File.ReadAllText("input.txt");
var lines = file.Split("\n");
int startRow = 0, startColumn = 0;

var found = false;
for (int r = 0; r < lines.Length; r++)
{
    if (found)
        break;
    for (int c = 0; c < lines[r].Length; c++)
        if (lines[r][c] == 'S')
        {
            startRow = r; startColumn = c;
            found = true;
            break;
        }
}

var searched = new List<(int, int)>();
var queue = new Queue<(int, int)>();
queue.Enqueue((startRow, startColumn));

while (queue.Count > 0)
{
    var node = queue.Dequeue();
    var row = node.Item1;
    var column = node.Item2;

    if (!searched.Contains(node) && row > 0 && row < lines.Length && column > 0 && column < lines[0].Length)
    {
        searched.Add(node);
        if (lines[row][column] == 'S')
        {
            searched.Add(node);
            queue.Enqueue((row - 1, column));
            queue.Enqueue((row + 1, column));
            queue.Enqueue((row, column + 1));
            queue.Enqueue((row, column - 1));

        }
        else
        {
            var c = lines[row][column];
            var newColumn = GetNextNodes(c, (row, column));
            queue.Enqueue(newColumn.Item1);
            queue.Enqueue(newColumn.Item2);


        }
    }

}

((int, int), (int, int)) GetNextNodes(char c, (int, int) current)
{
    var row = current.Item1;
    var column = current.Item2;
    var result = ((0, 0), (0, 0));
    if (c == '|')
    {
        result = ((row + 1, column), (row - 1, column));
    }
    if (c == '-')
    {

        result = ((row, column + 1), (row, column - 1));
    }
    if (c == 'L')
    {

        result = ((row - 1, column), (row, column + 1));
    }
    if (c == 'J')
    {

        result = ((row - 1, column), (row, column - 1));
    }
    if (c == '7')
    {

        result = ((row + 1, column), (row, column - 1));
    }
    if (c == 'F')
    {

        result = ((row + 1, column), (row, column + 1));
    }

    return result;
}

Console.WriteLine(searched.Count / 2);
