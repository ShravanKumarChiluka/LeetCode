public class Solution {
    public int[] MaxDepthAfterSplit(string seq) {
        int n = seq.Length;
        int[] ans = new int[n];
        int depth = 0;

        for (int i = 0; i < n; i++) {
            if (seq[i] == '(') {
                // Increment depth first for the new level
                depth++;
                // Assign to group 0 or 1 based on odd/even parity
                ans[i] = depth % 2; 
            } else {
                // The closing bracket matches the current level's parity
                ans[i] = depth % 2;
                // Decrement depth as we exit this level
                depth--;
            }
        }

        return ans;
    }
}
