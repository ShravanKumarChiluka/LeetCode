using System;

public class Solution {
    public long MinSumSquareDiff(int[] nums1, int[] nums2, int k1, int k2) {
        int n = nums1.Length;
        long totalK = (long)k1 + k2;
        
        // The maximum value of nums1[i], nums2[i] is 10^5, so maximum diff is 10^5
        int maxDiff = 100000;
        long[] bucket = new long[maxDiff + 1];
        long totalDiffSum = 0;

        // Calculate absolute differences and populate the frequency bucket
        for (int i = 0; i < n; i++) {
            int diff = Math.Abs(nums1[i] - nums2[i]);
            if (diff > 0) {
                bucket[diff]++;
                totalDiffSum += diff;
            }
        }

        // If total operations available are more than or equal to the sum of all differences, 
        // we can reduce all differences completely to 0.
        if (totalDiffSum <= totalK) {
            return 0;
        }

        // Process greedily from the largest difference downwards
        for (int d = maxDiff; d > 0 && totalK > 0; d--) {
            if (bucket[d] == 0) continue;

            // Determine how many items we can reduce by 1
            long countToReduce = Math.Min(totalK, bucket[d]);
            
            bucket[d] -= countToReduce;
            bucket[d - 1] += countToReduce;
            totalK -= countToReduce;
        }

        // Calculate the final sum of squared differences
        long minSquaredSum = 0;
        for (int d = 1; d <= maxDiff; d++) {
            if (bucket[d] > 0) {
                minSquaredSum += bucket[d] * ((long)d * d);
            }
        }

        return minSquaredSum;
    }
}
