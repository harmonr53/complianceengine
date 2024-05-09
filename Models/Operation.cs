using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace complianceengine.Models
{
    [DataContract(Name = "Operation")]
    public class Operation
    {
        [DataMember(Name = "Field")]
        public Field Field {get;set;}
        [DataMember(Name = "Op")]
        public string Op {get;set;}
        [DataMember(Name = "Val")]
        public List<string> Val {get;set;}
    }

}