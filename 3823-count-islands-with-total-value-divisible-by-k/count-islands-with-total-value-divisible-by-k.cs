using System;
using System.Collections.Generic;

public class Solution {
    public int CountIslands(int[][] grid, int k) {
        int m = grid.Length;
        int n = grid[0].Length;
        int validIslandsCount = 0;

        // Directions array for moving 4-directionally (up, down, left, right)
        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                // Positive integer represents unvisited land
                if (grid[i][j] > 0) {
                    long islandSum = 0;
                    Queue<(int r, int c)> queue = new Queue<(int r, int c)>();

                    // Start BFS
                    queue.Enqueue((i, j));
                    islandSum += grid[i][j];
                    grid[i][j] = 0; // Mark as visited by setting to 0

                    while (queue.Count > 0) {
                        var (currRow, currCol) = queue.Dequeue();

                        for (int d = 0; d < 4; d++) {
                            int nextRow = currRow + dRow[d];
                            int nextCol = currCol + dCol[d];

                            // Check grid boundaries and if the neighbor is land
                            if (nextRow >= 0 && nextRow < m && 
                                nextCol >= 0 && nextCol < n && 
                                grid[nextRow][nextCol] > 0) {
                                
                                islandSum += grid[nextRow][nextCol];
                                grid[nextRow][nextCol] = 0; // Mark as visited
                                queue.Enqueue((nextRow, nextCol));
                            }
                        }
                    }

                    // Check if the total value of this island is divisible by k
                    if (islandSum % k == 0) {
                        validIslandsCount++;
                    }
                }
            }
        }

        return validIslandsCount;
    }
}
