using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace complianceengine.Models
{
    [DataContract(Name = "Field")]
    public class Field
    {
        [DataMember(Name = "Number")]
        public int Number {get;set;}
        [DataMember(Name = "Start")]
        public int Start {get;set;}
        [DataMember(Name = "Length")]
        public int Length {get;set;}
    }

}