using System;
using System.Collections.Generic;

public class Solution {
    public int MinimumChanges(string s, int k) {
        int n = s.Length;
        
        // Precompute proper divisors for all possible substring lengths (2 to n)
        List<int>[] divisors = new List<int>[n + 1];
        for (int len = 2; len <= n; len++) {
            divisors[len] = new List<int>();
            for (int d = 1; d < len; d++) {
                if (len % d == 0) {
                    divisors[len].Add(d);
                }
            }
        }
        
        // cost[i, j] = min changes to make substring s[i..j] a semi-palindrome
        int[,] cost = new int[n, n];
        for (int i = 0; i < n; i++) {
            for (int j = i + 1; j < n; j++) {
                int len = j - i + 1;
                int minChanges = int.MaxValue;
                
                foreach (int d in divisors[len]) {
                    int changes = 0;
                    for (int start = 0; start < d; start++) {
                        int left = i + start;
                        int right = j - d + 1 + start;
                        while (left < right) {
                            if (s[left] != s[right]) {
                                changes++;
                            }
                            left += d;
                            right -= d;
                        }
                    }
                    minChanges = Math.Min(minChanges, changes);
                }
                cost[i, j] = minChanges;
            }
        }
        
        // dp[i, p] = min changes to partition prefix of length i into p semi-palindromes
        int INF = 1_000_000;
        int[,] dp = new int[n + 1, k + 1];
        for (int i = 0; i <= n; i++) {
            for (int p = 0; p <= k; p++) {
                dp[i, p] = INF;
            }
        }
        dp[0, 0] = 0;
        
        for (int p = 1; p <= k; p++) {
            // Each semi-palindrome must have length at least 2
            for (int i = 2 * p; i <= n; i++) {
                for (int prev = 2 * (p - 1); prev <= i - 2; prev++) {
                    dp[i, p] = Math.Min(dp[i, p], dp[prev, p - 1] + cost[prev, i - 1]);
                }
            }
        }
        
        return dp[n, k];
    }
}
