using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int[] Intersection(int[] nums1, int[] nums2) {
        int[] inter = new int[1];
        List<int> interList = inter.ToList();
        interList.RemoveAt(0);
        foreach(int num in nums1){
            if(nums2.Contains(num) && (!interList.Contains(num))){
                interList.Add(num);
            }
        }

        return interList.ToArray();
    }
}