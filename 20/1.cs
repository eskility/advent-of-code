using System.Runtime;
using System.Security.Cryptography.X509Certificates;

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

var queue = new Queue<Pulse>();
var sumLow = 0;
var sumHigh = 0;

foreach (var m in modules.Where(x => x.Value is Conjuction))
{
    foreach (var x in modules)
    {
        if (x.Value.Destinations.Contains(m.Value.Name))
        {
            Conjuction z = (Conjuction)m.Value;
            z.Memory.Add(x.Value.Name, false);
        }
    }
}

for (int i = 0; i < 1000; i++)
{


    queue.Enqueue(new Pulse(modules["broadcaster"], modules["broadcaster"], false));

    while (queue.Count > 0)
    {

        var pulse = queue.Dequeue();



        if (pulse.HighPulse)
        { sumHigh++; }
        else
        {
            sumLow++;
        }


        if (pulse.Target != null)
        {
            var newPulses = pulse.Target.ReceivePulse(pulse.HighPulse, pulse.Sender, modules);
            foreach (var p in newPulses)
            {
                queue.Enqueue(p);
            }
        }

    }
}
Console.WriteLine(sumLow * sumHigh);




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
                {
                    list.Add(new Pulse(this, null, false));
                }
            }
        }
        else
        {

            foreach (var receiver in Destinations)
            {
                if (modules.ContainsKey(receiver))
                    list.Add(new Pulse(this, modules[receiver], true));
                else
                {
                    list.Add(new Pulse(this, null, true));
                }
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
                {
                    list.Add(new Pulse(this, null, true));
                }
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
                {
                    list.Add(new Pulse(this, null, false));
                }
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
            {
                list.Add(new Pulse(this, null, highPulse));
            }
        }
        return list;
    }

}


