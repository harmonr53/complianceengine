using complianceengine.Models;
using complianceengine.Util;

namespace complianceengine;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        var startTime = DateTime.UtcNow;
        var pathToFiles = Environment.CurrentDirectory;
        // Console.Write(pathToFiles);
        var pathToIn = Path.Combine(pathToFiles, "output.csv");
        var pathToRuleIn = Path.Combine(pathToFiles, "rulein.txt");
        var pathToOut = Path.Combine(pathToFiles, "out.csv");
        Ruleset rs = Ruleset.parse(pathToRuleIn);
        List<Models.Rule> ru = new List<Rule>();
        List<Outcome> outcomes = new List<Outcome>();
        List<string> outcomess = new List<string>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(pathToIn))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    // Console.WriteLine(line);
                    Verdict v = new Verdict();
                    Dictionary <int,string> rec = new Dictionary<int, string>();
                    rec = EngineUtils.SplitQualifiedToDictionary(line);
                    var rectype = EngineUtils.ReturnRecType(rec);
                    ru = Models.Rule.getRulesThatMatch(rectype, rs);
                    string k = rec[0];
                    if(k == "35"){
                        bool test = true;
                    }
                    v = TestRecord.Test(rec,ru);
                    // Console.WriteLine("This is count of outcomes: " + v.outcomes.Count);
                    foreach(Outcome o in v.outcomes){
                        // string id = o.CompObsID.ToString();
                        // string rn = o.ruleName;
                        // string outc = o.outcome;
                        // List<string> fc = new List<string>();
                        // fc.Add(id);
                        // fc.Add(rn);
                        // fc.Add(outc);
                        // fc.Add(line);
                        // string outcl = String.Join(",", fc);
                        outcomes.Add(o);
                        // outcomess.Add(outcl);
                    }
                    // using(StreamWriter sw = new StreamWriter(pathToOut, true)){
                    //     foreach(Outcome o in v.outcomes){
                    //         string s = Outcome.returnOutcomeAsString(o);
                    //         sw.WriteLine(s);
                    //     }
                    // }
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        try{
            using(StreamWriter sw = new StreamWriter(pathToOut)){
                foreach(Outcome o in outcomes){
                    string s = Outcome.returnOutcomeAsString(o);
                    sw.WriteLine(s);
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }

        List<string> observations = new List<string>();
        List<string> output = new List<string>();

        using(StreamReader sr = new StreamReader(pathToIn))
        {
            string line;
            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                observations.Add(line);
            }
        }

        using(StreamReader sr = new StreamReader(pathToOut))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                output.Add(line);
            }
        }

        using(StreamWriter sw = new StreamWriter(Path.Combine(pathToFiles, "finalout.csv")))
        {

            foreach(string s1 in observations)
            {
                bool hasmatches = false;
                string s1x = returnID(s1);
                foreach(string s2 in output)
                {
                    string s2x = returnID(s2);
                    if(s1x == s2x){
                        hasmatches = true;
                        List<string> temp = new List<string>();
                        temp.Add(s2);
                        temp.Add(s1);
                        string x = string.Join(",", temp);
                        sw.WriteLine(x);
                    }
                }
                if(!hasmatches){
                    List<string> temp = new List<string>();
                    string y = s1x + ",no_rule,no_rule,no_rule";
                    temp.Add(y);
                    temp.Add(s1);
                    string x = string.Join(",", temp);
                    sw.WriteLine(x);
                }
            }
        }
        var endtime = DateTime.UtcNow;
        TimeSpan duration = endtime - startTime;
        Console.WriteLine($"The operation took {duration.TotalSeconds} seconds.");
    }
    public static string returnID(string s)
    {
        string[] split = s.Split(new char[]{','}, 2);
        return split[0];
    }
}
