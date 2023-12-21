//another LCM AOC....
//Learned the assumptions from https://www.youtube.com/watch?v=lxm6i21O83k
var data = File.ReadAllText("input.txt").Split("\n");
var modules = new Dictionary<string, IModule>();
foreach (var line in data)
{
    IModule module = null;
    string name = line.Split(" ")[0];
    var destinations = line.Split("->")[1].Replace(" ", "").Split(",");
    if (line.Contains('%'))
        module = new FlipFlop(name.Split("%")[1]);

    if (line.Contains('&'))
        module = new Conjuction(name.Split("&")[1]);

    if (line.Contains("broadcaster"))
        module = new BroadCaster(name);

    module.Destinations = [.. destinations];

    modules.Add(module.Name, module);
}

foreach (Conjuction conjuction in modules.Where(x => x.Value is Conjuction).Select(x => x.Value))
{
    foreach (var module in modules.Where(x => x.Value.Destinations.Contains(conjuction.Name)))
    {
        conjuction.Memory.Add(module.Value.Name, false);
    }
}

var queue = new Queue<Pulse>();
int counter = 0;

var feed = modules.Where(x => x.Value.Destinations.Contains("rx")).First().Value;

var cycleLengths = new Dictionary<string, int>();
var seen = new Dictionary<string, int>();
foreach (var module in modules.Where(x => x.Value.Destinations.Contains(feed.Name)))
{
    seen.Add(module.Value.Name, 0);
}

var found = false;
while (!found)
{
    counter++;
    queue.Enqueue(new Pulse(modules["broadcaster"], modules["broadcaster"], false));
    while (queue.Count > 0)
    {
        var pulse = queue.Dequeue();

        if (pulse.Target != null)
        {
            if (pulse.Target.Name == feed.Name && pulse.HighPulse)
            {
                if (!seen.ContainsKey(pulse.Sender.Name))
                {
                    seen.Add(pulse.Sender.Name, 0);
                }
                seen[pulse.Sender.Name]++;
                if (!cycleLengths.ContainsKey(pulse.Sender.Name))
                {
                    cycleLengths.Add(pulse.Sender.Name, counter);
                }
                       
                if (seen.All(x => x.Value > 0))
                {
                    Console.WriteLine(Lcm(cycleLengths.Values.Select(x => long.Parse(x.ToString())).ToArray()));
                    found = true;
                }
            }
            var newPulses = pulse.Target.ReceivePulse(pulse.HighPulse, pulse.Sender, modules);
            foreach (var p in newPulses)
            {
                queue.Enqueue(p);
            }
        }
    }
}


//gcd and lcd code from https://www.w3resource.com/csharp-exercises/math/csharp-math-exercise-20.php
static long Gcd(long n1, long n2)
{
    if (n2 == 0)
    {
        return n1;
    }
    else
    {
        return Gcd(n2, n1 % n2);
    }
}

static long Lcm(long[] numbers)
{
    return numbers.Aggregate((S, val) => S * val / Gcd(S, val));
}

class Pulse(IModule _sender, IModule _target, bool _highPulse)
{
    public IModule Target { get; set; } = _target;
    public IModule Sender { get; set; } = _sender;
    public bool HighPulse { get; set; } = _highPulse;
}

interface IModule
{
    public string Name { get; set; }
    public List<string> Destinations { get; set; }
    List<Pulse> ReceivePulse(bool highPulse, IModule sender, Dictionary<string, IModule> modules);
}


class Conjuction(string _name) : IModule
{
    public string Name { get; set; } = _name;
    public List<string> Destinations { get; set; } = [];
    public Dictionary<string, bool> Memory = [];
    List<Pulse> IModule.ReceivePulse(bool highPulse, IModule sender, Dictionary<string, IModule> modules)
    {
        var list = new List<Pulse>();

        Memory[sender.Name] = highPulse;

        if (Memory.Where(x => x.Value == true).Count() == Memory.Count)
        {
            foreach (var receiver in Destinations)
            {
                if (modules.ContainsKey(receiver))
                    list.Add(new Pulse(this, modules[receiver], false));
                else
                    list.Add(new Pulse(this, null, false));

            }
        }
        else
        {

            foreach (var receiver in Destinations)
            {
                if (modules.ContainsKey(receiver))
                    list.Add(new Pulse(this, modules[receiver], true));
                else
                    list.Add(new Pulse(this, null, true));

            }

        }

        return list;
    }

}

class FlipFlop(string _name) : IModule
{
    public string Name { get; set; } = _name;
    public bool State { get; set; } = false;
    public List<string> Destinations { get; set; } = [];

    List<Pulse> IModule.ReceivePulse(bool highPulse, IModule sender, Dictionary<string, IModule> modules)
    {
        var list = new List<Pulse>();
        if (State == false && !highPulse)
        {
            State = true;

            foreach (var receiver in Destinations)
            {
                if (modules.ContainsKey(receiver))
                    list.Add(new Pulse(this, modules[receiver], true));
                else
                    list.Add(new Pulse(this, null, true));

            }
        }
        else if (State == true && !highPulse)
        {
            State = false;

            foreach (var receiver in Destinations)
            {
                if (modules.ContainsKey(receiver))
                    list.Add(new Pulse(this, modules[receiver], false));
                else
                    list.Add(new Pulse(this, null, false));

            }
        }
        return list;
    }


}
class BroadCaster(string _name) : IModule
{
    public string Name { get; set; } = _name;
    public bool State { get; set; } = false;
    public List<string> Destinations { get; set; } = [];

    List<Pulse> IModule.ReceivePulse(bool highPulse, IModule sender, Dictionary<string, IModule> modules)
    {
        var list = new List<Pulse>();
        foreach (var receiver in Destinations)
        {
            if (modules.ContainsKey(receiver))
                list.Add(new Pulse(this, modules[receiver], highPulse));
            else
                list.Add(new Pulse(this, null, highPulse));
        }
        return list;
    }
}

