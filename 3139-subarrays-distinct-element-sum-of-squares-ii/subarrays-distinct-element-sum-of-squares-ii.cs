using System;
using System.Collections.Generic;

public class Solution {
    private const int MOD = 1000000007;

    public int SumCounts(int[] nums) {
        int n = nums.Length;
        LazySegmentTree tree = new LazySegmentTree(n);
        Dictionary<int, int> lastSeen = new Dictionary<int, int>();
        long totalAns = 0;

        for (int r = 0; r < n; r++) {
            // If the element was seen before, it only contributes a new distinct 
            // element to subarrays starting AFTER its last occurrence.
            int l = lastSeen.ContainsKey(nums[r]) ? lastSeen[nums[r]] + 1 : 0;
            
            // Increment the range [l, r] by 1
            tree.Update(0, 0, n - 1, l, r, 1);
            
            // Record the current element's index
            lastSeen[nums[r]] = r;
            
            // Add the sum of squares of all subarrays ending at index 'r'
            totalAns = (totalAns + tree.GetTotalSumSq()) % MOD;
        }

        return (int)totalAns;
    }

    private class LazySegmentTree {
        private int n;
        private long[] sum;
        private long[] sumSq;
        private long[] lazy;

        public LazySegmentTree(int n) {
            this.n = n;
            sum = new long[4 * n];
            sumSq = new long[4 * n];
            lazy = new long[4 * n];
        }

        public long GetTotalSumSq() {
            return sumSq[0];
        }

        private void Push(int node, int start, int end) {
            if (lazy[node] == 0) return;

            long val = lazy[node];
            int mid = start + (end - start) / 2;
            int leftChild = 2 * node + 1;
            int rightChild = 2 * node + 2;

            // Update left child
            Apply(leftChild, start, mid, val);
            // Update right child
            Apply(rightChild, mid + 1, end, val);

            // Clear current lazy flag
            lazy[node] = 0;
        }

        private void Apply(int node, int start, int end, long val) {
            long len = end - start + 1;
            
            // New sumSq = sumSq + 2 * val * sum + len * val * val
            sumSq[node] = (sumSq[node] + 2 * val % MOD * sum[node] % MOD + len * val % MOD * val % MOD) % MOD;
            // New sum = sum + len * val
            sum[node] = (sum[node] + len * val % MOD) % MOD;
            // Accumulate lazy tag
            lazy[node] = (lazy[node] + val) % MOD;
        }

        public void Update(int node, int start, int end, int l, int r, long val) {
            if (r < start || end < l) return;

            if (l <= start && end <= r) {
                Apply(node, start, end, val);
                return;
            }

            Push(node, start, end);

            int mid = start + (end - start) / 2;
            Update(2 * node + 1, start, mid, l, r, val);
            Update(2 * node + 2, mid + 1, end, l, r, val);

            sum[node] = (sum[2 * node + 1] + sum[2 * node + 2]) % MOD;
            sumSq[node] = (sum[2 * node + 1] + sum[2 * node + 2]) % MOD;
            // Ensure values remain non-negative
            sum[node] = (sum[node] + MOD) % MOD;
            sumSq[node] = (sumSq[2 * node + 1] + sumSq[2 * node + 2]) % MOD;
        }
    }
}
