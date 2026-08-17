using System;

public class Solution {
    private int[,] memo;
    private int[] prefixSums;

    public int StoneGameV(int[] stoneValue) {
        int n = stoneValue.Length;
        memo = new int[n, n];
        prefixSums = new int[n + 1];
        
        // Build prefix sums for O(1) subarray sum queries
        for (int i = 0; i < n; i++) {
            prefixSums[i + 1] = prefixSums[i] + stoneValue[i];
        }

        // Initialize memoization table with -1
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                memo[i, j] = -1;
            }
        }

        return GetMaxScore(0, n - 1);
    }

    private int GetMaxScore(int left, int right) {
        // Base case: Only one stone left, no split possible
        if (left == right) {
            return 0;
        }

        // Return cached result if already calculated
        if (memo[left, right] != -1) {
            return memo[left, right];
        }

        int maxScore = 0;

        // Try every possible partition index 'i'
        for (int i = left; i < right; i++) {
            int leftSum = prefixSums[i + 1] - prefixSums[left];
            int rightSum = prefixSums[right + 1] - prefixSums[i + 1];

            if (leftSum < rightSum) {
                // Bob discards the right row because it is larger
                maxScore = Math.Max(maxScore, leftSum + GetMaxScore(left, i));
            } 
            else if (leftSum > rightSum) {
                // Bob discards the left row because it is larger
                maxScore = Math.Max(maxScore, rightSum + GetMaxScore(i + 1, right));
            } 
            else {
                // Sums are equal, Alice chooses which row to keep
                int keepLeft = leftSum + GetMaxScore(left, i);
                int keepRight = rightSum + GetMaxScore(i + 1, right);
                maxScore = Math.Max(maxScore, Math.Max(keepLeft, keepRight));
            }
        }

        return memo[left, right] = maxScore;
    }
}
