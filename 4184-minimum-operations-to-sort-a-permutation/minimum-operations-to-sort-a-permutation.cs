using System;

public class Solution {
    public int MinOperations(int[] nums) {
        int n = nums.Length;
        int zeroIdx = -1;

        // Find the index of 0
        for (int i = 0; i < n; i++) {
            if (nums[i] == 0) {
                zeroIdx = i;
                break;
            }
        }

        // Helper method to check if the sequence forms a valid relative permutation
        bool Check(int step) {
            for (int i = 1; i < n; i++) {
                int prev = (zeroIdx + (i - 1) * step % n + n) % n;
                int curr = (zeroIdx + i * step % n + n) % n;
                if (nums[prev] > nums[curr]) {
                    return false;
                }
            }
            return true;
        }

        int ans = int.MaxValue;

        // Case 1: Check if the array behaves like a rotated increasing sequence
        if (Check(1)) {
            ans = Math.Min(ans, zeroIdx);            // Purely rotating left
            ans = Math.Min(ans, n - zeroIdx + 2);    // Reversing, rotating, and reversing back
        }

        // Case 2: Check if the array behaves like a rotated decreasing sequence
        if (Check(-1)) {
            ans = Math.Min(ans, zeroIdx + 2);        // Rotating and reversing
            ans = Math.Min(ans, n - zeroIdx);        // Reversing and rotating
        }

        return ans == int.MaxValue ? -1 : ans;
    }
}
