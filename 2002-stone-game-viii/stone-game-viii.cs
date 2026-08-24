using System;
using System.Linq;

public class Solution {
    public int StoneGameVIII(int[] stones) {
        int n = stones.Length;

        int[] prefixSums = new int[n];
        prefixSums[0] = stones[0];
        for(int i=1;i<n;i++){
            prefixSums[i] = prefixSums[i-1] + stones[i];
        }

        int maxDiff = prefixSums[n-1];
        for(int i=n-2; i>=1;i--){
            maxDiff = Math.Max(maxDiff,prefixSums[i] - maxDiff);
        }

        return maxDiff;
    }
}