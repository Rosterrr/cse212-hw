using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Identical letters cannot form a symmetric pair.
            if (word[0] == word[1])
                continue;

            // Process each distinct word only once.
            if (!seen.Add(word))
                continue;

            var reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
                pairs.Add($"{reversed} & {word}");
        }

        return pairs.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var fields = line.Split(',');
            var degree = fields[3].Trim();

            degrees.TryGetValue(degree, out var count);
            degrees[degree] = count + 1;
        }

        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        var counts = new Dictionary<char, int>();

        foreach (var character in word1)
        {
            if (character == ' ')
                continue;

            var letter = char.ToLowerInvariant(character);

            counts.TryGetValue(letter, out var count);
            counts[letter] = count + 1;
        }

        foreach (var character in word2)
        {
            if (character == ' ')
                continue;

            var letter = char.ToLowerInvariant(character);

            if (!counts.TryGetValue(letter, out var count))
                return false;

            if (count == 1)
                counts.Remove(letter);
            else
                counts[letter] = count - 1;
        }

        return counts.Count == 0;
    }

    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = client.Send(request);

        response.EnsureSuccessStatusCode();

        using var jsonStream = response.Content.ReadAsStream();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(
                jsonStream, options);

        if (featureCollection?.Features == null)
            return Array.Empty<string>();

        var summaries = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            var place =
                feature.Properties?.Place ?? "Unknown location";

            var magnitude =
                feature.Properties?.Mag?.ToString(
                    CultureInfo.InvariantCulture) ?? "Unknown";

            summaries.Add($"{place} - Mag {magnitude}");
        }

        return summaries.ToArray();
    }
}
