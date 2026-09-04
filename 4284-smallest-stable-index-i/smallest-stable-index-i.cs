using System;
using System.Linq;
public class Solution {
    public int FirstStableIndex(int[] nums, int k) {
        int j=nums.Length;
        for(int i=0;i<nums.Length;i++){
            int max_nums = nums.Take(i+1).Max();
            int min_nums = nums[(i)..j].Min();
            if((max_nums - min_nums) <= k){
                return i;
            }
        }
        return -1;
    }
}