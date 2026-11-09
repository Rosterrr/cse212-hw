using System;
using System.Text.Json.Serialization;

public class FeatureCollection
{
    [JsonPropertyName("features")]
    public EarthquakeFeature[] Features { get; set; } =
        Array.Empty<EarthquakeFeature>();
}

public class EarthquakeFeature
{
    [JsonPropertyName("properties")]
    public EarthquakeProperties Properties { get; set; } = new();
}

public class EarthquakeProperties
{
    [JsonPropertyName("place")]
    public string Place { get; set; } = string.Empty;

    [JsonPropertyName("mag")]
    public double? Mag { get; set; }
}
