using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace complianceengine.Models
{
    [DataContract(Name = "Rule")]
    public class Rule
    {
        [DataMember(Name = "Rulename")]
        public string Name {get;set;}
        [DataMember(Name = "Focus")]
        public string Focus {get;set;}
        [DataMember(Name = "Type")]
        public string Type {get;set;}
        [DataMember(Name = "Mode")]
        public string Mode {get;set;}
        [DataMember(Name = "Modeexpiry")]
        public string Modeexpiry {get;set;}
        [DataMember(Name = "Select")]
        public List<Operation> Select {get;set;}
        [DataMember(Name = "Pass")]
        public List<Operation> Pass {get;set;}

        public static Rule ReturnRule(string json)
        {
            Rule ru = Activator.CreateInstance<Rule>();
            DataContractJsonSerializer js = new DataContractJsonSerializer(ru.GetType());
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            ru = (Rule)js.ReadObject(ms);
            ms.Dispose();
            return ru;
        }

        public static List<Rule> getRulesThatMatch(string rectype, Ruleset rules)
        {
            List<Rule> r = new List<Rule>();
            foreach(Rule ru in rules.Rules)
            {
                if(ru.Type == rectype)
                {
                    r.Add(ru);
                }
            }
            return r;
        }
    }
}