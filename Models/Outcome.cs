namespace complianceengine.Models
{
    public class Outcome
    {
        public int CompObsID {get;set;}
        public string outcome {get;set;}
        public string ruleName{get;set;}
        public int ExceptionId{get;set;}
        public bool warn {get;set;}
        public bool exc {get;set;}

        public static string returnOutcomeAsString(Outcome oc){
            string s = "";
            string idx = oc.CompObsID.ToString();
            string warn = oc.warn.ToString();
            List<string> ret = new List<string>();
            ret.Add(idx);
            ret.Add(oc.ruleName);
            ret.Add(oc.outcome);
            ret.Add(warn);
            s = string.Join(",", ret);
            return s;
        }
    }
}