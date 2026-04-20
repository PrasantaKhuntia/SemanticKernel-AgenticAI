using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SemanticKernel_AgenticAI.Core.Models
{
    public class CityExtractionResult
    {
        [JsonPropertyName("cities")]
        public List<string> Cities { get; set; } = new();
    }
}
