using System.Runtime.CompilerServices;

var listOfHashes = File.ReadAllText("input.txt").Split(",");

var boxes = new Dictionary<int, Box>();
for (int i = 0; i < 256; i++)
    boxes.Add(i, new Box(i));
foreach (var x in listOfHashes)
{
    var label = "";
    if (x.Contains('='))
        label = x.Split('=')[0];
    else
        label = x.Split('-')[0];
    var boxId = 0;
    for (int i = 0; i < label.Length; i++)
    {
        boxId += label[i];
        boxId *= 17;
        boxId %= 256;
    }
    var box = boxes[boxId];
    if (x.Contains('-'))
    {
        var lens = x.Split('-');
        box.RemoveLens(lens[0]);

    }
    else
    {
        var lens = x.Split('=');
        if (box.Lenses.Select(x => x.Id).Contains(lens[0]))
        {
            box.Lenses.Where(x => x.Id == lens[0]).First().Strength = int.Parse(lens[1]);
        }
        else
        {

            var z = new Lens(lens[0], int.Parse(lens[1]));
            box.Lenses.Add(z);
        }

    }
}


var sum = 0;

foreach (var x in boxes)
{
    var box = x.Value;
    var i = 1;
    foreach (var lens in box.Lenses)
    {
        sum += ((box.Id + 1) * i) * lens.Strength;
        i++;
    }

}
Console.WriteLine(sum);
class Lens(string _id, int _strength)
{
    public string Id { get; set; } = _id;
    public int Strength { get; set; } = _strength;
}
class Box(int _id)
{
    public int Id { get; set; } = _id;
    public List<Lens> Lenses { get; set; } = [];

    public void RemoveLens(string id)
    {
        for (int i = 0; i < Lenses.Count; i++)
        {
            if (Lenses[i].Id == id)
                Lenses.RemoveAt(i);

        }
    }
}
