public class Solution {
    public int MinSumOfLengths(int[] arr, int target) {
        int n = arr.Length;

        int[] minLens = new int[n];
        Array.Fill(minLens, int.MaxValue);
        int minTotalLen = int.MaxValue;
        int currentSum = 0;
        int l = 0;

        for(int r=0;r<n;r++){
            currentSum += arr[r];

            while(currentSum > target && l <= r){
                currentSum -= arr[l];
                l++;
            }
            if(currentSum == target){
                int currentLen = r-l+1;

                if(l > 0 && minLens[l - 1] != int.MaxValue){
                    minTotalLen = Math.Min(minTotalLen, currentLen + minLens[l-1]);
                }
                if(r > 0){
                    minLens[r] = Math.Min(minLens[r-1], currentLen);
                }
                else{
                    minLens[r] = currentLen;
                }
            }
            else{
                if(r > 0){
                    minLens[r] = minLens[r-1];
                }
            }
        }
        return minTotalLen == int.MaxValue ? -1 : minTotalLen;
    }
}