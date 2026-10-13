public class Solution {
    public long MaxAlternatingSum(int[] nums) {
        int len = nums.Length;
        long[] squares = new long[len];

        for(int i = 0; i< len; i++){
            squares[i] = (long)nums[i] * nums[i];
        }

        Array.Sort(squares);

        long total = 0;
        int mid = len/2;

        for(int i = 0;i<mid;i++){
            total -= squares[i];
        }
        for(int i = mid; i< len;i++){
            total += squares[i];
        }
        return total;
    }
}