using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    // A helper class to keep track of the maximum weight and the indices taken.
    private class Element : IComparable<Element> {
        public long Weight { get; set; }
        public List<int> Indices { get; set; }

        public Element(long weight, List<int> indices) {
            this.Weight = weight;
            this.Indices = indices;
        }

        // Compare elements based on weight (descending) and then lexicographically (ascending)
        public int CompareTo(Element other) {
            if (this.Weight != other.Weight) {
                return other.Weight.CompareTo(this.Weight); // Max weight first
            }
            
            // Compare the sorted lists lexicographically
            int minLen = Math.Min(this.Indices.Count, other.Indices.Count);
            for (int i = 0; i < minLen; i++) {
                if (this.Indices[i] != other.Indices[i]) {
                    return this.Indices[i].CompareTo(other.Indices[i]); // Smaller index first
                }
            }
            return this.Indices.Count.CompareTo(other.Indices.Count);
        }
    }

    private class Interval {
        public int Start { get; set; }
        public int End { get; set; }
        public int Weight { get; set; }
        public int Id { get; set; }
    }

    public int[] MaximumWeight(IList<IList<int>> intervals) {
        int n = intervals.Count;
        var items = new Interval[n];
        
        for (int i = 0; i < n; i++) {
            items[i] = new Interval {
                Start = intervals[i][0],
                End = intervals[i][1],
                Weight = intervals[i][2],
                Id = i
            };
        }

        // 1. Sort intervals by their Start time
        items = items.OrderBy(x => x.Start).ToArray();

        // dp[i, k] stores the best Element choice using a subset of intervals from index i to n-1, choosing at most k intervals.
        var dp = new Element[n + 1, 5];

        // Initialize base cases for out-of-bounds or 0 quota
        for (int i = 0; i <= n; i++) {
            for (int k = 0; k <= 4; k++) {
                dp[i, k] = new Element(0, new List<int>());
            }
        }

        // 2. Compute DP table bottom-up
        for (int i = n - 1; i >= 0; i--) {
            // Find the next non-overlapping interval index using Binary Search
            int nextIdx = FindNextNonOverlapping(items, i);

            for (int k = 1; k <= 4; k++) {
                // Option A: Skip the current interval
                Element skip = dp[i + 1, k];

                // Option B: Take the current interval
                Element nextState = dp[nextIdx, k - 1];
                long takeWeight = items[i].Weight + nextState.Weight;
                
                // Form the new lexicographically sorted index path
                var takeIndices = new List<int>(nextState.Indices) { items[i].Id };
                takeIndices.Sort();

                Element take = new Element(takeWeight, takeIndices);

                // Choose the best option between Skip and Take
                if (take.CompareTo(skip) < 0) {
                    dp[i, k] = take;
                } else {
                    dp[i, k] = skip;
                }
            }
        }

        return dp[0, 4].Indices.ToArray();
    }

    // Binary search to find the first interval that starts strictly after the current interval ends.
    private int FindNextNonOverlapping(Interval[] items, int currIdx) {
        int low = currIdx + 1;
        int high = items.Length - 1;
        int ans = items.Length;

        while (low <= high) {
            int mid = low + (high - low) / 2;
            if (items[mid].Start > items[currIdx].End) {
                ans = mid;
                high = mid - 1; // Try to find an even earlier valid start
            } else {
                low = mid + 1;
            }
        }
        return ans;
    }
}
