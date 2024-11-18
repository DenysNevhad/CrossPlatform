using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        string[] input = File.ReadAllText("INPUT.txt").Split();
        int B1 = int.Parse(input[0]);
        int B2 = int.Parse(input[1]);
        int B3 = int.Parse(input[2]);
        int T = int.Parse(input[3]);

        string result = SolveBuckets(B1, B2, B3, T);

        File.WriteAllText("OUTPUT.txt", result);
    }
    public static string SolveBuckets(int B1, int B2, int B3, int T)
    {
        if (T > B1 || T < 0 || GCD(B1, GCD(B2, B3)) != 1 && T % GCD(B1, GCD(B2, B3)) != 0)
        {
            return "IMPOSSIBLE";
        }

        var queue = new Queue<(int, int, int, int)>();
        var visited = new HashSet<(int, int, int)>();
        queue.Enqueue((B1, 0, 0, 0));
        visited.Add((B1, 0, 0));

        while (queue.Count > 0)
        {
            var (x, y, z, steps) = queue.Dequeue();

            if (x == T)
            {
                return steps.ToString();
            }

            foreach (var nextState in GetNextStates(x, y, z, B1, B2, B3))
            {
                if (!visited.Contains(nextState))
                {
                    visited.Add(nextState);
                    queue.Enqueue((nextState.Item1, nextState.Item2, nextState.Item3, steps + 1));
                }
            }
        }

        return "IMPOSSIBLE";
    }

    static List<(int, int, int)> GetNextStates(int x, int y, int z, int B1, int B2, int B3)
    {
        var nextStates = new List<(int, int, int)>();
        int pour;

        pour = Math.Min(x, B2 - y);
        nextStates.Add((x - pour, y + pour, z));

        pour = Math.Min(x, B3 - z);
        nextStates.Add((x - pour, y, z + pour));

        pour = Math.Min(y, B1 - x);
        nextStates.Add((x + pour, y - pour, z));

        pour = Math.Min(y, B3 - z);
        nextStates.Add((x, y - pour, z + pour));

        pour = Math.Min(z, B1 - x);
        nextStates.Add((x + pour, y, z - pour));

        pour = Math.Min(z, B2 - y);
        nextStates.Add((x, y + pour, z - pour));

        return nextStates;
    }

    static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
}
