using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> RemoveInvalidParentheses(string s) {
        List<string> result = new List<string>();
        if(s == null) return result;

        Queue<string> queue = new Queue<string>();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue(s);
        visited.Add(s);

        bool foundValidAtThisLevel = false;

        while(queue.Count > 0){
            int levelSize = queue.Count;

            for(int k = 0; k < levelSize; k++){
                string current = queue.Dequeue();

                if(isValid(current)){
                    result.Add(current);
                    foundValidAtThisLevel = true;
                }
                if(foundValidAtThisLevel) continue;

                for(int i = 0; i < current.Length; i++){
                    if(current[i] != '(' && current[i] != ')') continue;
                    string nextString = current.Substring(0,i) + current.Substring(i+1);

                    if(!visited.Contains(nextString)){
                        visited.Add(nextString);
                        queue.Enqueue(nextString);
                    }
                }
            }
            if(foundValidAtThisLevel){
                break;
            }
        }
        return result;
    }

    private bool isValid(string s){
        int count = 0;
        foreach(char c in s){
            if(c == '('){
                count++;
            }
            else if(c == ')'){
                count--;
                if(count < 0) return false;
            }
        }
        return count == 0;
    }
}