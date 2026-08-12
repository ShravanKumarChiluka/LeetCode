public class Solution {
    public int MaxSubarrayLength(int[] nums, int k) {
        Dictionary<int,int> frequencies = new Dictionary<int,int>();
        int maxLength = 0;
        int left = 0;

        for(int right = 0; right<nums.Length; right++){
            int currentNum = nums[right];

            if(!frequencies.ContainsKey(currentNum)){
                frequencies[currentNum] = 0;
            }
            frequencies[currentNum]++;

            while(frequencies[currentNum] > k){
                int leftNum = nums[left];
                frequencies[leftNum]--;
                left++;
            }
            maxLength = Math.Max(maxLength,right-left+1);
        }
        return maxLength;
    }
}