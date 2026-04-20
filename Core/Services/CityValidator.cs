using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel_AgenticAI.Core.Services
{
    public static class CityValidator
    {
        private static readonly List<string> ValidCities = new()
        {
            "Mumbai",
            "Delhi",
            "Pune",
            "Hyderabad",
            "Bangalore",
            "Chennai"
        };

        public static bool TryNormalize(string input, out string normalized)
        {
            normalized = null;

            // Exact match
            var exact = ValidCities
                .FirstOrDefault(c => c.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (exact != null)
            {
                normalized = exact;
                return true;
            }

            // Fuzzy match (simple contains)
            var fuzzy = ValidCities
                .FirstOrDefault(c => c.StartsWith(input[..Math.Min(3, input.Length)], StringComparison.OrdinalIgnoreCase));

            if (fuzzy != null)
            {
                normalized = fuzzy;
                return true;
            }

            return false;
        }
    }
}
