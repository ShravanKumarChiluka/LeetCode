using System;
using System.Collections.Generic;

public class Solution {
    public int TotalNumbers(int[] digits) {
        // Step 1: Count the frequency of each digit in the input array
        int[] availableCounts = new int[10];
        foreach (int d in digits) {
            availableCounts[d]++;
        }

        int count = 0;

        // Step 2: Iterate through all possible 3-digit even numbers
        // 3-digit numbers start at 100 and end at 999. 
        // We increment by 2 to guarantee the number is even.
        for (int num = 100; num <= 998; num += 2) {
            // Extract the digits
            int hundreds = num / 100;
            int tens = (num / 10) % 10;
            int units = num % 10;

            // Step 3: Count the digits needed for the current number
            int[] requiredCounts = new int[10];
            requiredCounts[hundreds]++;
            requiredCounts[tens]++;
            requiredCounts[units]++;

            // Step 4: Check if we have enough of each digit available
            bool canForm = true;
            for (int i = 0; i < 10; i++) {
                if (availableCounts[i] < requiredCounts[i]) {
                    canForm = false;
                    break;
                }
            }

            // If we have enough digits, it's a valid unique combination
            if (canForm) {
                count++;
            }
        }

        return count;
    }
}
