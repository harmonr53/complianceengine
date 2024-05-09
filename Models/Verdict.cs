namespace complianceengine.Models
{
    public class Verdict
    {
        public string PassFail {get;set;}
        public string tests {get;set;}
        public List<Outcome> outcomes {get;set;}

    }
}