using System;

public class Solution {
    public int MinimizeSum(int[] nums) {
        Array.Sort(nums);
        int n = nums.Length;
        
        // Option 1: Change the two smallest elements to match nums[2]
        int changeTwoSmallest = nums[n - 1] - nums[2];
        
        // Option 2: Change the two largest elements to match nums[n - 3]
        int changeTwoLargest = nums[n - 3] - nums[0];
        
        // Option 3: Change the smallest and largest elements to match inner values
        int changeOneEach = nums[n - 2] - nums[1];
        
        return Math.Min(changeTwoSmallest, Math.Min(changeTwoLargest, changeOneEach));
    }
}
