//Djiekstra modified.Learned from https://www.youtube.com/watch?v=2pDSooPLLkI
using System.Linq.Expressions;

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

    var hl = node.Item1;
    var r = node.Item2;
    var c = node.Item3;
    var dr = node.Item4;
    var dc = node.Item5;
    var n = node.Item6;

    if (r == table.GetLength(0) - 1 && c == table.GetLength(1) - 1 && n >= 4)
    {
        Console.WriteLine(hl);
        break;
    }


    if (seen.Contains((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6)))
        continue;

    seen.Add((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6));

    if (n < 10 && (dr, dc) != (0, 0))
    {
        var nr = r + dr;
        var nc = c + dc;
        if (0 <= nr && nr < table.GetLength(0) && 0 <= nc && nc < table.GetLength(1))
            pq.Enqueue((hl + table[nr, nc], nr, nc, dr, dc, n + 1), hl + table[nr, nc]);
    }

    var directions = new List<(int, int)>();

    if (n >= 4 || (dr, dc) == (0, 0))
    {
        directions.Add((0, 1));
        directions.Add((1, 0));
        directions.Add((0, -1));
        directions.Add((-1, 0));


        foreach (var dir in directions)
        {
            if (dir != (dr, dc) && dir != (-dr, -dc))
            {
                var nr = r + dir.Item1;
                var nc = c + dir.Item2;
                if (0 <= nr && nr < table.GetLength(0) && 0 <= nc && nc < table.GetLength(1))
                    pq.Enqueue((hl + table[nr, nc], nr, nc, dir.Item1, dir.Item2, 1), hl + table[nr, nc]);
            }
        }
    }

}

