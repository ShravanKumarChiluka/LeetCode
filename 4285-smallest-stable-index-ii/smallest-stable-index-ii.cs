using System;

public class Solution {
    public int FirstStableIndex(int[] nums, int k) {
        if (nums == null || nums.Length == 0) return -1;
        
        int n = nums.Length;
        
        // 1. Precompute suffix minimums from right to left
        int[] suffixMin = new int[n];
        suffixMin[n - 1] = nums[n - 1];
        for (int j = n - 2; j >= 0; j--) {
            suffixMin[j] = Math.Min(nums[j], suffixMin[j + 1]);
        }
        
        // 2. Track running max from left to right and evaluate stability
        int maxSoFar = int.MinValue;
        for (int i = 0; i < n; i++) {
            maxSoFar = Math.Max(maxSoFar, nums[i]);
            
            // Instability score calculation in O(1)
            if (maxSoFar - suffixMin[i] <= k) {
                return i;
            }
        }
        
        return -1;
    }
}
