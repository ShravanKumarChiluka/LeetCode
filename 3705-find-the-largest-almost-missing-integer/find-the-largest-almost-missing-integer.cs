using System;
using System.Collections.Generic;

public class Solution {
    public int LargestInteger(int[] nums, int k) {
        int n = nums.Length;
        // Tracks how many subarrays each number appears in
        Dictionary<int, int> subarrayCounts = new Dictionary<int, int>();
        
        // Tracks element frequencies within the current sliding window
        Dictionary<int, int> windowCounts = new Dictionary<int, int>();

        // Process all subarrays of size k
        for (int i = 0; i <= n - k; i++) {
            // If it's the first window, build it from scratch
            if (i == 0) {
                for (int j = 0; j < k; j++) {
                    windowCounts[nums[j]] = windowCounts.GetValueOrDefault(nums[j], 0) + 1;
                }
            } else {
                // Slide the window: remove the element leaving on the left
                int outNum = nums[i - 1];
                windowCounts[outNum]--;
                if (windowCounts[outNum] == 0) {
                    windowCounts.Remove(outNum);
                }
                
                // Add the element entering on the right
                int inNum = nums[i + k - 1];
                windowCounts[inNum] = windowCounts.GetValueOrDefault(inNum, 0) + 1;
            }

            // Increment the subarray appearance count for each unique number in this window
            foreach (int num in windowCounts.Keys) {
                subarrayCounts[num] = subarrayCounts.GetValueOrDefault(num, 0) + 1;
            }
        }

        // Find the largest integer that appeared in exactly 1 subarray
        int maxAlmostMissing = -1;
        foreach (var pair in subarrayCounts) {
            if (pair.Value == 1) {
                maxAlmostMissing = Math.Max(maxAlmostMissing, pair.Key);
            }
        }

        return maxAlmostMissing;
    }
}
