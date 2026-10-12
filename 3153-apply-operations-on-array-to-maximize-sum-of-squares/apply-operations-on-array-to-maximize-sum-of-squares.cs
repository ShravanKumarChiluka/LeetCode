using System;
using System.Collections.Generic;

public class Solution {
    public int MaxSum(IList<int> nums, int k) {
        long mod = 1_000_000_007;
        
        // Step 1: Count the total frequency of set bits at each position (up to 30 bits)
        int[] bitCounts = new int[32];
        foreach (int num in nums) {
            for (int i = 0; i < 30; i++) {
                if (((num >> i) & 1) == 1) {
                    bitCounts[i]++;
                }
            }
        }
        
        long totalSumOfSquares = 0;
        
        // Step 2: Greedily construct the k largest numbers
        for (int i = 0; i < k; i++) {
            long currentNumber = 0;
            for (int j = 0; j < 30; j++) {
                // If there are remaining set bits for this position, assign one to this number
                if (bitCounts[j] > 0) {
                    currentNumber |= (1L << j);
                    bitCounts[j]--; // Consume the bit
                }
            }
            
            // Step 3: Accumulate the square of the constructed number modulo 10^9 + 7
            long square = (currentNumber * currentNumber) % mod;
            totalSumOfSquares = (totalSumOfSquares + square) % mod;
        }
        
        return (int)totalSumOfSquares;
    }
}
