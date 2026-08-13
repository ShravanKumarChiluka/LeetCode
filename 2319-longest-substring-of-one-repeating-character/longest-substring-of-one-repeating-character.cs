public class Solution {
    public class Node{
        public char cl;
        public char cr;
        public int ll;
        public int lr;
        public int maxLen;
    }
    private Node[] tree;
    private int n;
    public int[] LongestRepeating(string s, string queryCharacters, int[] queryIndices) {
        n=s.Length;
        tree = new Node[4*n];
        for(int i=0;i<tree.Length;i++){
            tree[i] = new Node();
        }

        Build(s,1,0,n-1);

        int k = queryIndices.Length;
        int[] result = new int[k];

        for(int i=0;i<k;i++){
            Update(1,0,n-1,queryIndices[i], queryCharacters[i]);
            result[i] = tree[1].maxLen;
        }
        return result;
    }

    private void PushUp(int treeIndex, int leftChild, int rightChild, int lenL,int lenR){
        Node node = tree[treeIndex];
        Node left = tree[leftChild];
        Node right = tree[rightChild];

        node.cl = left.cl;
        node.cr = right.cr;

        node.ll = left.ll;
        node.lr = right.lr;

        node.maxLen = Math.Max(left.maxLen,right.maxLen);

        if(left.cr == right.cl){
            if(left.ll == lenL){
                node.ll = left.ll + right.ll;
            }
            if(right.lr == lenR){
                node.lr = right.lr + left.lr;
            }
            node.maxLen = Math.Max(node.maxLen, left.lr + right.ll);
        }
    }

    private void Build(string s, int treeIndex, int l, int r){
        if(l == r){
            tree[treeIndex].cl = s[l];
            tree[treeIndex].cr = s[l];
            tree[treeIndex].ll = 1;
            tree[treeIndex].lr = 1;
            tree[treeIndex].maxLen = 1;
            return;
        }
        int mid = l + (r-l)/2;
        int leftChild = 2*treeIndex;
        int rightChild = 2*treeIndex + 1;

        Build(s,leftChild,l,mid);
        Build(s,rightChild,mid+1,r);

        PushUp(treeIndex, leftChild, rightChild, mid-l+1,r-mid);
    }

    private void Update(int treeIndex, int l, int r, int targetIdx, char val){
        if(l == r){
            tree[treeIndex].cl = val;
            tree[treeIndex].cr = val;
            return;
        }

        int mid = l+(r-l)/2;
        int leftChild = 2*treeIndex;
        int rightChild = 2*treeIndex+1;

        if(targetIdx <=mid){
            Update(leftChild,l,mid,targetIdx,val);
        }
        else{
            Update(rightChild,mid+1,r,targetIdx,val);
        }

        PushUp(treeIndex,leftChild,rightChild,mid-l+1,r-mid);
    }
}