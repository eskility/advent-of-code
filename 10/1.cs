//I had the correct code for so long, but didn't figure I needed to divide searched by 2.
//Thanks to https://www.youtube.com/watch?v=r3i3XE9H4uw
using System.Data;

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
searched.Add((startRow,startColumn));

List<char> upWards = ['S', 'J', 'L', '|'];
List<char> upwardsReceiver = ['|', '7', 'F'];
List<char> downwards = ['S', '|', '7', 'F'];
List<char> downwardsReceiver = ['|', 'J', 'L'];
List<char> left = ['S', '-', 'J', '7'];
List<char> leftReceiver = ['-', 'L', 'F'];
List<char> right = ['S', '-', 'L', 'F'];
List<char> rightReceiver = ['-', 'J', '7'];

while (queue.Count > 0)
{
    var node = queue.Dequeue();
    var row = node.Item1;
    var column = node.Item2;

    if (row != 0 && upWards.Contains(lines[row][column]) && upwardsReceiver.Contains(lines[row - 1][column])
  && !searched.Contains((row - 1, column)))
    {
        queue.Enqueue((row - 1, column));
        searched.Add((row - 1, column));
    }
    if (row < lines.Length - 1 && downwards.Contains(lines[row][column]) && downwardsReceiver.Contains(lines[row + 1][column])
  && !searched.Contains((row + 1, column)))
    {
        queue.Enqueue((row + 1, column));
        searched.Add((row + 1, column));
    }
    if (column > 0 && left.Contains(lines[row][column]) && leftReceiver.Contains(lines[row][column - 1])
      && !searched.Contains((row, column - 1)))
    {
        queue.Enqueue((row, column - 1));
        searched.Add((row, column - 1));
    }
    if (column < lines[row].Length - 1 && right.Contains(lines[row][column]) && rightReceiver.Contains(lines[row][column + 1])
      && !searched.Contains((row, column + 1)))
    {
        queue.Enqueue((row, column + 1));
        searched.Add((row, column + 1));
    }
}

Console.WriteLine(searched.Count / 2);
