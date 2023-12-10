//I had the correct code for so long, but didn't figure I needed to divide searched by 2.
//Thanks to https://www.youtube.com/watch?v=r3i3XE9H4uw
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Text;

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

var visited = new List<(int, int)>();
var queue = new Queue<(int, int)>();
queue.Enqueue((startRow, startColumn));
visited.Add((startRow, startColumn));



List<char> maybe_s = ['|', '-', 'J', 'L', '7', 'F'];

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
  && !visited.Contains((row - 1, column)))
    {
        queue.Enqueue((row - 1, column));
        visited.Add((row - 1, column));
        if (lines[row][column] == 'S')
            maybe_s = maybe_s.Intersect(upWards).ToList();
    }
    if (row < lines.Length - 1 && downwards.Contains(lines[row][column]) && downwardsReceiver.Contains(lines[row + 1][column])
  && !visited.Contains((row + 1, column)))
    {
        queue.Enqueue((row + 1, column));
        visited.Add((row + 1, column));
        if (lines[row][column] == 'S')
            maybe_s = maybe_s.Intersect(downwards).ToList();
    }
    if (column > 0 && left.Contains(lines[row][column]) && leftReceiver.Contains(lines[row][column - 1])
      && !visited.Contains((row, column - 1)))
    {
        queue.Enqueue((row, column - 1));
        visited.Add((row, column - 1));
        if (lines[row][column] == 'S')
            maybe_s = maybe_s.Intersect(left).ToList();
    }
    if (column < lines[row].Length - 1 && right.Contains(lines[row][column]) && rightReceiver.Contains(lines[row][column + 1])
      && !visited.Contains((row, column + 1)))
    {
        queue.Enqueue((row, column + 1));
        visited.Add((row, column + 1));
        if (lines[row][column] == 'S')
            maybe_s = maybe_s.Intersect(right).ToList();
    }
}



//from https://www.youtube.com/watch?v=zhmzPQwgPg0 
//based on Point in polygon - rayCasting algorithm
static int CountInvs(int i, int j, List<(int, int)> visited, string[] lines)
{
    string line = lines[i];
    int count = 0;
    for (int k = 0; k < j; k++)
    {
        if (!visited.Contains((i, k)))
        {
            continue;
        }
        if (line[k] == 'J' || line[k] == 'L' || line[k] == '|')
            count += 1;


    }
    return count;
}

int ans = 0;
for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];
    for (int j = 0; j < line.Length; j++)
    {
        if (!visited.Contains((i, j)))
        {
            int invs = CountInvs(i, j, visited, lines);
            if (invs > 0)
            {
                if (invs % 2 == 1)
                {
                    ans += 1;
                }
            }
        }
    }
}

Console.WriteLine(ans);



