using System;

public class Solution {
    public int LongestValidParentheses(string s) {
        if (string.IsNullOrEmpty(s)) return 0;

        int left = 0, right = 0, maxLength = 0;

        // Pass 1: Scan from left to right
        for (int i = 0; i < s.Length; i++) {
            if (s[i] == '(') {
                left++;
            } else {
                right++;
            }

            if (left == right) {
                maxLength = Math.Max(maxLength, 2 * right);
            } else if (right > left) {
                // Invalid substring state: too many closing brackets, reset counters
                left = right = 0;
            }
        }

        left = right = 0;

        // Pass 2: Scan from right to left (handles unmatched leading '(' cases)
        for (int i = s.Length - 1; i >= 0; i--) {
            if (s[i] == '(') {
                left++;
            } else {
                right++;
            }

            if (left == right) {
                maxLength = Math.Max(maxLength, 2 * left);
            } else if (left > right) {
                // Invalid substring state: too many opening brackets, reset counters
                left = right = 0;
            }
        }

        return maxLength;
    }
}
