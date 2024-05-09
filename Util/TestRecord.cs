using System.Data;
using complianceengine.Models;

namespace complianceengine.Util
{
    public class TestRecord
    {
        public static Verdict Test(Dictionary<int,string> r, List<Models.Rule> rules)
        {
            Verdict v = new Verdict();
            List<Outcome> outcomes = new List<Outcome>();
            foreach(Models.Rule rule in rules)
            {
                Outcome oc = new Outcome();
                oc.CompObsID = Convert.ToInt32(r[0]);
                oc.ruleName = rule.Name;
                oc.outcome = "not selected";
                if(rule.Select != null){
                    if(!Test(r, rule.Select))
                        continue;
                }   

                if(Test(r, rule.Pass))
                    oc.outcome = "pass";
                else
                    oc.outcome = "fail";
                    

                if(rule.Mode == "WARN")
                    oc.warn = true;

                if(oc.outcome != "not selected")
                    outcomes.Add(oc);
            }
            if(outcomes.Any(s => s.outcome == "fail"))
                v.PassFail = "fail";
            else
                v.PassFail = "pass";

            v.outcomes = outcomes;
            return v;
        }

        public static bool Test(Dictionary<int,string> r, List<Operation> ops)
        {
            bool pass = false;
            foreach(Operation op in ops)
            {
                string fldVal = returnFieldValue(r, op);
                Operation newOp = changeFieldsToValues(op, r);
                if(fldVal != null){
                    pass = formatAndTest(fldVal, newOp);
                }
                else{

                }
                if(!pass)
                    break;

            }
            return pass;
        }

        public static bool formatAndTest(string val, Operation op){
            bool pass = true;
            string test = op.Op.ToUpper();
            if(test == "EQ" || test == "="){
                foreach(string v in op.Val){
                    string s = replaceNulls(v);
                    if(testEQ(s, val)){
                        pass = true;
                        break;
                    }
                    else  
                        pass = false;
                }
            }
            else if(test == "NE" || test == "/="){
                foreach(string v in op.Val){
                    string s  = replaceNulls(v);
                    if(testNE(s, val))
                        pass = true;
                    else{
                        pass = false;
                        break;
                    }
                }
            }
            else if(test == "CO"){
                foreach(string v in op.Val){
                    if(val.Contains(v.Trim('\''))){
                        pass = true;
                        break;
                    }
                    else
                        pass = false;
                }
            }
            else if(test == "NC"){
                foreach(string v in op.Val){
                    if(val.Contains(v.Trim('\''))){
                        pass = false;
                        break;
                    }
                    else
                        pass = true;
                }
            }
            return pass;
        }

        public static bool testEQ(string testVal, string recVal){
            string x = "";
            if(testVal.StartsWith("'") && testVal.EndsWith("'"))
                x = trueTrim(testVal);
            else
                x = testVal;
            
            if(recVal == x)
                return true;
            else
                return false;
        }

        public static bool testNE(string testVal, string recVal){
            string x = "";
            if(testVal.StartsWith("'") && testVal.EndsWith("'"))
                x = trueTrim(testVal);
            else
                x = testVal;
            
            if(recVal != x)
                return true;
            else
                return false;
        }

        public static string trueTrim(string val){
            if(val != "'"){
                val = val.Trim().Remove(0,1);
                val = val.Remove(val.Length -1, 1);
            }
            return val;
        }
        public static string replaceNulls(string s){
            string t = s;
            string str = "";
            if(t.Trim().ToUpper() != "NULL")
                str = s;

            return str;
        }

        public static Operation changeFieldsToValues(Operation op, Dictionary<int,string> rec)
        {
            if(op.Val[0].Contains("Field") || op.Val[0].Contains("field")){
                Field f = new Field();
                f = returnFld(op.Val[0]); //Return field object representing Fieldnn from val
                Operation o = new Operation();
                o.Field = f;
                var pv = returnFieldValue(rec, o);
                List<string> vallist = new List<string>();
                vallist.Add(pv);
                Operation newOp = new Operation();
                newOp.Field = op.Field;
                newOp.Op = op.Op;
                newOp.Val = vallist;
                return newOp;
            }
            else
                return op;
        }

        public static Field returnFld(string val){
            string[] split = val.Split(new char[] { '(' });
            int fldName = Convert.ToInt32(split[0].Trim(new char[] { 'F', 'i', 'e', 'l', 'd', 'f'}));
            string unformattedFldVal = split[1].Trim(new char[] { ')' });
            string[] split2 = unformattedFldVal.Split(new char[] {','});
            Field f  = new Field();
            f.Number = fldName;
            f.Start = Convert.ToInt32(split2[0]);
            f.Length = Convert.ToInt32(split2[1]);
            return f;
        }

        public static string returnFieldValue(Dictionary<int, string> rec, Operation op)
        {
            var fieldValue = rec[op.Field.Number];
            var processedVal = searchForLength(fieldValue, op.Field);
            return processedVal;
        }

        public static string searchForLength(string fldVal, Field f)
        {
            string s = "";
            if(f.Start != 0){
                if(f.Length != 0){
                    if(f.Start - 1 > fldVal.Length){
                        //Dont do anything
                    }
                    else{
                        if((f.Start -1) + f.Length > fldVal.Length)
                            s = fldVal.Substring(f.Start -1, fldVal.Length - (f.Start -1));
                        else
                            s = fldVal.Substring(f.Start -1, f.Length);
                    }
                }
                else{
                    if(f.Start > fldVal.Length){
                        //Don't do anything
                    }
                    else
                        s = fldVal.Substring(f.Start - 1);
                }
                
            }
            else
                s = fldVal;
            
            return s;
        }
    }
}