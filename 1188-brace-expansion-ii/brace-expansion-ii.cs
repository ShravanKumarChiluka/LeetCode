using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public IList<string> BraceExpansionII(string expression) {
        // Stack to store state before entering a group '{'
        // Each entry contains (previous_union, previous_product)
        Stack<(List<string> Union, List<string> Prod)> stack = new Stack<(List<string>, List<string>)>();
        
        List<string> currentUnion = new List<string>();
        List<string> currentProd = new List<string> { "" };

        for (int i = 0; i < expression.Length; i++) {
            char c = expression[i];

            if (char.IsLetter(c)) {
                // Adjacency means concatenation: multiply current product items by the new character
                List<string> nextProd = new List<string>();
                foreach (string p in currentProd) {
                    nextProd.Add(p + c);
                }
                currentProd = nextProd;
            } 
            else if (c == '{') {
                // Save current context to stack and reset for the inner group
                stack.Push((currentUnion, currentProd));
                currentUnion = new List<string>();
                currentProd = new List<string> { "" };
            } 
            else if (c == '}') {
                // Wrap up the last option in the inner group
                currentUnion.AddRange(currentProd);
                
                // Pop the outer context
                var (prevUnion, prevProd) = stack.Pop();
                
                // Concatenate outer product with inner group results (Cartesian product)
                List<string> combinedProd = new List<string>();
                foreach (string p in prevProd) {
                    foreach (string u in currentUnion) {
                        combinedProd.Add(p + u);
                    }
                }
                
                currentProd = combinedProd;
                currentUnion = prevUnion;
            } 
            else if (c == ',') {
                // Commas denote a union boundary: move current product options to union list
                currentUnion.AddRange(currentProd);
                currentProd = new List<string> { "" };
            }
        }

        // Add final product chunk to the total union collection
        currentUnion.AddRange(currentProd);

        // Deduplicate elements using SortedSet to guarantee unique items in sorted order
        SortedSet<string> uniqueSortedResult = new SortedSet<string>(currentUnion);
        
        return uniqueSortedResult.ToList();
    }
}
