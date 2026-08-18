public class Solution {
    public IList<int> Intersection(int[][] nums) {
        int[] counts = new int[1001];

        foreach(var row in nums){
            foreach(var num in row){
                counts[num]++;
            }
        }
        List<int> res = new List<int>();
        int len = nums.Length;

        for(int i=0; i<=1000;i++){
            if(counts[i] == len){
                res.Add(i);
            }
        }
        return res;
    }
}