using System;
using System.Collections.Generic;

public class Solution {
    public int[] FindSubtreeSizes(int[] parent, string s) {
        int n = parent.Length;
        
        // 1. Build the initial tree adjacency list
        List<int>[] initialTree = new List<int>[n];
        for (int i = 0; i < n; i++) {
            initialTree[i] = new List<int>();
        }
        for (int i = 1; i < n; i++) {
            initialTree[parent[i]].Add(i);
        }

        // 2. First DFS: Find the new parent for each node simultaneously
        int[] newParent = (int[])parent.Clone();
        int[] lastSeen = new int[26];
        Array.Fill(lastSeen, -1);

        void FindNewParents(int node) {
            int charIdx = s[node] - 'a';
            int previousAncestor = lastSeen[charIdx];

            // If an ancestor with the same character exists, update parent
            if (previousAncestor != -1) {
                newParent[node] = previousAncestor;
            }

            // Set current node as the closest ancestor for this character
            lastSeen[charIdx] = node;

            foreach (int child in initialTree[node]) {
                FindNewParents(child);
            }

            // Backtrack: restore the previous ancestor before leaving this branch
            lastSeen[charIdx] = previousAncestor;
        }

        FindNewParents(0);

        // 3. Build the restructured tree using newParent
        List<int>[] finalTree = new List<int>[n];
        for (int i = 0; i < n; i++) {
            finalTree[i] = new List<int>();
        }
        for (int i = 1; i < n; i++) {
            finalTree[newParent[i]].Add(i);
        }

        // 4. Second DFS: Calculate subtree sizes
        int[] answer = new int[n];

        int CalculateSizes(int node) {
            int size = 1; // Count the node itself
            foreach (int child in finalTree[node]) {
                size += CalculateSizes(child);
            }
            answer[node] = size;
            return size;
        }

        CalculateSizes(0);

        return answer;
    }
}
