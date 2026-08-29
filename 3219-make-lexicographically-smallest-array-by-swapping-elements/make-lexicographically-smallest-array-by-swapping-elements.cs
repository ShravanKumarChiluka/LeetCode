using System;
using System.Collections.Generic;

public class Solution {
    public int[] LexicographicallySmallestArray(int[] nums, int limit) {
        int n = nums.Length;

        var paired = new(int val,int idx)[n];
        for(int i=0;i<n;i++){
            paired[i] = (nums[i],i);
        }
        Array.Sort(paired,(a,b) => a.val.CompareTo(b.val));

        int[] result = new int[n];
        int left = 0;
        while(left < n){
            int right = left+1;
            while(right < n && paired[right].val -paired[right-1].val <= limit){
                right++;
            }
            int[] indices = new int[right - left];
            for (int k = left; k < right; k++) {
                indices[k - left] = paired[k].idx;
            }
            Array.Sort(indices);

            for(int k=left;k<right;k++){
                result[indices[k-left]] = paired[k].val;
            }
            left = right;
        }
        return result;
    }
}