using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int MinSwaps(int[] nums) {
        int n = nums.Length;
        int ans = 0;
        
        // 1. Create a tracking array of tuples containing: (originalValue, originalIndex)
        var items = new (int Val, int OrigIdx)[n];
        for (int i = 0; i < n; i++) {
            items[i] = (nums[i], i);
        }
        
        // 2. Sort the array based on the custom rules:
        //    Primary: Digit Sum ascending
        //    Secondary: Value ascending (tie-breaker)
        Array.Sort(items, (a, b) => {
            int sumA = GetDigitSum(a.Val);
            int sumB = GetDigitSum(b.Val);
            if (sumA != sumB) {
                return sumA.CompareTo(sumB);
            }
            return a.Val.CompareTo(b.Val);
        });
        
        // 3. Detect cycles to compute the minimum number of swaps needed
        bool[] visited = new bool[n];
        
        for (int i = 0; i < n; i++) {
            // If the element is already visited or already in its correct position
            if (visited[i] || items[i].OrigIdx == i) {
                continue;
            }
            
            // Calculate size of the current cycle
            int cycleSize = 0;
            int j = i;
            
            while (!visited[j]) {
                visited[j] = true;
                j = items[j].OrigIdx; // Move to the index where the current element originally came from
                cycleSize++;
            }
            
            if (cycleSize > 0) {
                ans += (cycleSize - 1);
            }
        }
        
        return ans;
    }
    
    // Helper function to calculate the sum of digits
    private int GetDigitSum(int num) {
        int sum = 0;
        while (num > 0) {
            sum += num % 10;
            num /= 10;
        }
        return sum;
    }
}
