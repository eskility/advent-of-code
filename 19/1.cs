using System.Security;
using System.Text.RegularExpressions;

var data = File.ReadAllText("input.txt").Split("\n\n");
var workflowdata = data[0].Split("\n");
var partsdata = data[1];
var workflows = new Dictionary<string, Workflow>();
var parts = new List<Part>();
var accepted = new List<Part>();

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


foreach (var x in partsdata.Split("\n"))
{
    var numbers = Regex.Matches(x, @"\d+").Select(x => x.Value).ToList();
    var part = new Part(int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]), int.Parse(numbers[3]));
    parts.Add(part);
}

long sum = 0;
foreach (var part in parts)
{
    var rule = workflows["in"];
    while (rule != null)
    {
        bool allCheck = false;
        for (int i = 0; i < rule.Rules.Count; i++)
        {
            if (rule.Rules[i].Number == 0 && rule.Rules[i].Name == 'e')
            {
                if (rule.Rules[i].Target == "A")
                {
                    accepted.Add(part);
                    rule = null;
                    break;
                }
                else if (rule.Rules[i].Target == "R")
                {
                    rule = null;
                    break;
                }
                else
                {
                    rule = workflows[rule.Rules[i].Target];
                    break;
                }
            }
            else
            {
                if (rule.Rules[i].Name == 'a')
                {

                    var op = rule.Rules[i].Operator;

                    if (rule.Rules[i].Operator == '<')
                    {
                        if (part.A < rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }

                    if (rule.Rules[i].Operator == '>')
                    {
                        if (part.A > rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                }
                if (rule.Rules[i].Name == 'm')
                {
                    if (rule.Rules[i].Operator == '<')
                    {
                        if (part.M < rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                    if (rule.Rules[i].Operator == '>')
                    {
                        if (part.M > rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                }
                if (rule.Rules[i].Name == 's')
                {
                    if (rule.Rules[i].Operator == '<')
                    {
                        if (part.S < rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                    if (rule.Rules[i].Operator == '>')
                    {
                        if (part.S > rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                }
                if (rule.Rules[i].Name == 'x')
                {
                    if (rule.Rules[i].Operator == '<')
                    {
                        if (part.X < rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                    if (rule.Rules[i].Operator == '>')
                    {
                        if (part.X >rule.Rules[i].Number)
                        {
                            if (rule.Rules[i].Target == "A")
                            {
                                accepted.Add(part);
                                rule = null;
                                break;
                            }
                            else if (rule.Rules[i].Target == "R")
                            {
                                rule = null;
                                break;
                            }
                            else
                            {
                                rule = workflows[rule.Rules[i].Target];
                                break;
                            }
                        }


                    }
                }

            }
        }

    }

}



foreach (var part in accepted)
{
    sum += (part.X + part.M + part.A + part.S);
}

Console.Write(sum);
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

    public string Target { get; set; } = _target;

}
