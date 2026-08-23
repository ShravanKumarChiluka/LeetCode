public class Solution {
    public bool SumGame(string num) {
        int n = num.Length;
        int leftSum = 0;
        int rightSum = 0;
        int leftCount = 0;
        int rightCount = 0;

        for(int i=0; i<n/2 ;i++){
            if(num[i] == '?'){
                leftCount++;
            }
            else{
                leftSum += num[i] - '0';
            }
        }

        for(int i=n/2; i<n;i++){
            if(num[i] == '?'){
                rightCount++;
            }
            else{
                rightSum += num[i] - '0';
            }
        }

        return 2* (leftSum - rightSum) + 9 * (leftCount-rightCount) != 0;
    }
}