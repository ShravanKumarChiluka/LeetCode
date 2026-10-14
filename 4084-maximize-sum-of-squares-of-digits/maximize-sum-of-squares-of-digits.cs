using System;
using System.Text;

public class Solution {
    public string MaxSumOfSquares(int num, int sum) {
        // If even filling all positions with 9 cannot reach 'sum', it's impossible.
        if ((long)num * 9 < sum) {
            return "";
        }

        StringBuilder sb = new StringBuilder(num);

        // 1. Greedily append as many 9s as possible to maximize squares and lexicographical value.
        int nines = sum / 9;
        sb.Append('9', nines);

        // 2. Append the single remainder digit if it exists.
        int remainder = sum % 9;
        if (remainder > 0) {
            sb.Append(remainder);
        }

        // 3. Pad the remaining length with 0s.
        int remainingLength = num - sb.Length;
        if (remainingLength > 0) {
            sb.Append('0', remainingLength);
        }

        return sb.ToString();
    }
}
