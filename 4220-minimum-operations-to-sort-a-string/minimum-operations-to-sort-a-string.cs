using System;

public class Solution {
    public int MinOperations(string s) {
        int n = s.Length;
        
        // 1. Check if the string is already sorted
        bool isSorted = true;
        for (int i = 1; i < n; i++) {
            if (s[i] < s[i - 1]) {
                isSorted = false;
                break;
            }
        }
        if (isSorted) return 0;
        
        // 2. Base case for impossible sort due to "proper substring" constraint
        if (n == 2) return -1;
        
        // Find global minimum and maximum characters
        char mn = s[0];
        char mx = s[0];
        for (int i = 1; i < n; i++) {
            if (s[i] < mn) mn = s[i];
            if (s[i] > mx) mx = s[i];
        }
        
        // 3. One operation is enough if an extreme element is already in the right spot
        if (s[0] == mn || s[n - 1] == mx) {
            return 1;
        }
        
        // 4. Two operations if min or max exists somewhere in the middle
        for (int i = 1; i < n - 1; i++) {
            if (s[i] == mn || s[i] == mx) {
                return 2;
            }
        }
        
        // 5. Three operations if min is at the last index and max is at the first index
        return 3;
    }
}
