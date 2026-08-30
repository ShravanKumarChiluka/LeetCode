public class Solution {
    public void MoveZeroes(int[] nums) {
        int non_zero = 0;
        for(int i=0;i<nums.Length;i++){
            if(nums[i] != 0){
                nums[non_zero] = nums[i];
                non_zero++;
            }
        }
        while(non_zero < nums.Length){
            nums[non_zero] = 0;
            non_zero++;
        }
    }
}