using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace complianceengine.Models
{
    [DataContract(Name = "Ruleset")]
    public class Ruleset
    {
        [DataMember(Name = "Rules", IsRequired = true)]
        public List<Rule> Rules {get;set;}
        [DataMember(Name = "Exceptions", IsRequired = true)]
        public List<Excptn> Exceptions {get;set;}

        public static Ruleset ReturnRuleset(string json)
        {
            Ruleset ru = Activator.CreateInstance<Ruleset>();
            DataContractJsonSerializer js = new DataContractJsonSerializer(ru.GetType());
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            ru = (Ruleset)js.ReadObject(ms);
            ms.Dispose();
            return ru;
        }

        public static Ruleset parse(string filename)
        {
            string json;
            using (StreamReader r = new StreamReader(filename))
            {
                json = r.ReadToEnd();
            }
            Ruleset rules = Activator.CreateInstance<Ruleset>();
            DataContractJsonSerializer js = new DataContractJsonSerializer(rules.GetType());
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            rules = (Ruleset)js.ReadObject(ms);

            ms.Dispose();

            return rules;
        }
    }
}