using System;
using System.Collections.Generic;

public class Solution {
    public int MaxSelectedElements(int[] nums) {
        Array.Sort(nums);
        
        // dp[v] stores the max length of a consecutive sequence ending at value v
        var dp = new Dictionary<int, int>();
        int maxLen = 0;

        foreach (int num in nums) {
            // Update num + 1 FIRST to avoid using the same element twice
            int prevForPlusOne = dp.GetValueOrDefault(num, 0);
            dp[num + 1] = prevForPlusOne + 1;

            int prevForCurrent = dp.GetValueOrDefault(num - 1, 0);
            dp[num] = prevForCurrent + 1;

            maxLen = Math.Max(maxLen, Math.Max(dp[num], dp[num + 1]));
        }

        return maxLen;
    }
}
