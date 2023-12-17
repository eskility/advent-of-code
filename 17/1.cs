//Djiekstra modified.Learned from https://www.youtube.com/watch?v=2pDSooPLLkI
var data = File.ReadAllText("input.txt").Split("\n");
int[,] table = new int[data.Length, data[0].Length];

for (int row = 0; row < data.Length; row++)
{
    for (int column = 0; column < data[row].Length; column++)
    {
        table[row, column] = data[row][column] - '0';
    }
}

var seen = new HashSet<(int, int, int, int, int)>();
var pq = new PriorityQueue<(int, int, int, int, int, int), int>();
pq.Enqueue((0, 0, 0, 0, 0, 0), 0);

while (pq.Count > 0)
{
    var node = pq.Dequeue();
    var heatLoss = node.Item1;
    var row = node.Item2;
    var column = node.Item3;
    var directionRow = node.Item4;
    var directionColumn = node.Item5;
    var steps = node.Item6;

    if (row == table.GetLength(0) - 1 && column == table.GetLength(1) - 1)
    {
        Console.WriteLine(heatLoss);
        break;
    }

    if (seen.Contains((row, column, directionRow, directionColumn, steps)))
        continue;

    seen.Add((row, column, directionRow, directionColumn, steps));

    if (steps < 3 && (directionRow, directionColumn) != (0, 0))
    {
        var nr = row + directionRow;
        var nc = column + directionColumn;
        if (0 <= nr && nr < table.GetLength(0) && 0 <= nc && nc < table.GetLength(1))
            pq.Enqueue((heatLoss + table[nr, nc], nr, nc, directionRow, directionColumn, steps + 1), heatLoss + table[nr, nc]);
    }

    var directions = new List<(int, int)>
    {
        (1, 0),
        (0, 1),
        (0, -1),
        (-1, 0)
    };

    foreach (var dir in directions)
    {
        if (dir != (directionRow, directionColumn) && dir != (-directionRow, -directionColumn))
        {
            var nr = row + dir.Item1;
            var nc = column + dir.Item2;
            if (0 <= nr && nr < table.GetLength(0) && 0 <= nc && nc < table.GetLength(1))
                pq.Enqueue((heatLoss + table[nr, nc], nr, nc, dir.Item1, dir.Item2, 1), heatLoss + table[nr, nc]);
        }
    }
}

