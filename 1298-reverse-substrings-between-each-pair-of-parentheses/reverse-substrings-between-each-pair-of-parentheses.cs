using System;
using System.Collections.Generic;

public class Solution {
    public string ReverseParentheses(string s) {
        // FIX 1: Change Stack type from string to char
        Stack<char> deck = new Stack<char>();
        
        foreach(char c in s){
            if(c != ')'){
                deck.Push(c);
            }
            else{
                // FIX 2: Reset 'reverse' to empty for each new set of parentheses
                string reverse = String.Empty;
                
                while(deck.Count > 0 && deck.Peek() != '('){
                    reverse += deck.Pop();
                }
                if(deck.Count > 0){
                    deck.Pop(); // Pops the '('
                }
                foreach(char x in reverse){
                    deck.Push(x);
                }
            }
        }

        char[] result = deck.ToArray();
        Array.Reverse(result);
        return new string(result);
    }
}
