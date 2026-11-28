using System;
using System.Collections.Generic;

public class Solution {
    public int MaximumSaleItems(int[][] items, int budget) {
        int n = items.Length;
        int minPrice = int.MaxValue;
        int maxFactor = 0;

        // 1. Find the minimum price and maximum factor
        foreach (var item in items) {
            maxFactor = Math.Max(maxFactor, item[0]);
            minPrice = Math.Min(minPrice, item[1]);
        }

        // 2. Count frequency of each factor and sum its multiples (Sieve approach)
        int[] freq = new int[maxFactor + 1];
        foreach (var item in items) {
            freq[item[0]]++;
        }

        for (int i = 1; i <= maxFactor; i++) {
            if (freq[i] > 0) {
                for (int multiple = 2 * i; multiple <= maxFactor; multiple += i) {
                    freq[i] += freq[multiple];
                }
            }
        }

        // 3. Collect all profitable 2-for-1 opportunities (price < 2 * minPrice)
        // Using long for keys/values to prevent integer overflow
        var costCount = new SortedDictionary<long, long>();
        foreach (var item in items) {
            int factor = item[0];
            long price = item[1];
            long freeCopiesAvailable = freq[factor] - 1;

            if (freeCopiesAvailable > 0 && price < 2L * minPrice) {
                if (!costCount.ContainsKey(price)) {
                    costCount[price] = 0;
                }
                costCount[price] += freeCopiesAvailable;
            }
        }

        // 4. Greedily buy the cheapest 2-for-1 deals first
        long totalItems = 0;
        long remainingBudget = budget;

        foreach (var kvp in costCount) {
            long price = kvp.Key;
            long count = kvp.Value;

            if (remainingBudget < price) {
                break;
            }

            long canBuy = Math.Min(count, remainingBudget / price);
            remainingBudget -= canBuy * price;
            totalItems += canBuy * 2;
        }

        // 5. Spend any leftover budget on the single cheapest item (1-for-1)
        totalItems += remainingBudget / minPrice;

        return (int)totalItems;
    }
}
