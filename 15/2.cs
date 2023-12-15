var listOfHashes = File.ReadAllText("input.txt").Split(",");
var boxes = new Box[256];

for (int i = 0; i < 256; i++)
{
    boxes[i] = new Box(i);
}

foreach (var x in listOfHashes)
{
    string label;
    if (x.Contains('='))
        label = x.Split('=')[0];
    else
        label = x.Split('-')[0];

    var box = boxes[StringToHash(label)];

    if (x.Contains('-'))
    {
        box.RemoveLens(label);
    }
    else
    {
        var lens = x.Split('=');
        var strength = int.Parse(lens[1]);
        box.UpdateOrAddLens(label, strength);
    }
}

var sum = 0;
foreach (var box in boxes)
{
    for (int i = 0; i < box.Lenses.Count; i++)
    {
        sum += (box.Id + 1) * (i + 1) * box.Lenses[i].Strength;
    }
}

Console.WriteLine(sum);

static int StringToHash(string s)
{
    var sum = 0;
    for (int i = 0; i < s.Length; i++)
    {
        sum += s[i];
        sum *= 17;
        sum %= 256;
    }
    return sum;
}

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
    public void UpdateOrAddLens(string id, int strength)
    {
        var updated = false;

        for (int i = 0; i < Lenses.Count; i++)

            if (Lenses[i].Id == id)
            {
                Lenses[i].Strength = strength;
                updated = true;
            }

        if (!updated)
            Lenses.Add(new Lens(id, strength));
    }
}
