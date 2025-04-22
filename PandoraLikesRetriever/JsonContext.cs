using System.Text.Json.Serialization;

namespace PandoraLikesRetriever;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(PandoraFeedbackResponse))]
internal partial class JsonContext : JsonSerializerContext
{
}
