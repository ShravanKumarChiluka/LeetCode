using System;
using System.Collections.Generic;

public class Solution {
    public long FindKthSmallest(int[] coins, int k) {
        // Precompute all subset combinations and their Least Common Multiples (LCMs)
        // to avoid repetitive calculations inside the binary search loop.
        var combinations = PrecomputeLCMs(coins);

        // Define binary search boundaries
        long low = 1;
        long high = (long)coins[0] * k; 
        long result = high;

        // Binary search for the exact value
        while (low <= high) {
            long mid = low + (high - low) / 2;

            if (CountAmountsLessThanOrEqual(mid, combinations) >= k) {
                result = mid; // mid is a potential candidate, try finding a smaller one
                high = mid - 1;
            } else {
                low = mid + 1; // Not enough valid amounts, increase the target
            }
        }

        return result;
    }

    // Counts valid coin multiples <= maxAmount using Principle of Inclusion-Exclusion
    private long CountAmountsLessThanOrEqual(long maxAmount, List<(long lcm, int elementCount)> combinations) {
        long totalCount = 0;

        foreach (var (lcm, elementCount) in combinations) {
            long multiplesCount = maxAmount / lcm;

            // If the subset has an odd number of coins, add the count.
            // If even, subtract it to remove overlapping duplicates.
            if (elementCount % 2 == 1) {
                totalCount += multiplesCount;
            } else {
                totalCount -= multiplesCount;
            }
        }

        return totalCount;
    }

    // Generates all 2^N - 1 non-empty subsets of coins and calculates their LCMs
    private List<(long lcm, int elementCount)> PrecomputeLCMs(int[] coins) {
        var combinations = new List<(long lcm, int elementCount)>();
        int n = coins.Length;
        int totalSubsets = 1 << n; // 2^n combinations

        // Start from 1 to skip the empty subset
        for (int mask = 1; mask < totalSubsets; mask++) {
            long currentLcm = 1;
            int elementCount = 0;
            bool overflow = false;

            for (int i = 0; i < n; i++) {
                // Check if the i-th coin is included in this subset
                if ((mask & (1 << i)) != 0) {
                    elementCount++;
                    currentLcm = Lcm(currentLcm, coins[i]);
                    
                    // Optimization: If LCM exceeds our max possible upper bound, 
                    // maxAmount / currentLcm will result in 0 anyway.
                    if (currentLcm > 5e10) { 
                        overflow = true;
                        break;
                    }
                }
            }

            if (!overflow) {
                combinations.Add((currentLcm, elementCount));
            }
        }

        return combinations;
    }

    // Helper method to find Greatest Common Divisor
    private long Gcd(long a, long b) {
        while (b != 0) {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Helper method to find Least Common Multiple
    private long Lcm(long a, long b) {
        return (a / Gcd(a, b)) * b;
    }
}
