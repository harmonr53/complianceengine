using System;
using System.Text;

namespace complianceengine.Util
{
    public static class EngineUtils
    {
        public static string ReturnRecType(Dictionary<int,string> obs)
        {
            string s = obs.FirstOrDefault(kv => kv.Key == 2).Value;

            return s;
        }
        public static Dictionary<int, String> SplitQualifiedToDictionary(this String source)
        {
            char delimiter = ',';
            char qualifier = '"';
            Boolean toTrim = false;
            // Avoid throwing an exception if the source is null
            if (String.IsNullOrEmpty(source))
                return new Dictionary<int, String> { {0, ""} };

            var results = new Dictionary<int, String>();
            var result = new StringBuilder();
            Boolean inQualifier = false;

            // Add an artificial delimiter at the end of the source string for easy termination
            String sourceX = source + delimiter;

            int index = 0;

            // Loop through each character in the source string
            for (var idx = 0; idx < sourceX.Length; idx++)
            {
                // If the current character is a delimiter and not inside a qualifier
                if (sourceX[idx] == delimiter && !inQualifier)
                {
                    // Add the current substring to the dictionary (trim if specified)
                    results[index] = toTrim ? result.ToString().Trim() : result.ToString();
                    index++;
                    result.Clear();
                }
                // If the current character is a qualifier
                else if (sourceX[idx] == qualifier)
                {
                    // If we are already inside a qualifier
                    if (inQualifier)
                    {
                        // Check for double-qualifiers (escaping)
                        if (idx + 1 < sourceX.Length && sourceX[idx + 1] == qualifier)
                        {
                            idx++;
                            result.Append(sourceX[idx]);
                            continue;
                        }
                        // We found the end of the enclosing qualifiers
                        inQualifier = false;
                        continue;
                    }
                    else
                        // We found an opening qualifier
                        inQualifier = true;
                }
                // Append the current character to the substring if it's not a qualifier or delimiter
                else
                {
                    result.Append(sourceX[idx]);
                }
            }

            return results;
        }


        
        // public static List<String> SplitQualified(this String source, Char delimiter, Char qualifier,
        //                             Boolean toTrim)
        // {
        //     // Avoid throwing exception if the source is null
        //     if (String.IsNullOrEmpty(source))
        //         return new List<String> { "" };

        //     var results = new List<String>();
        //     var result = new StringBuilder();
        //     Boolean inQualifier = false;

        //     // The algorithm is designed to expect a delimiter at the end of each substring, but the
        //     // expectation of the caller is that the final substring is not terminated by delimiter.
        //     // Therefore, we add an artificial delimiter at the end before looping through the source string.
        //     String sourceX = source + delimiter;

        //     // Loop through each character of the source
        //     for (var idx = 0; idx < sourceX.Length; idx++)
        //     {
        //         // If current character is a delimiter
        //         // (except if we're inside of qualifiers, we ignore the delimiter)
        //         if (sourceX[idx] == delimiter && inQualifier == false)
        //         {
        //             // Terminate the current substring by adding it to the collection
        //             // (trim if specified by the method parameter)
        //             results.Add(toTrim ? result.ToString().Trim() : result.ToString());
        //             result.Clear();
        //         }
        //         // If current character is a qualifier
        //         else if (sourceX[idx] == qualifier)
        //         {
        //             // ...and we're already inside of qualifier
        //             if (inQualifier)
        //             {
        //                 // check for double-qualifiers, which is escape code for a single
        //                 // literal qualifier character.
        //                 if (idx + 1 < sourceX.Length && sourceX[idx + 1] == qualifier)
        //                 {
        //                     idx++;
        //                     result.Append(sourceX[idx]);
        //                     continue;
        //                 }
        //                 // Since we found only a single qualifier, that means that we've
        //                 // found the end of the enclosing qualifiers.
        //                 inQualifier = false;
        //                 continue;
        //             }
        //             else
        //                 // ...we found an opening qualifier
        //                 inQualifier = true;
        //         }
        //         // If current character is neither qualifier nor delimiter
        //         else
        //             result.Append(sourceX[idx]);
        //     }

        //     return results;
        // }
    }
}