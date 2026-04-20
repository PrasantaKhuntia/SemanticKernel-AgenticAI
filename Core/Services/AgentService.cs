using SemanticKernel_AgenticAI.Core.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SemanticKernel_AgenticAI.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly Kernel _kernel;

        public AgentService(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<string> AskAsync(string input)
        {
            var executionSettings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var result = await _kernel.InvokePromptAsync(
                $"User Query: {input}",
                new KernelArguments(executionSettings)
            );

            return result.ToString();
        }
    }
}