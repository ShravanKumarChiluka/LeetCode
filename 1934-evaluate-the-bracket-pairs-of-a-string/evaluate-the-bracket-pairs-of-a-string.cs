using System;
using System.Collections.Generic;
using System.Text;

public class Solution {
    public string Evaluate(string s, IList<IList<string>> knowledge) {
        // 1. Build a hash map from the knowledge list for O(1) lookups
        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var pair in knowledge) {
            dict[pair[0]] = pair[1];
        }

        StringBuilder result = new StringBuilder();
        StringBuilder currentKey = new StringBuilder();
        bool inBracket = false;

        // 2. Scan the string in a single left-to-right pass
        foreach (char c in s) {
            if (c == '(') {
                inBracket = true;
            } else if (c == ')') {
                inBracket = false;
                string key = currentKey.ToString();
                
                // Append the value if found, otherwise append "?"
                if (dict.TryGetValue(key, out string value)) {
                    result.Append(value);
                } else {
                    result.Append('?');
                }
                
                // Reset the temporary key accumulator
                currentKey.Clear();
            } else {
                if (inBracket) {
                    currentKey.Append(c);
                } else {
                    result.Append(c);
                }
            }
        }

        return result.ToString();
    }
}
