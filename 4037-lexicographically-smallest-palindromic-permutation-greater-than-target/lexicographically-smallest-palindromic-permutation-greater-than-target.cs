using System;
using System.Text;

public class Solution {
    public string LexPalindromicPermutation(string s, string target) {
        int[] freq = new int[26];
        foreach (char ch in s) {
            freq[ch - 'a']++;
        }

        // 1. Validation: At most one character can have an odd frequency
        int oddCount = 0;
        int midCharIdx = -1;
        for (int i = 0; i < 26; i++) {
            if (freq[i] % 2 != 0) {
                oddCount++;
                midCharIdx = i;
            }
        }
        if (oddCount > 1) {
            return "";
        }

        // Half frequencies dictate the layout of the first half
        for (int i = 0; i < 26; i++) {
            freq[i] /= 2;
        }

        int n = s.Length;
        int half = n / 2;
        char[] ans = new char[n];

        // Helper method to mirror the first half and append the center character if needed
        void CompletePalindrome() {
            if (midCharIdx != -1) {
                ans[half] = (char)('a' + midCharIdx);
            }
            for (int i = 0; i < half; i++) {
                ans[n - 1 - i] = ans[i];
            }
        }

        // 2. Greedily match prefix with target
        int pos = 0;
        while (pos < half) {
            int chIdx = target[pos] - 'a';
            if (freq[chIdx] == 0) {
                break;
            }
            ans[pos] = target[pos];
            freq[chIdx]--;
            pos++;
        }

        // Check if an exact prefix match works
        if (pos == half) {
            CompletePalindrome();
            if (CompareArrays(ans, target) > 0) {
                return new string(ans);
            }
        }

        // 3. Fallback / Backtracking loop
        while (true) {
            if (pos < half) {
                int minIdx = target[pos] - 'a' + 1;
                int chosenCh = -1;
                
                // Find the smallest available character strictly greater than target[pos]
                for (int i = minIdx; i < 26; i++) {
                    if (freq[i] > 0) {
                        chosenCh = i;
                        break;
                    }
                }

                if (chosenCh != -1) {
                    ans[pos] = (char)('a' + chosenCh);
                    freq[chosenCh]--;

                    // Fill remaining part of the first half greedily with smallest available characters
                    int dst = pos + 1;
                    for (int i = 0; i < 26; i++) {
                        while (freq[i] > 0) {
                            ans[dst++] = (char)('a' + i);
                            freq[i]--;
                        }
                    }

                    CompletePalindrome();
                    return new string(ans);
                }
            }

            // If we backtracked to the very beginning and still couldn't deviate, no solution exists
            if (pos == 0) {
                return "";
            }

            // Restore state of last character to try an alternate route
            pos--;
            freq[target[pos] - 'a']++;
        }
    }

    private int CompareArrays(char[] a, string b) {
        for (int i = 0; i < a.Length; i++) {
            if (a[i] != b[i]) {
                return a[i].CompareTo(b[i]);
            }
        }
        return 0;
    }
}
