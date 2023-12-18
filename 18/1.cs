//Djiekstra modified.Learned from https://www.youtube.com/watch?v=2pDSooPLLkI
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

var data = File.ReadAllText("input.txt").Split("\n");

var maxDepth = 0;
var maxWidth = 0;
var depth = 0;
var width = 0;

foreach (var line in data)
{
    var dig = line.Split(" ");
    if (dig[0] == "R")
        width += int.Parse(dig[1]);
    if (dig[0] == "L")
    {
        maxWidth = Math.Max(maxWidth, width);
        width = 0;
    }

    if (dig[0] == "D")
        depth += int.Parse(dig[1]);
    if (dig[0] == "U")
    {
        maxDepth = Math.Max(maxDepth, depth);
        depth = 0;
    }
}

Console.WriteLine(" " + maxDepth + " & " + maxWidth);


char[,] table = new char[maxDepth + 1, maxWidth + 1];

var x = 0;
var y = 0;
foreach (var line in data)
{
    var dig = line.Split(" ");
    var direction = dig[0];
    var n = int.Parse(dig[1]);

    for (int i = 0; i < n; i++)
    {
        if (direction == "D")
        {
            if (x < table.GetLength(0))
                table[x, y] = '#';
            if (x < table.GetLength(0)-1)
                x++;


        }
        if (direction == "U" )
        {
            if (x > 0)
                table[x, y] = '#';
            if (x > 0)
                x--;

        }
        if (direction == "R" )
        {
            if (y < table.GetLength(1))
                table[x, y] = '#';
            if (y < table.GetLength(1)-1)
                y++;

        }
        if (direction == "L" && y > 0)
        {
             if (y>0)
                table[x, y] = '#';
            if (y>0)
                y--;

        }
    }

}
var sum = 0;

for (int row = 0; row < table.GetLength(0); row++)
{
    var start = 10;
    var end = 0;
    for (int column = 0; column < table.GetLength(1); column++)
    {
        if (table[row, column] == '#')
        {
            start = Math.Min(column, start);
            end = Math.Max(column, start);
        }
    }
    sum += (end - start) + 1;
}
Console.WriteLine("asd");
