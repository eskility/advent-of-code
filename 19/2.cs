var data = File.ReadAllText("input.txt").Split("\n\n");
var workflowdata = data[0].Split("\n");
var partsdata = data[1];
var workflows = new Dictionary<string, Workflow>();
var parts = new List<Part>();
var accepted = new List<Part>();
var rules = new Dictionary<string, Rule>();
var ranges = new Dictionary<string, (int, int)>();
foreach (var line in workflowdata)
{
    var workflow = new Workflow(line.Split("{")[0], [], "A");


    foreach (var x in line.Split("{")[1].Split(","))
    {
        if (x.Contains(">") || x.Contains("<"))
        {


            var name = x[0];
            var op = x[1];
            var number = int.Parse(Regex.Matches(x, @"\d+").Select(x => x.Value).First());
            var target = x.Split(":")[1];
            var rule = new Rule(name, op, number, target);
            workflow.Rules.Add(rule);


        }

        else
        {
            workflow.Rules.Add(new Rule('e', 'e', 0, x.Replace("}", "")));
        }
    }
    workflows.Add(workflow.Id, workflow);
}

var paths = new List<string>();






long sum = 0;

foreach (var rule in workflows["in"].Rules)
{
    Checkpath(workflows[rule.Target].Rules, "in," + rule.Target, workflows);
}



void Checkpath(List<Rule> list, string path, Dictionary<string, Workflow> workflows)
{
    for (int i = 0; i < list.Count; i++)
    {
        if (list[i].Target == "A")
        {
            if (!paths.Contains(path + ":" + i))
                paths.Add(path + ":" + i);

        }
        else if (list[i].Target != "R")
        {
            Checkpath(workflows[list[i].Target].Rules, (path + ",") + workflows[list[i].Target].Id, workflows);
        }

    }
}


var ok = new List<string>();


foreach (var x in paths)
{
    var listX = new List<(int, int)>();
    var listM = new List<(int, int)>();
    var listA = new List<(int, int)>();
    var listS = new List<(int, int)>();



    var z = x.Split(":")[0].Split(",");


    for (int i = 0; i < z.Length; i++)
    {
        for (int v = 0; v < workflows[z[i]].Rules.Count; v++)
        {
            var rule = workflows[z[i]].Rules[v];

            if (rule.Target == "A" && i==z.Length-1&& v.ToString() == x.Split(":")[1])
            {


                var direction = 1;
                var plus = -1;
                if (rule.Operator == '>')
                {
                    plus = 1;
                    direction = 0;
                }


                if (rule.Name == 'x')
                    listX.Add((rule.Number + plus, direction));

                if (rule.Name == 'm')
                    listM.Add((rule.Number + plus, direction));

                if (rule.Name == 'a')
                    listA.Add((rule.Number + plus, direction));

                if (rule.Name == 's')
                    listS.Add((rule.Number + plus, direction));

                break;


            }




            else
            {

                if (i < z.Length - 1 && rule.Target == workflows[z[i + 1]].Id)
                {
                    var direction = 1;
                    var plus = -1;
                    if (rule.Operator == '>')
                    {
                        plus = 1;
                        direction = 0;
                    }


                    if (rule.Name == 'x')
                        listX.Add((rule.Number + plus, direction));

                    if (rule.Name == 'm')
                        listM.Add((rule.Number + plus, direction));

                    if (rule.Name == 'a')
                        listA.Add((rule.Number + plus, direction));

                    if (rule.Name == 's')
                        listS.Add((rule.Number + plus, direction));

                    break;
                }
                else
                {
                    var direction = 0;
                    var plus = 0;
                    if (rule.Operator == '>')
                    {
                        plus = 0;

                        direction = 1;
                    }


                    if (rule.Name == 'x')
                        listX.Add((rule.Number + plus, direction));

                    if (rule.Name == 'm')
                        listM.Add((rule.Number + plus, direction));

                    if (rule.Name == 'a')
                        listA.Add((rule.Number + plus, direction));

                    if (rule.Name == 's')
                        listS.Add((rule.Number + plus, direction));


                }
            }
        }


    }




    int xLow = 1, mLow = 1, aLow = 1, sLow = 1;
    if (listX.Where(x => x.Item2 == 0).Any())
    {
        xLow = listX.Where(x => x.Item2 == 0).Select(x => x.Item1).Max();
    }
    if (listM.Where(x => x.Item2 == 0).Any())
        mLow = listM.Where(x => x.Item2 == 0).Select(x => x.Item1).Max();
    if (listA.Where(x => x.Item2 == 0).Any())
        aLow = listA.Where(x => x.Item2 == 0).Select(x => x.Item1).Max();
    if (listS.Where(x => x.Item2 == 0).Any())
        sLow = listS.Where(x => x.Item2 == 0).Select(x => x.Item1).Max();

    int xHigh = 4000, mHigh = 4000, aHigh = 4000, sHigh = 4000;
    if (listX.Where(x => x.Item2 == 1).Any())
    {
        xHigh = listX.Where(x => x.Item2 == 1).Select(x => x.Item1).Min();
    }

    if (listM.Where(x => x.Item2 == 1).Any())
        mHigh = listM.Where(x => x.Item2 == 1).Select(x => x.Item1).Min();
    if (listA.Where(x => x.Item2 == 1).Any())
        aHigh = listA.Where(x => x.Item2 == 1).Select(x => x.Item1).Min();
    if (listS.Where(x => x.Item2 == 1).Any())
        sHigh = listS.Where(x => x.Item2 == 1).Select(x => x.Item1).Min();





    long a = xHigh - xLow + 1;
    long b = mHigh - mLow + 1;
    long c = aHigh - aLow + 1;
    long d = sHigh - sLow + 1;

    sum += a * b * c * d;
    Console.WriteLine(x + " valid is x>={0} x<={1} m>={2} m<={3} a>={4} a<={5} s>={6} s<={7}", xLow, xHigh, mLow, mHigh, aLow, aHigh, sLow, sHigh);




}
Console.WriteLine(sum);









class Part(int _x, int _m, int _a, int _s)
{
    public int X { get; set; } = _x;
    public int M { get; set; } = _m;
    public int A { get; set; } = _a;
    public int S { get; set; } = _s;
}

class Workflow(string _id, List<Rule> _conditions, string _endRule)
{
    public string Id { get; set; } = _id;
    public List<Rule> Rules { get; set; } = _conditions;
}

class Rule(char _name, char _operator, int _number, string _target)
{
    public char Name { get; set; } = _name;
    public char Operator { get; set; } = _operator;
    public int Number { get; set; } = _number;

    public string Path { get; set; } = "";
    public string Target { get; set; } = _target;

}
