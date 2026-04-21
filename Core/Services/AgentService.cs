using SemanticKernel_AgenticAI.Core.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel_AgenticAI.Core.Planner;

namespace SemanticKernel_AgenticAI.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly AgentPlanner _planner;

        public AgentService(AgentPlanner planner)
        {
            _planner = planner;
        }

        public async Task<string> AskAsync(string input)
        {
            return await _planner.ExecuteAsync(input);
        }
    }
}