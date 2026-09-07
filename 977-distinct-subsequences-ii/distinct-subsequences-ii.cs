using System;
using System.Linq;

public class Solution {
    public int DistinctSubseqII(string s) {
        int MOD = 1_000_000_007;
        // ends[i] stores the number of distinct subsequences ending with character ('a' + i)
        long[] ends = new long[26];
        long totalSum = 0;

        foreach (char c in s) {
            int index = c - 'a';
            
            // New subsequences formed by appending character 'c'
            long newSubseqCount = (totalSum + 1) % MOD;
            
            // Calculate the updated total sum:
            // Remove the old count for this character and add the new count
            totalSum = (totalSum - ends[index] + newSubseqCount + MOD) % MOD;
            
            // Update the count for the current character
            ends[index] = newSubseqCount;
        }

        return (int)totalSum;
    }
}
