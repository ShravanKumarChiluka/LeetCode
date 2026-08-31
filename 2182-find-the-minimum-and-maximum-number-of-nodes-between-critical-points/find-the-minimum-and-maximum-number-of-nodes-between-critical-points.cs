/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public int[] NodesBetweenCriticalPoints(ListNode head) {
        if(head == null || head.next == null || head.next.next == null){
            return new int[] {-1,-1};
        }

        int minDistance = int.MaxValue;
        int firstCriticalIndex = -1;
        int prevCriticalIndex = -1;

        ListNode prev = head;
        ListNode curr = head.next;
        int currentIndex = 1;

        while(curr.next != null){
            ListNode nextNode = curr.next;

            bool isLocalMaxima = curr.val > prev.val && curr.val > nextNode.val;
            bool isLocalMinima = curr.val < prev.val && curr.val < nextNode.val;

            if(isLocalMaxima || isLocalMinima){
                if(firstCriticalIndex == -1){
                    firstCriticalIndex = currentIndex;
                }
                else{
                    minDistance = Math.Min(minDistance, currentIndex - prevCriticalIndex);
                }
                prevCriticalIndex = currentIndex;
            }
            prev = curr;
            curr = nextNode;
            currentIndex++;
        }

        if(firstCriticalIndex == prevCriticalIndex){
            return new int[] {-1,-1};
        }
        int maxDistance = prevCriticalIndex - firstCriticalIndex;
        return new int[] {minDistance,maxDistance};
    }
}