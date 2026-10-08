using System;
using System.Text;

public class Solution {
    public string RemoveOuterParentheses(string s) {
        StringBuilder result = new StringBuilder();
        int depth = 0;

        foreach (char c in s) {
            if (c == '(') {
                // If depth > 0, it means this '(' is not the outermost one
                if (depth > 0) {
                    result.Append(c);
                }
                depth++;
            } else {
                depth--;
                // If depth > 0 after decrement, this ')' is not the outermost one
                if (depth > 0) {
                    result.Append(c);
                }
            }
        }

        return result.ToString();
    }
}
