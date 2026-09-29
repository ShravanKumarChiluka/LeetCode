using System;

public class Solution {
    private bool[,,] visited;

    public bool HasValidPath(char[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;

        // Base optimization: path length must be even, start must be '(', end must be ')'
        if ((m + n - 1) % 2 != 0 || grid[0][0] == ')' || grid[m - 1][n - 1] == '(') {
            return false;
        }

        // Max possible balance cannot exceed the path length
        int maxBalance = m + n;
        visited = new bool[m, n, maxBalance];

        return Dfs(grid, 0, 0, 0);
    }

    private bool Dfs(char[][] grid, int i, int j, int k) {
        int m = grid.Length;
        int n = grid[0].Length;

        // Update balance tracker based on current cell
        k += grid[i][j] == '(' ? 1 : -1;

        // Invalid states: balance drops below 0 or exceeds remaining steps to close
        if (k < 0 || k > (m - 1 - i) + (n - 1 - j)) {
            return false;
        }

        // Base case: reached bottom-right corner
        if (i == m - 1 && j == n - 1) {
            return k == 0;
        }

        // If this exact state was already calculated, skip it
        if (visited[i, j, k]) {
            return false;
        }
        visited[i, j, k] = true;

        // Explore moving Down
        if (i + 1 < m && Dfs(grid, i + 1, j, k)) {
            return true;
        }

        // Explore moving Right
        if (j + 1 < n && Dfs(grid, i, j + 1, k)) {
            return true;
        }

        return false;
    }
}
