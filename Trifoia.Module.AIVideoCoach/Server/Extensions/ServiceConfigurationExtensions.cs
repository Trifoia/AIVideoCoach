using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using System;
using Azure.AI.Inference;
using Azure;
using Azure.Identity;
using System.ClientModel;
using OpenAI.Embeddings;
using Trifoia.Module.AIVideoCoach.Services;


namespace Trifoia.Module.AIVideoCoach.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection ConfigureClientOptions(this IServiceCollection services)
        {
            // Get configuration settings
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var client = (configuration["AIHost"] ?? "local").ToLower();

            //Sets up Ollama Local Model
            if (client == "local")
            {
                // Register local AI service
                services.AddSingleton<IChatClient, OllamaChatClient>(sp =>
                {
                    var endpoint = configuration["LOCAL_ENDPOINT"] ?? throw new Exception("Missing configuration LOCAL_ENDPOINT");
                    var modelName = configuration["LOCAL_MODEL_NAME"] ?? throw new Exception("Missing configuration LOCAL_MODEL_NAME");
                    return new OllamaChatClient(endpoint, modelName);
                });
            }
            else if (client == "openai")
            {
                // Register OpenAI client
                services.AddSingleton<IChatClient>(sp =>
                {
                    var apiKey = configuration["OPENAI_KEY"]
                                 ?? throw new Exception("Missing configuration OPENAI_KEY");
                    var modelId = configuration["REMOTE_MODEL_OR_DEPLOYMENT_ID"]
                                  ?? throw new Exception("Missing configuration REMOTE_MODEL_OR_DEPLOYMENT_ID");

                    // Create OpenAIClient and cast it to IChatClient
                    return new OpenAIClient(apiKey).AsChatClient(modelId);
                });
            }
            else if (client == "azureopenai")
            {
                services.AddSingleton<IChatClient>(sp =>
                {
                    var endpoint = configuration["AZURE_OPENAI_ENDPOINT"]
                        ?? throw new Exception("Missing configuration: AZURE_OPENAI_ENDPOINT");
                    var deploymentId = configuration["AZURE_OPENAI_DEPLOYMENT"]
                        ?? throw new Exception("Missing configuration: AZURE_OPENAI_DEPLOYMENT");
                    var apiKey = configuration["AZURE_OPENAI_API_KEY"]
                        ?? throw new Exception("Missing configuration: AZURE_OPENAI_API_KEY");

                    // Use ApiKeyCredential from System.ClientModel
                    var openAIClient = new AzureOpenAIClient(
                        new Uri(endpoint),
                        new ApiKeyCredential(apiKey));

                    return openAIClient.AsChatClient(deploymentId);
                });

                // Azure OpenAI Embedding Client
                services.AddSingleton<IEmbeddingClient>(sp =>
                {
                    var endpoint = configuration["AZURE_OPENAI_ENDPOINT"]
                        ?? throw new Exception("Missing configuration: AZURE_OPENAI_ENDPOINT");
                    var deploymentId = configuration["AZURE_EMBEDDING_DEPLOYMENT"]
                        ?? throw new Exception("Missing configuration: AZURE_EMBEDDING_DEPLOYMENT");
                    var apiKey = configuration["AZURE_OPENAI_API_KEY"]
                        ?? throw new Exception("Missing configuration: AZURE_OPENAI_API_KEY");

                    return new AzureOpenAIEmbeddingClient(new Uri(endpoint), deploymentId, apiKey);
                });
            }

            return services;
        }
    }
}

