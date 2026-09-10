/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    private int matchingNodesCount = 0;
    public int AverageOfSubtree(TreeNode root) {
        matchingNodesCount = 0;
        CalculateSubtreeSumAndCount(root);
        return matchingNodesCount;
    }
    private(int sum,int count) CalculateSubtreeSumAndCount(TreeNode node){
        if(node == null){
            return (0,0);
        }

        var leftResult = CalculateSubtreeSumAndCount(node.left);
        var rightResult = CalculateSubtreeSumAndCount(node.right);

        int totalSum = leftResult.sum + rightResult.sum + node.val;
        int totalCount = leftResult.count + rightResult.count + 1;

        if(totalSum / totalCount == node.val){
            matchingNodesCount++ ;
        }
        return (totalSum,totalCount);
    }
}