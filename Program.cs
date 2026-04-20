using SemanticKernel_AgenticAI.Core.Interfaces;
using SemanticKernel_AgenticAI.Core.Models;
using SemanticKernel_AgenticAI.Core.Services;
using SemanticKernel_AgenticAI.Infrastructure;
using SemanticKernel_AgenticAI.Plugins;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        // Load config
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = config.GetSection("OpenAI").Get<OpenAISettings>();

        // Create Kernel
        var kernel = KernelFactory.CreateKernel(settings);

        // Create dependencies manually
        var httpClient = new HttpClient();

        var weatherPlugin = new WeatherPlugin();
        var comparisonPlugin = new ComparisonPlugin();

        var orchestrator = new AgentOrchestrator(weatherPlugin, comparisonPlugin);

        IAgentService agent = new AgentService(kernel);

        Console.WriteLine("Agent is ready. Type 'exit' to quit.");

        while (true)
        {
            Console.Write("\nYou: ");
            var input = Console.ReadLine();

            if (input?.ToLower() == "exit")
                break;

            var response = await agent.AskAsync(input);

            Console.WriteLine($"Agent: {response}");
        }
    }
}