public class Solution {
    public int LongestSubsequence(int[] nums) {
        int total = 0;
        bool hasNonZero = false;

        foreach (int num in nums){
            total ^= num;
            if(num != 0){
                hasNonZero = true;
            }
        }
            if(!hasNonZero){
                return 0;
            }

            if(total !=0){
                return nums.Length;
            }
            return nums.Length-1;
        
    }
}