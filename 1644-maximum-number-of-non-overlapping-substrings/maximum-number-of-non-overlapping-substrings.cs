using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> MaxNumOfSubstrings(string s) {
        int n = s.Length;
        int[] first = new int[26];
        int[] last = new int[26];
        Array.Fill(first, -1);
        Array.Fill(last, -1);

        // Step 1: Record the first and last occurrence of each character
        for (int i = 0; i < n; i++) {
            int charIdx = s[i] - 'a';
            if (first[charIdx] == -1) {
                first[charIdx] = i;
            }
            last[charIdx] = i;
        }

        List<int[]> intervals = new List<int[]>();

        // Step 2: Form valid candidate intervals for each character
        for (int i = 0; i < 26; i++) {
            if (first[i] == -1) continue;

            int startIndex = first[i];
            int endIndex = CheckInterval(s, startIndex, first, last);

            // If endIndex is valid, we store this valid interval
            if (endIndex != -1) {
                intervals.Add(new int[] { startIndex, endIndex });
            }
        }

        // Step 3: Greedy interval selection
        // Sort intervals by their end positions (ascending) to take the shortest valid end first
        intervals.Sort((a, b) => a[1].CompareTo(b[1]));

        List<string> result = new List<string>();
        int lastEnd = -1;

        foreach (var interval in intervals) {
            int start = interval[0];
            int end = interval[1];

            // If it doesn't overlap with the last taken interval, add it
            if (start > lastEnd) {
                result.Add(s.Substring(start, end - start + 1));
                lastEnd = end;
            }
        }

        return result;
    }

    private int CheckInterval(string s, int start, int[] first, int[] last) {
        int end = last[s[start] - 'a'];
        
        for (int i = start; i <= end; i++) {
            int charIdx = s[i] - 'a';
            
            // If this character appeared before our starting boundary, 
            // then the interval starting at 'start' is invalid.
            if (first[charIdx] < start) {
                return -1;
            }
            
            // Dynamically extend the end boundary to include all occurrences of the new character
            end = Math.Max(end, last[charIdx]);
        }
        
        return end;
    }
}
