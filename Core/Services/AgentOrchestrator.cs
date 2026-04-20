using SemanticKernel_AgenticAI.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel_AgenticAI.Core.Services
{
    public class AgentOrchestrator
    {
        private readonly WeatherPlugin _weather;
        private readonly ComparisonPlugin _comparison;

        public AgentOrchestrator(
            WeatherPlugin weather,
            ComparisonPlugin comparison)
        {
            _weather = weather;
            _comparison = comparison;
        }

        public async Task<string> HandleCompareWeather(string city1, string city2)
        {
            var w1 = await _weather.GetWeather(city1);
            var w2 = await _weather.GetWeather(city2);

            return _comparison.CompareWeather(
                w1.Temperature,
                w2.Temperature,
                city1,
                city2,
                w1.Condition,
                w2.Condition
            );
        }
    }
}
