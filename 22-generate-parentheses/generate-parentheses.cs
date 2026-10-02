using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> GenerateParenthesis(int n) {
        List<string> result = new List<string>();
        Backtrack(result,"",0,0,n);
        return result;
    }
    public void Backtrack(List<string> result, string currentString,int openCount, int closeCount, int max){
        if(currentString.Length == max * 2){
            result.Add(currentString);
            return;
        }

        if(openCount < max){
            Backtrack(result,currentString + "(", openCount + 1, closeCount,max);
        }
        if(closeCount < openCount){
            Backtrack(result,currentString + ")", openCount, closeCount + 1, max);
        }
    }
}