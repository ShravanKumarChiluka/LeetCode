public class Solution {
    public int MinimumDeletions(int[] nums) {
        int n = nums.Length;
        int left =0;
        int right  = 0;
        int minIndex = 0;
        int maxIndex = 0;

        for(int i=0;i<n;i++){
            if(nums[i] < nums[minIndex]){
                minIndex = i;
            }
            if(nums[i] > nums[maxIndex]){
                maxIndex = i;
            }
        }
        left = Math.Min(minIndex,maxIndex);
        right = Math.Max(minIndex,maxIndex);

        int removeFromLeft = right + 1;
        int removeFromRight = n-left;
        int removeFromBoth = (left + 1) + (n-right);

        return Math.Min(removeFromLeft,Math.Min(removeFromRight,removeFromBoth));
    }
}