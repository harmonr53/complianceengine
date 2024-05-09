using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace complianceengine.Models
{
    [DataContract(Name = "Exceptions")]
    public class Excptn
    {
        [DataMember(Name = "Id")]
        public int id {get;set;}
        [DataMember(Name = "Rule")]
        public string Rule {get;set;}
        [DataMember(Name = "Expires")]
        public string Expires {get;set;}
        [DataMember(Name = "Reason")]
        public string Reason {get;set;}
        [DataMember(Name = "Identify", IsRequired = true)]
        public List<Operation> Identify {get;set;}
    }
}