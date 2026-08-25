public class Solution {
    public int MissingMultiple(int[] nums, int k) {
        List<int> num = nums.ToList();
        int i=1;
        while(true){
            int multiple = k*i;
            if(!num.Contains(multiple)){
                return multiple;
            }
            i++;
        }
    }
}