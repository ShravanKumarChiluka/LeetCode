public class Solution {
    public int MinAddToMakeValid(string s) {
        int open = 0;
        int additions = 0;

        foreach (char ele in s) {
            if (ele == '(') {
                open++;
            } else if (ele == ')') {
                if (open > 0) {
                    open--; // Matched with an open bracket
                } else {
                    additions++; // Unmatched close bracket needs a '(' added
                }
            }
        }

        return open + additions; // Sum of unmatched '(' and unmatched ')'
    }
}