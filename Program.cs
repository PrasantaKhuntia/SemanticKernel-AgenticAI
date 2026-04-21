using SemanticKernel_AgenticAI.Core.Interfaces;
using SemanticKernel_AgenticAI.Core.Models;
using SemanticKernel_AgenticAI.Core.Services;
using SemanticKernel_AgenticAI.Infrastructure;
using SemanticKernel_AgenticAI.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using SemanticKernel_AgenticAI.Core.Planner;
using SemanticKernel_AgenticAI.Core.Memory;

class Program
{
    static async Task Main(string[] args)
    {
        // Load config
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = config.GetSection("OpenAI").Get<OpenAISettings>();

        var kernel = KernelFactory.CreateKernel(settings);

        // Register plugins
        kernel.Plugins.AddFromObject(new WeatherPlugin());
        kernel.Plugins.AddFromObject(new ComparisonPlugin());

        // Create Memory
        var chatContextBuilder = new ChatContextBuilder();

        // Create planner
        var planner = new AgentPlanner(kernel, chatContextBuilder);

        // Create agent
        IAgentService agent = new AgentService(planner);

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