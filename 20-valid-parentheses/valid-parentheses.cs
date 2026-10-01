using System;
using System.Collections.Generic;

public class Solution {
    public bool IsValid(string s) {
        Stack<char> deck = new Stack<char>();
        if(s.Length%2 != 0) return false;
        foreach(char ele in s){
            if(ele == '(' || ele == '[' || ele == '{'){
                deck.Push(ele);
            }
            if(ele == ')'){
                if(deck.Count == 0 || deck.Pop() != '(') return false;
            }
            if(ele == ']'){
                if(deck.Count == 0 || deck.Pop() != '[') return false;
            }
            if(ele == '}'){
                if(deck.Count == 0 || deck.Pop() != '{') return false;
            }
        }
        return deck.Count == 0;
    }
}