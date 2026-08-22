using System;

public class Solution {
    public bool CheckDivisibility(int n) {
        int numberSum = 0;
        int remainingSum = Math.Abs(n);
        while(remainingSum > 0){
            numberSum += remainingSum % 10;
            remainingSum /=10;
        }
        int product = GetProductDigit(n);
        int ans = numberSum + product;
        if(n % ans ==0){
            return true;
        }
        return false;

    }
    static int GetProductDigit(int n){
        if(n==0) return 0;
        int product = 1;
        while(n>0){
            int digit = n%10;
            product *=digit;
            n/=10;
        }
        return product;
    }
}