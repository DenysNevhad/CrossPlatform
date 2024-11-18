using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        string inputPath = "INPUT.txt";
        string outputPath = "OUTPUT.txt";

        string[] lines = File.ReadAllLines(inputPath);
        long[] results = new long[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            results[i] = CountDecodingWays(lines[i].Trim());
        }

        File.WriteAllLines(outputPath, Array.ConvertAll(results, r => r.ToString()));
    }

    public static long CountDecodingWays(string digits)
    {
        if (string.IsNullOrWhiteSpace(digits)) return 0;

        int n = digits.Length;
        long[] dp = new long[n + 1];
        dp[0] = 1;

        for (int i = 1; i <= n; i++)
        {
            if (digits[i - 1] >= '1' && digits[i - 1] <= '9')
            {
                dp[i] += dp[i - 1];
            }

            if (i > 1)
            {
                int twoDigit = int.Parse(digits.Substring(i - 2, 2));
                if (twoDigit >= 10 && twoDigit <= 33)
                {
                    dp[i] += dp[i - 2];
                }
            }

            if (digits[i - 1] == '0')
            {
                if (i > 1 && digits[i - 2] >= '1' && digits[i - 2] <= '9')
                {
                    dp[i] += dp[i - 2];
                }
            }
        }

        return dp[n];
    }
}
