using System;
using System.Collections.Generic;

public class Solution {
    private const long MOD = 1_000_000_007L;
    private long[][] pascalTriangle;

    public int NumOfWays(int[] nums) {
        int n = nums.Length;
        
        // Step 1: Precompute Pascal's Triangle for Combination values (nCr)
        pascalTriangle = new long[n + 1][];
        for (int i = 0; i <= n; i++) {
            pascalTriangle[i] = new long[i + 1];
            pascalTriangle[i][0] = 1;
            pascalTriangle[i][i] = 1;
            for (int j = 1; j < i; j++) {
                pascalTriangle[i][j] = (pascalTriangle[i - 1][j - 1] + pascalTriangle[i - 1][j]) % MOD;
            }
        }

        List<int> list = new List<int>(nums);
        
        // Step 2: Solve recursively and subtract 1 (to exclude the original array layout)
        long totalWays = Solve(list);
        return (int)((totalWays - 1 + MOD) % MOD);
    }

    private long Solve(List<int> nums) {
        // Base case: If the list has 2 or fewer elements, there is only 1 way to arrange them
        if (nums.Count <= 2) {
            return 1;
        }

        int root = nums[0];
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        // Separate elements into left and right subtrees
        for (int i = 1; i < nums.Count; i++) {
            if (nums[i] < root) {
                left.Add(nums[i]);
            } else {
                right.Add(nums[i]);
            }
        }

        // Recursive results for subtrees
        long leftWays = Solve(left);
        long rightWays = Solve(right);

        // Total ways for this subtree level using Combinations: C(total_elements - 1, left_elements)
        long combinations = pascalTriangle[nums.Count - 1][left.Count];
        
        // Multiply all paths under modulo arithmetic
        long totalWays = (combinations * leftWays) % MOD;
        totalWays = (totalWays * rightWays) % MOD;

        return totalWays;
    }
}
