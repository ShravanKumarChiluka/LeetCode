using System;

public class Solution {
    public int MaxPalindromes(string s, int k) {
        int n = s.Length;
        // dp[i] stores the max palindromes possible in the prefix s[0...i-1]
        int[] dp = new int[n + 1];
        
        for (int i = 0; i < n; i++) {
            // By default, carry forward the best result from the previous index
            dp[i + 1] = Math.Max(dp[i + 1], dp[i]);
            
            // Check for an odd or even palindrome centered around the current index
            for (int j = 0; j < 2; j++) {
                int left = i;
                int right = i + j;
                
                // Expand outward from the center
                while (left >= 0 && right < n && s[left] == s[right]) {
                    int length = right - left + 1;
                    
                    if (length >= k) {
                        // Found a valid palindrome! Update dp at the end index of this palindrome
                        dp[right + 1] = Math.Max(dp[right + 1], dp[left] + 1);
                        
                        // Greedy optimization: since expanding further only increases the length
                        // and reduces options for future matches, we break out immediately.
                        break;
                    }
                    
                    left--;
                    right++;
                }
            }
        }
        
        return dp[n];
    }
}
