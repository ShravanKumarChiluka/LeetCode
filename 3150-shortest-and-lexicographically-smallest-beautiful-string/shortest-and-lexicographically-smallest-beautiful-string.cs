using System;

public class Solution {
    public string ShortestBeautifulSubstring(string s, int k) {
        int n = s.Length;
        int left = 0;
        int countOnes = 0;
        string result = "";

        for (int right = 0; right < n; right++) {
            // Expand window: Count the 1s
            if (s[right] == '1') {
                countOnes++;
            }

            // Shrink window: When we have exactly k '1's
            while (countOnes == k) {
                // Trim leading zeros to ensure the window is as short as possible
                if (s[left] == '1') {
                    // Extract the valid candidate substring
                    string currentSubstring = s.Substring(left, right - left + 1);

                    // Update result if it's the first match, shorter, or lexicographically smaller
                    if (result == "" || 
                        currentSubstring.Length < result.Length || 
                        (currentSubstring.Length == result.Length && string.CompareOrdinal(currentSubstring, result) < 0)) {
                        result = currentSubstring;
                    }

                    // Prepare to shrink further by removing the '1' at the left boundary
                    countOnes--;
                }
                left++;
            }
        }

        return result;
    }
}
