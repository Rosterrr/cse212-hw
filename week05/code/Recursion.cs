using System;
using System.Collections.Generic;

public static class Recursion
{
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }

    public static void PermutationsChoose(List<string> results, string letters,
        int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        foreach (char letter in letters)
        {
            if (!word.Contains(letter))
                PermutationsChoose(results, letters, size, word + letter);
        }
    }

    public static decimal CountWaysToClimb(int s,
        Dictionary<int, decimal>? remember = null)
    {
        if (s <= 0) return 0;
        if (s <= 2) return s;
        if (s == 3) return 4;

        remember ??= new Dictionary<int, decimal>();
        if (remember.TryGetValue(s, out decimal cached))
            return cached;

        // All three branches share the same dictionary.
        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);
        remember[s] = ways;
        return ways;
    }

    public static void WildcardBinary(string pattern, List<string> results)
    {
        int star = pattern.IndexOf('*');
        if (star < 0)
        {
            results.Add(pattern);
            return;
        }

        string before = pattern[..star];
        string after = pattern[(star + 1)..];
        WildcardBinary(before + '0' + after, results);
        WildcardBinary(before + '1' + after, results);
    }

    public static void SolveMaze(List<string> results, Maze maze,
        int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        currPath ??= new List<(int, int)>();
        if (!maze.IsValidMove(currPath, x, y))
            return;

        currPath.Add((x, y));
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            SolveMaze(results, maze, x - 1, y, currPath);
            SolveMaze(results, maze, x + 1, y, currPath);
            SolveMaze(results, maze, x, y - 1, currPath);
            SolveMaze(results, maze, x, y + 1, currPath);
        }

        // Other routes may use this square after this branch returns.
        currPath.RemoveAt(currPath.Count - 1);
    }
}
