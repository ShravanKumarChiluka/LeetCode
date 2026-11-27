public class Solution {
    public int MaximumSaleItems(int[][] items, int budget) {
        int n = items.Length;
        int[] freeCopies = new int[n];

        // 1. Precompute free copies gained from buying at least 1 copy of item i
        // Note: items[i][0] is factor, items[i][1] is price
        for (int i = 0; i < n; i++) {
            int factorI = items[i][0];
            for (int j = 0; j < n; j++) {
                if (i != j && items[j][0] % factorI == 0) {
                    freeCopies[i]++;
                }
            }
        }

        // dp[b] = max total items obtainable with budget b
        int[] dp = new int[budget + 1];

        for (int i = 0; i < n; i++) {
            int price = items[i][1];
            int freeCount = freeCopies[i];

            if (price > budget) continue;

            // 2. First copy (0/1 Knapsack): gives 1 purchased + freeCount free items
            for (int b = budget; b >= price; b--) {
                dp[b] = Math.Max(dp[b], dp[b - price] + 1 + freeCount);
            }

            // 3. Additional copies (Unbounded Knapsack): gives only 1 purchased item
            for (int b = price; b <= budget; b++) {
                dp[b] = Math.Max(dp[b], dp[b - price] + 1);
            }
        }

        return dp[budget];
    }
}
