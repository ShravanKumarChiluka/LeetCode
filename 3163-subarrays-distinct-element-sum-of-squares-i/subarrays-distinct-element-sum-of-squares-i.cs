using System;
using System.Collections.Generic;

public class Solution {
    public int SumCounts(IList<int> nums) {
        int totalSum = 0;
        int n = nums.Count;

        // Iterate through all possible starting positions of subarrays
        for (int i = 0; i < n; i++) {
            HashSet<int> distinctElements = new HashSet<int>();

            // Extend the subarray from index i to j
            for (int j = i; j < n; j++) {
                distinctElements.Add(nums[j]);
                
                // Get the distinct element count
                int count = distinctElements.Count;
                
                // Add the square of the count to the total sum
                totalSum += count * count;
            }
        }

        return totalSum;
    }
}
