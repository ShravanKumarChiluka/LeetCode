public class Solution {
    public int ReverseDegree(string s) {
        int reverseDegree = 0;
        for(int i=0; i<s.Length;i++){
            int indexPos = i+1;
            int reverseIndex = 26 - (s[i] - 'a');
            reverseDegree += indexPos * reverseIndex;
        }
        return reverseDegree;
    }
}