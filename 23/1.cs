var data = File.ReadAllText("input.txt").Split("\n");
List<(int,int)> seen = [];
List<int> depths = [];
Bfs(0,seen,(0,1),data,depths);

void Bfs(int depth, List<(int,int)> seen,(int,int) location, string[] data,List<int> depths)
{
    var directions = new List<(int,int)>();
    
    if(  data[location.Item1][location.Item2] !='#')
    {
        depths.Add(depth);
              seen.Add(location);


    if( data[location.Item1][location.Item2]=='v')
    {
directions.Add((location.Item1+1, location.Item2));
    }
    else    if( data[location.Item1][location.Item2]=='^')
    {
directions.Add((location.Item1-1, location.Item2));
    }
     else  if( data[location.Item1][location.Item2]=='>')
    {
directions.Add((location.Item1, location.Item2+1));
    }
      else  if( data[location.Item1][location.Item2]=='<')
    {
directions.Add((location.Item1, location.Item2-1));
    }

else{
    directions.Add((location.Item1-1, location.Item2));directions.Add((location.Item1+1, location.Item2));directions.Add((location.Item1, location.Item2+1));directions.Add((location.Item1,location.Item2-1));
}

foreach(var direction in directions)
{
    if(direction.Item1>=0 && direction.Item1<data.Length && direction.Item2 >=0 && direction.Item2<data[0].Length && !seen.Contains(direction))
    {
         if(directions.Count>1)
         Bfs(depth+1,seen.ToList(),direction,data,depths);
         else
         Bfs(depth+1,seen,direction,data,depths);
    } 
}
}
}

Console.WriteLine(depths.Max());
