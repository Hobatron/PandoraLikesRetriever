using System.Text.Json.Serialization;

namespace PandoraLikesRetriever;

public class PandoraFeedbackResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("feedback")]
    public List<FeedbackItem> Feedback { get; set; } = new();
}

public class FeedbackItem
{
    [JsonPropertyName("feedbackId")]
    public string FeedbackId { get; set; } = string.Empty;

    [JsonPropertyName("feedbackDateCreated")]
    public DateTime FeedbackDateCreated { get; set; }

    [JsonPropertyName("isPositive")]
    public bool IsPositive { get; set; }

    [JsonPropertyName("stationId")]
    public string StationId { get; set; } = string.Empty;

    [JsonPropertyName("stationName")]
    public string StationName { get; set; } = string.Empty;

    [JsonPropertyName("stationType")]
    public string StationType { get; set; } = string.Empty;

    [JsonPropertyName("musicId")]
    public string MusicId { get; set; } = string.Empty;

    [JsonPropertyName("pandoraId")]
    public string PandoraId { get; set; } = string.Empty;

    [JsonPropertyName("songTitle")]
    public string SongTitle { get; set; } = string.Empty;

    [JsonPropertyName("albumTitle")]
    public string AlbumTitle { get; set; } = string.Empty;

    [JsonPropertyName("artistName")]
    public string ArtistName { get; set; } = string.Empty;

    [JsonPropertyName("artistSeoToken")]
    public string ArtistSeoToken { get; set; } = string.Empty;

    [JsonPropertyName("artistDetailUrl")]
    public string ArtistDetailUrl { get; set; } = string.Empty;

    [JsonPropertyName("trackSeoToken")]
    public string TrackSeoToken { get; set; } = string.Empty;

    [JsonPropertyName("trackDetailUrl")]
    public string TrackDetailUrl { get; set; } = string.Empty;

    [JsonPropertyName("albumSeoToken")]
    public string AlbumSeoToken { get; set; } = string.Empty;

    [JsonPropertyName("trackNum")]
    public int TrackNum { get; set; }

    [JsonPropertyName("trackLength")]
    public int TrackLength { get; set; }

    [JsonPropertyName("albumArt")]
    public List<AlbumArt> AlbumArt { get; set; } = new();
}

public class AlbumArt
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("size")]
    public int Size { get; set; }
}
