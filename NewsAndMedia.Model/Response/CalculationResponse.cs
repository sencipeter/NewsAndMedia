using System.Text.Json.Serialization;

namespace NewsAndMedia.Model.Response
{
    public class CalculationResponse
    {
        [JsonPropertyName("computed_value")]
        public decimal ComputedValue { get; set; }

        [JsonPropertyName("input_value")]
        public decimal InputValue { get; set; }

        [JsonPropertyName("previous_value")]
        public decimal PreviousValue { get; set; }
    }
}
