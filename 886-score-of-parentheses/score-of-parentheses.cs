using System;

public class Solution {
    public int ScoreOfParentheses(string s) {
        int totalScore = 0;
        int depth = 0;

        for (int i = 0; i < s.Length; i++) {
            if (s[i] == '(') {
                // Moving down a nesting layer
                depth++;
            } else {
                // Moving up a nesting layer
                depth--;
                
                // If the previous character was '(', we found a core "()" unit
                if (s[i - 1] == '(') {
                    // 1 << depth is equivalent to 2^depth
                    totalScore += (1 << depth);
                }
            }
        }

        return totalScore;
    }
}
