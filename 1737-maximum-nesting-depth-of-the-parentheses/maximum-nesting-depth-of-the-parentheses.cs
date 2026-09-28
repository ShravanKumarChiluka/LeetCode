using System;

public class Solution {
    public int MaxDepth(string s) {
        int maxDepth = 0;
        int currentDepth = 0;
        
        foreach (char c in s) {
            if (c == '(') {
                currentDepth++;
                if (currentDepth > maxDepth) {
                    maxDepth = currentDepth;
                }
            } 
            else if (c == ')') {
                currentDepth--;
            }
        }
        return maxDepth;
    }
}
