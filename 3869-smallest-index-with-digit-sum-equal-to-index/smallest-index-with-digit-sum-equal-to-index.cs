public class Solution {
    public int SmallestIndex(int[] nums) {
       for(int i=0;i<nums.Length;i++){
        int sum = SumofNumber(nums[i]);
        if(sum == i) return i;
       } 
       return -1;
    }
    public int SumofNumber(int n){
        int sum = 0;
        while(n > 0){
            sum += n % 10;
            n = n/10;
        }
        return sum;
    }
}