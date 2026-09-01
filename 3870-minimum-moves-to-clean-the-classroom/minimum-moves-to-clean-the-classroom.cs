public class Solution {
    public int MinMoves(IList<string> classroom, int energy) {
        // Grid is 1D array of strings, so rows m = 1
        int m = classroom.Count;
        int n = classroom[0].Length;
        
        int startR = -1, startC = -1;
        var litters = new List<(int r, int c)>();
        
        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {
                if (classroom[r][c] == 'S') {
                    startR = r;
                    startC = c;
                } else if (classroom[r][c] == 'L') {
                    litters.Add((r, c));
                }
            }
        }
        
        int totalLitters = litters.Count;
        int targetMask = (1 << totalLitters) - 1;
        
        var queue = new Queue<(int r, int c, int e, int mask, int steps)>();
        var visited = new int[m, n, 1 << totalLitters];
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                for (int k = 0; k < (1 << totalLitters); k++)
                    visited[i, j, k] = -1;
                    
        queue.Enqueue((startR, startC, energy, 0, 0));
        visited[startR, startC, 0] = energy;
        
        int[] dr = {-1, 1, 0, 0};
        int[] dc = {0, 0, -1, 1};
        
        while (queue.Count > 0) {
            var curr = queue.Dequeue();
            
            // Check if all litter is collected
            if (curr.mask == targetMask) {
                return curr.steps;
            }
            
            // If current cell is NOT a recharge station and energy is 0, we are stuck
            if (classroom[curr.r][curr.c] != 'R' && curr.e == 0) {
                continue;
            }
            
            for (int i = 0; i < 4; i++) {
                int nr = curr.r + dr[i];
                int nc = curr.c + dc[i];
                
                if (nr < 0 || nr >= m || nc < 0 || nc >= n || classroom[nr][nc] == 'X')
                    continue;
                    
                // Deduct energy for the move
                int nextE = curr.e - 1;
                
                // If it's a recharge station, it immediately restores to full capacity
                if (classroom[nr][nc] == 'R') {
                    nextE = energy;
                }
                
                int nextMask = curr.mask;
                for (int lid = 0; lid < totalLitters; lid++) {
                    if (litters[lid].r == nr && litters[lid].c == nc) {
                        nextMask |= (1 << lid);
                    }
                }
                
                if (nextE > visited[nr, nc, nextMask]) {
                    visited[nr, nc, nextMask] = nextE;
                    queue.Enqueue((nr, nc, nextE, nextMask, curr.steps + 1));
                }
            }
        }
        
        return -1;
    }
}
