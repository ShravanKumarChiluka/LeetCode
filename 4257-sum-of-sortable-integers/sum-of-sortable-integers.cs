public class Solution {
    public int SortableIntegers(int[] nums) {
        int n = nums.Length;
        int sum = 0;

        // Iterate through all possible divisors k of n
        for (int k = 1; k <= n; k++) {
            if (n % k == 0) {
                if (IsSortable(nums, k)) {
                    sum += k;
                }
            }
        }

        return sum;
    }

    private bool IsSortable(int[] nums, int k) {
        int n = nums.Length;
        int lastMax = int.MinValue;

        for (int start = 0; start < n; start += k) {
            int end = start + k - 1;
            
            // 1. Find the min and max elements and validate cyclic sortability
            int minVal = int.MaxValue;
            int maxVal = int.MinValue;
            int drops = 0;

            for (int i = start; i <= end; i++) {
                if (nums[i] < minVal) minVal = nums[i];
                if (nums[i] > maxVal) maxVal = nums[i];

                // Check for adjacent drop within the window
                if (i < end && nums[i] > nums[i + 1]) {
                    drops++;
                }
            }

            // A valid cyclic rotation can have at most 1 drop.
            // If it has exactly 1 drop, the boundary condition must hold.
            if (drops > 1) return false;
            if (drops == 1 && nums[end] > nums[start]) return false;

            // 2. Cross-subarray validation: 
            // The minimum of this chunk must be >= the maximum of the previous chunk
            if (minVal < lastMax) return false;

            // Track the maximum for the next iteration's boundary check
            lastMax = maxVal;
        }

        return true;
    }
}
