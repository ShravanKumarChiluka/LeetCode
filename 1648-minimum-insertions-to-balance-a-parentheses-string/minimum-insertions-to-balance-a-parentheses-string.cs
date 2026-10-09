public class Solution {
    public int MinInsertions(string s) {
        int insertions = 0;
        int neededRight = 0;

        foreach (char c in s) {
            if (c == '(') {
                // If we need an odd number of right brackets, 
                // it means the previous '(' only got one ')' instead of two.
                // We must insert 1 right bracket to balance it.
                if (neededRight % 2 != 0) {
                    insertions++;   // Insert 1 ')'
                    neededRight--;  // We fulfilled that single missing ')'
                }
                neededRight += 2;   // This new '(' requires 2 ')'
            } else {
                // c == ')'
                neededRight--;

                // If neededRight drops below 0, we have an unmatched ')'
                if (neededRight < 0) {
                    insertions++;   // Insert 1 '(' to match it
                    neededRight += 2; // The newly inserted '(' gives us 2 ')' needs (minus the current one)
                }
            }
        }

        // At the end, any remaining 'neededRight' elements must be added manually
        return insertions + neededRight;
    }
}
