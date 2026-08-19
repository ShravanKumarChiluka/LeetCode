using System;
using System.Collections.Generic;

public class Solution {
    public int MaxNumberOfFamilies(int n, int[][] reservedSeats) {
        // Map to store the bitmask of reserved seats for each row
        Dictionary<int, int> rowMasks = new Dictionary<int, int>();

        // Step 1: Build the bitmask for rows with reservations
        foreach (var seat in reservedSeats) {
            int row = seat[0];
            int seatNum = seat[1];
            
            // Only seats 2 through 9 matter for 4-person groups
            if (seatNum >= 2 && seatNum <= 9) {
                if (!rowMasks.ContainsKey(row)) {
                    rowMasks[row] = 0;
                }
                // Set the bit corresponding to the seat number (0-indexed from seat 2)
                rowMasks[row] |= (1 << (seatNum - 2));
            }
        }

        // Bitwise masks representing forbidden seat states for each block
        int leftMask = 15;   // binary 00001111 -> seats 2,3,4,5
        int midMask = 60;    // binary 00111100 -> seats 4,5,6,7
        int rightMask = 240; // binary 11110000 -> seats 6,7,8,9

        // Start by assuming all rows can fit 2 families max
        int maxFamilies = n * 2;

        // Step 2: Deduct family slots based on actual reservations
        foreach (var kvp in rowMasks) {
            int mask = kvp.Value;
            bool leftFree = (mask & leftMask) == 0;
            bool rightFree = (mask & rightMask) == 0;
            bool midFree = (mask & midMask) == 0;

            if (leftFree && rightFree) {
                // Both sides are free, stays at 2 families (no deduction needed)
                continue;
            }
            else if (leftFree || rightFree || midFree) {
                // Exactly 1 family can sit in this row, deduct 1 from the default 2
                maxFamilies -= 1;
            }
            else {
                // No families can fit in this row, deduct 2 from the default 2
                maxFamilies -= 2;
            }
        }

        return maxFamilies;
    }
}
