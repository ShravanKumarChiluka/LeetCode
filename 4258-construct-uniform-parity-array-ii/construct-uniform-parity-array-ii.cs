public class Solution {
    public bool UniformArray(int[] nums1) {
        int minOdd = int.MaxValue;

        foreach(int x in nums1){
            if(x % 2 != 0){
                minOdd = Math.Min(minOdd,x);
            }
        }

        foreach(int x in nums1){
            if(x % 2 ==0 && minOdd != int.MaxValue && x<minOdd){
                return false;
            }
        }
        return true;
    }
}