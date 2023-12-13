using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var file = File.ReadAllText("input.txt");
var input = file.Split("\n\n");

var matchedRows2 = new List<int>();
var matchedColumns = new List<int>();
for (int i = 0; i < input.Length; i++)
{
    var columndata = input[i].Split("\n");

    var j = columndata[0].Length - 1;
    var listX = new List<string>();
    var listY = new List<string>();
    for (int column = 0; column < columndata[0].Length && j > i; column++)
    {
        var columnX = new StringBuilder();
        var columnY = new StringBuilder();


        for (int row = 0; row < columndata.Length; row++)
        {
            columnX.Append(columndata[row][column]);
            columnY.Append(columndata[row][j]);

        }
        listX.Add(columnX.ToString());
        listY.Add(columnY.ToString());
        if (listX.Last() == listX.Last())
        {
            var mirror = true;

            for (int z = 0; z <=Math.Min(column, columndata[0].Length - 1 - column); z++)
            {
                if (listX[z] != listY[z])
                    mirror = false;
            }



            if (mirror)
                matchedColumns.Add(column);
        }

        j--;
    }




    var rowdata = input[i].Split("\n");
    for (int row = 0; row < rowdata.Length - 1; row++)
    {
        if (rowdata[row] == rowdata[row + 1])
        {
            var mirror = true;
            int x = row, y = row + 1; ;
            while (x >= 0 && y <= rowdata.Length - 1)
            {
                if (rowdata[x] != rowdata[y])
                {
                    mirror = false;
                    break;
                }
                x--; y++;
            }
            if (mirror)
                matchedRows2.Add((row));
        }
    }
}








var sum = 0;

for (int i = 0; i < matchedRows2.Count; i++)
{
    sum += ((matchedRows2[i] + 1) * 100);
}
for (int i = 0; i < matchedColumns.Count; i++)
{
    sum += matchedColumns[i] + 1;
}

Console.WriteLine(sum);