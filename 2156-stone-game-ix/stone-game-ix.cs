public class Solution {
    public bool StoneGameIX(int[] stones) {
        int[] count = new int[3];

        foreach(int stone in stones){
            count[stone % 3]++;
        }

        if(count[0] % 2 == 0){
            return Math.Min(count[1],count[2]) > 0;
        }

        return Math.Abs(count[1] - count[2]) > 2;
    }
}