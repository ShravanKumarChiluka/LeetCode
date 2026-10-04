public class Solution {
    public bool CheckValidString(string s) {
        int leftMin = 0;
        int leftMax = 0;

        foreach (char ch in s) {
            if (ch == '(') {
                leftMin++;
                leftMax++;
            } else if (ch == ')') {
                leftMin--;
                leftMax--;
            } else if (ch == '*') {
                // '*' can be ')', which reduces the minimum open parentheses
                leftMin--; 
                // '*' can be '(', which increases the maximum open parentheses
                leftMax++; 
            }

            // If leftMax drops below 0, it means we have more ')' than 
            // any possible combinations of '(' and '*' can accommodate.
            if (leftMax < 0) return false;

            // leftMin cannot be negative because we can't have less than 0 open parentheses.
            // If it goes negative, reset it to 0 (meaning we treat excess '*' as empty strings).
            if (leftMin < 0) leftMin = 0;
        }

        // The string is valid if it's possible to have exactly 0 open parentheses at the end.
        return leftMin == 0;
    }
}
