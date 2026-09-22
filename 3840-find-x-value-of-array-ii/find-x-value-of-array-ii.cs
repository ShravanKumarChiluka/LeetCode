using System;

public class Solution {
    public class SegmentTreeNode {
        public int Prod;
        public int[] Counts;

        public SegmentTreeNode(int k) {
            Prod = 1;
            Counts = new int[k];
        }
    }

    private SegmentTreeNode[] tree;
    private int n;
    private int K;

    public int[] ResultArray(int[] nums, int k, int[][] queries) {
        n = nums.Length;
        K = k;
        tree = new SegmentTreeNode[4 * n];
        
        Build(nums, 0, 0, n - 1);

        int[] result = new int[queries.Length];
        for (int i = 0; i < queries.Length; i++) {
            int index = queries[i][0];
            int val = queries[i][1];
            int start = queries[i][2];
            int x = queries[i][3];
            Update(0, 0, n - 1, index, val);

            SegmentTreeNode queryResult = Query(0, 0, n - 1, start, n - 1);

            result[i] = queryResult.Counts[x];
        }

        return result;
    }

    private void Build(int[] nums, int node, int start, int end) {
        tree[node] = new SegmentTreeNode(K);
        if (start == end) {
            int valMod = nums[start] % K;
            tree[node].Prod = valMod;
            tree[node].Counts[valMod] = 1;
            return;
        }

        int mid = start + (end - start) / 2;
        int leftChild = 2 * node + 1;
        int rightChild = 2 * node + 2;

        Build(nums, leftChild, start, mid);
        Build(nums, rightChild, mid + 1, end);

        Merge(tree[node], tree[leftChild], tree[rightChild]);
    }

    private void Update(int node, int start, int end, int idx, int val) {
        if (start == end) {
            int valMod = val % K;
            Array.Clear(tree[node].Counts, 0, K);
            tree[node].Prod = valMod;
            tree[node].Counts[valMod] = 1;
            return;
        }

        int mid = start + (end - start) / 2;
        int leftChild = 2 * node + 1;
        int rightChild = 2 * node + 2;

        if (idx <= mid) {
            Update(leftChild, start, mid, idx, val);
        } else {
            Update(rightChild, mid + 1, end, idx, val);
        }

        Merge(tree[node], tree[leftChild], tree[rightChild]);
    }

    private SegmentTreeNode Query(int node, int start, int end, int ql, int qr) {

        if (ql <= start && end <= qr) {
            SegmentTreeNode leafCopy = new SegmentTreeNode(K);
            leafCopy.Prod = tree[node].Prod;
            Array.Copy(tree[node].Counts, leafCopy.Counts, K);
            return leafCopy;
        }

        int mid = start + (end - start) / 2;
        int leftChild = 2 * node + 1;
        int rightChild = 2 * node + 2;

        if (qr <= mid) {
            return Query(leftChild, start, mid, ql, qr);
        }
        if (ql > mid) {
            return Query(rightChild, mid + 1, end, ql, qr);
        }

        SegmentTreeNode leftAns = Query(leftChild, start, mid, ql, qr);
        SegmentTreeNode rightAns = Query(rightChild, mid + 1, end, ql, qr);
        
        SegmentTreeNode mergedAns = new SegmentTreeNode(K);
        Merge(mergedAns, leftAns, rightAns);
        return mergedAns;
    }

    private void Merge(SegmentTreeNode parent, SegmentTreeNode left, SegmentTreeNode right) {
        parent.Prod = (int)((long)left.Prod * right.Prod % K);
        Array.Clear(parent.Counts, 0, K);

        for (int r = 0; r < K; r++) {
            parent.Counts[r] += left.Counts[r];
        }

        for (int r = 0; r < K; r++) {
            if (right.Counts[r] > 0) {
                int combinedMod = (int)((long)left.Prod * r % K);
                parent.Counts[combinedMod] += right.Counts[r];
            }
        }
    }
}
