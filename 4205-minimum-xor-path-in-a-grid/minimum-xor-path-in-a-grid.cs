public class Solution {
    public int MinCost(int[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;
        
        // dp[r][c] stores all reachable XOR sums up to cell (r, c)
        HashSet<int>[][] dp = new HashSet<int>[m][];
        for (int r = 0; r < m; r++) {
            dp[r] = new HashSet<int>[n];
            for (int c = 0; c < n; c++) {
                dp[r][c] = new HashSet<int>();
            }
        }
        
        // Base case: Starting position
        dp[0][0].Add(grid[0][0]);
        
        // Populate the DP table
        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {
                if (r == 0 && c == 0) continue;
                
                int currentVal = grid[r][c];
                
                // If moving down from the cell above
                if (r > 0) {
                    foreach (int prevXor in dp[r - 1][c]) {
                        dp[r][c].Add(prevXor ^ currentVal);
                    }
                }
                
                // If moving right from the cell to the left
                if (c > 0) {
                    foreach (int prevXor in dp[r][c - 1]) {
                        dp[r][c].Add(prevXor ^ currentVal);
                    }
                }
            }
        }
        
        // Return the minimum XOR value found at the bottom-right destination cell
        return dp[m - 1][n - 1].Min();
    }
}
