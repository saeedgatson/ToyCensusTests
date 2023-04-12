using System.Text.Json.Serialization;

namespace ToyCensusTests.Model
{
    // CensusToyResponse myDeserializedClass = JsonSerializer.Deserialize<List<CensusToyResponse>>(response);
    public class CensusToyResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
