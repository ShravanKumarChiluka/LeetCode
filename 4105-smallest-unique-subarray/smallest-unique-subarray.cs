using System;

public class Solution {
    public int SmallestUniqueSubarray(int[] nums) {
        int n = nums.Length;
        if (n <= 1) return n;

        // Randomize bases at runtime to prevent anti-hash test cases
        long base1 = Random.Shared.Next(100_000_000, 500_000_000) | 1;
        long base2 = Random.Shared.Next(500_000_000, 900_000_000) | 1;
        const long mod1 = 1_000_000_007L;
        const long mod2 = 1_000_000_009L;

        long[] pow1 = new long[n + 1];
        long[] pow2 = new long[n + 1];
        pow1[0] = 1;
        pow2[0] = 1;

        for (int i = 1; i <= n; i++) {
            pow1[i] = (pow1[i - 1] * base1) % mod1;
            pow2[i] = (pow2[i - 1] * base2) % mod2;
        }

        // Allocate the buffer once and reuse it across all binary search checks
        ulong[] hashes = new ulong[n];

        int low = 1, high = n, ans = n;
        while (low <= high) {
            int mid = low + (high - low) / 2;
            if (HasUniqueSubarray(nums, mid, hashes, pow1, pow2, mod1, mod2, base1, base2)) {
                ans = mid;
                high = mid - 1;
            } else {
                low = mid + 1;
            }
        }

        return ans;
    }

    private bool HasUniqueSubarray(int[] nums, int len, ulong[] hashes, long[] pow1, long[] pow2,
                                   long mod1, long mod2, long base1, long base2) {
        int n = nums.Length;
        int count = n - len + 1;

        long h1 = 0, h2 = 0;
        long p1 = pow1[len - 1];
        long p2 = pow2[len - 1];

        for (int i = 0; i < len; i++) {
            h1 = (h1 * base1 + nums[i]) % mod1;
            h2 = (h2 * base2 + nums[i]) % mod2;
        }

        hashes[0] = ((ulong)h1 << 32) | (ulong)h2;

        for (int i = len; i < n; i++) {
            h1 = (h1 - (nums[i - len] * p1) % mod1 + mod1) % mod1;
            h1 = (h1 * base1 + nums[i]) % mod1;

            h2 = (h2 - (nums[i - len] * p2) % mod2 + mod2) % mod2;
            h2 = (h2 * base2 + nums[i]) % mod2;

            hashes[i - len + 1] = ((ulong)h1 << 32) | (ulong)h2;
        }

        // Sort only the active slice of the preallocated array
        Array.Sort(hashes, 0, count);

        for (int i = 0; i < count; i++) {
            bool leftDiff = (i == 0) || (hashes[i] != hashes[i - 1]);
            bool rightDiff = (i == count - 1) || (hashes[i] != hashes[i + 1]);
            if (leftDiff && rightDiff) {
                return true;
            }
        }

        return false;
    }
}
