public class Solution {
    public int LargestOverlap(int[][] img1, int[][] img2) {
        int n = img1.Length;
        var list1 = new List<(int r, int c)>();
        var list2 = new List<(int r, int c)>();

        for(int r= 0; r<n;r++){
            for(int c = 0; c < n; c++){
                if(img1[r][c] == 1) list1.Add((r,c));
                if(img2[r][c] == 1) list2.Add((r,c));
            }
        }
        var counts = new Dictionary<(int dr, int dc), int>();
        int maxOverlap = 0;

        foreach(var p1 in list1){
            foreach(var p2 in list2){
                var vector = (p1.r - p2.r, p1.c - p2.c);
                counts.TryGetValue(vector, out int val);
                counts[vector] = val + 1;
                maxOverlap = Math.Max(maxOverlap, counts[vector]);
            }
        }
        return maxOverlap;
    }
}