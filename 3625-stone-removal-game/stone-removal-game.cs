public class Solution {
    public bool CanAliceWin(int n) {
        int stonesToRemove = 10;
        bool isAliceTurn = true;

        while (n >= stonesToRemove) {
            n -= stonesToRemove;
            stonesToRemove--;
            isAliceTurn = !isAliceTurn;
        }

        // If it's currently Bob's turn when a move can no longer be made, Alice wins
        return !isAliceTurn;
    }
}
