public class Solution {
    public int NumberOfSets(int n, int k) {
        int totalPoints = n + k - 1;
        int choose = 2 * k;
        
        // If we don't have enough points to form the segments
        if (totalPoints < choose) return 0;
        
        long MOD = 1_000_000_007;
        
        // Compute nCr % MOD using Modular Inverse (Fermat's Little Theorem)
        long numerator = 1;
        long denominator = 1;
        
        // Restricting the loop to the smaller of choose or (totalPoints - choose)
        int r = Math.Min(choose, totalPoints - choose);
        
        for (int i = 1; i <= r; i++) {
            numerator = (numerator * (totalPoints - i + 1)) % MOD;
            denominator = (denominator * i) % MOD;
        }
        
        return (int)((numerator * ModInverse(denominator, MOD)) % MOD);
    }
    
    private long ModInverse(long n, long m) {
        return Power(n, m - 2, m);
    }
    
    private long Power(long baseVal, long exp, long m) {
        long res = 1;
        baseVal = baseVal % m;
        while (exp > 0) {
            if ((exp & 1) == 1)
                res = (res * baseVal) % m;
            exp >>= 1;
            baseVal = (baseVal * baseVal) % m;
        }
        return res;
    }
}
