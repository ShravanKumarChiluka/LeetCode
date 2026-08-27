public class Solution {
    public string LexGreaterPermutation(string s, string target) {
        int n = s.Length;
        int[] counts = new int[26];
        
        // Count frequencies of each character in s
        foreach (char c in s) {
            counts[c - 'a']++;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        // Helper function for backtracking/greedy simulation
        if (TryBuild(0, false, target, counts, sb, n)) {
            return sb.ToString();
        }

        return "";
    }

    private bool TryBuild(int idx, bool isGreater, string target, int[] counts, System.Text.StringBuilder sb, int n) {
        // Base case: successfully built a full-length permutation
        if (idx == n) {
            return isGreater;
        }

        // If we are already strictly greater, fill the rest with the smallest available chars
        if (isGreater) {
            for (int i = 0; i < 26; i++) {
                while (counts[i] > 0) {
                    sb.Append((char)('a' + i));
                    counts[i]--;
                }
            }
            return true;
        }

        int targetCharIdx = target[idx] - 'a';

        // Scenario A: Try to match the target character at the current index
        if (counts[targetCharIdx] > 0) {
            counts[targetCharIdx]--;
            sb.Append(target[idx]);
            
            if (TryBuild(idx + 1, false, target, counts, sb, n)) {
                return true;
            }
            
            // Backtrack if matching target[idx] doesn't yield a valid solution later
            sb.Remove(sb.Length - 1, 1);
            counts[targetCharIdx]++;
        }

        // Scenario B: Try to pick the smallest character strictly greater than target[idx]
        for (int i = targetCharIdx + 1; i < 26; i++) {
            if (counts[i] > 0) {
                counts[i]--;
                sb.Append((char)('a' + i));
                
                // Since this character is larger than target[idx], the rest can be greedily smallest
                TryBuild(idx + 1, true, target, counts, sb, n);
                return true;
            }
        }

        return false;
    }
}
