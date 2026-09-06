public class Solution {
    public int NumDistinct(string s, string t) {
        int m = s.Length;
        int n = t.Length;
        
        // Base case: if t is longer than s, it's impossible to form t
        if (n > m) return 0;

        // dp[i, j] represents matching s[0...i-1] with t[0...j-1]
        int[,] dp = new int[m + 1, n + 1];

        // An empty string t can always be formed by an empty subsequence of s (1 way)
        for (int i = 0; i <= m; i++) {
            dp[i, 0] = 1;
        }

        // Fill out the table
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                if (s[i - 1] == t[j - 1]) {
                    dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                } else {
                    dp[i, j] = dp[i - 1, j];
                }
            }
        }

        return dp[m, n];
    }
}
