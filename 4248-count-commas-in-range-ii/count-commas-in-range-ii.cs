public class Solution
{
    public long CountCommas(long n)
    {
        long count = 0;

        long start = 1000;
        long commas = 1;

        while (start <= n)
        {
            long end = start * 1000 - 1;

            if (end > n)
            {
                end = n;
            }

            count += (end - start + 1) * commas;

            start *= 1000;
            commas++;
        }

        return count;
    }
}