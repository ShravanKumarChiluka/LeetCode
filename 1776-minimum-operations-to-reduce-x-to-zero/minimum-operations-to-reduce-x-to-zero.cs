using System;
using System.Linq;

public class Solution {
    public int MinOperations(int[] nums, int x) {
        int totalSum = 0;
        foreach (int num in nums) {
            totalSum += num;
        }

        // The target sum we want to find in the middle of the array
        int target = totalSum - x;

        // If target is exactly 0, it means we need to remove all elements
        if (target == 0) {
            return nums.Length;
        }
        
        // If target is negative, it's impossible to reduce x to 0 
        // because all elements are positive integers.
        if (target < 0) {
            return -1;
        }

        int left = 0;
        int currentSum = 0;
        int maxLen = -1;

        // Sliding window to find the longest subarray that sums up to 'target'
        for (int right = 0; right < nums.Length; right++) {
            currentSum += nums[right];

            // Shrink the window from the left if the current sum exceeds target
            while (currentSum > target && left <= right) {
                currentSum -= nums[left];
                left++;
            }

            // If we found a valid subarray, track its maximum length
            if (currentSum == target) {
                maxLen = Math.Max(maxLen, right - left + 1);
            }
        }

        // If maxLen was never updated, no valid subarray exists
        return maxLen == -1 ? -1 : nums.Length - maxLen;
    }
}
