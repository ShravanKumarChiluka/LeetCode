using System;

public class Solution {
    public bool CheckOverlap(int radius, int xCenter, int yCenter, int x1, int y1, int x2, int y2) {
        // Find the closest x-coordinate on the rectangle to the circle's center
        int closestX = Math.Clamp(xCenter, x1, x2);
        
        // Find the closest y-coordinate on the rectangle to the circle's center
        int closestY = Math.Clamp(yCenter, y1, y2);
        
        // Calculate the distance vector components from the circle center to this closest point
        int distanceX = xCenter - closestX;
        int distanceY = yCenter - closestY;
        
        // Check if the squared distance is less than or equal to the squared radius
        return (distanceX * distanceX) + (distanceY * distanceY) <= (radius * radius);
    }
}
