using System;

public class Solution {
    public int ClosedIsland(int[][] grid) {
        if (grid == null || grid.Length == 0) return 0;
        
        int rows = grid.Length;
        int cols = grid[0].Length;
        
        // Step 1: Flood-fill all land (0s) connected to the outer boundaries.
        // This converts boundary-connected land into water (1s) to exclude them.
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (r == 0 || r == rows - 1 || c == 0 || c == cols - 1) {
                    if (grid[r][c] == 0) {
                        Dfs(grid, r, c, rows, cols);
                    }
                }
            }
        }
        
        int closedIslandsCount = 0;
        
        // Step 2: Scan the interior grid. Any remaining 0s belong to a closed island.
        for (int r = 1; r < rows - 1; r++) {
            for (int c = 1; c < cols - 1; c++) {
                if (grid[r][c] == 0) {
                    closedIslandsCount++;
                    Dfs(grid, r, c, rows, cols); // Sink the island to avoid duplicate counts
                }
            }
        }
        
        return closedIslandsCount;
    }
    
    private void Dfs(int[][] grid, int r, int c, int rows, int cols) {
        // Base case: Out of bounds or already water/visited
        if (r < 0 || r >= rows || c < 0 || c >= cols || grid[r][c] == 1) {
            return;
        }
        
        // Mark the current land cell as water (1) to track it as visited
        grid[r][c] = 1;
        
        // Traverse all 4 cardinal directions
        Dfs(grid, r + 1, c, rows, cols); // Down
        Dfs(grid, r - 1, c, rows, cols); // Up
        Dfs(grid, r, c + 1, rows, cols); // Right
        Dfs(grid, r, c - 1, rows, cols); // Left
    }
}
