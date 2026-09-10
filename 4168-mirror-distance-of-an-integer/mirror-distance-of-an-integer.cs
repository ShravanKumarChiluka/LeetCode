public class Solution {
    public int MirrorDistance(int n) {
        string numberString = Math.Abs(n).ToString();
        char[] charArray = numberString.ToCharArray();
        Array.Reverse(charArray);

        int reverseNumber = int.Parse(new string(charArray));
        return Math.Abs(n - reverseNumber);
    }
}