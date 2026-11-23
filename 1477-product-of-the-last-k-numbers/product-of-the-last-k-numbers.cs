using System.Collections.Generic;

public class ProductOfNumbers {
    private List<int> prefixProducts;

    public ProductOfNumbers() {
        // Initialize with 1 as a neutral base for multiplication
        prefixProducts = new List<int> { 1 };
    }
    
    public void Add(int num) {
        if (num == 0) {
            // Reset the prefix list when 0 is encountered
            prefixProducts = new List<int> { 1 };
        } else {
            // Multiply the new number by the last running product
            int lastProduct = prefixProducts[prefixProducts.Count - 1];
            prefixProducts.Add(lastProduct * num);
        }
    }
    
    public int GetProduct(int k) {
        int n = prefixProducts.Count;
        
        // If k is greater than or equal to the current list size,
        // a 0 was added within the last k numbers.
        if (k >= n) {
            return 0;
        }
        
        // Divide the latest product by the product k steps prior
        return prefixProducts[n - 1] / prefixProducts[n - 1 - k];
    }
}
