using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace Linquiztic.Service
{
    public class AIService
    {
        private readonly ChatClient _client;

        public AIService()
        {
            var endpoint = new Uri("https://models.github.ai/inference");
            var credential = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            var model = "openai/gpt-4o";

            var options = new OpenAIClientOptions
            {
                Endpoint = endpoint
            };

            _client = new ChatClient(model, new ApiKeyCredential(credential), options);
        }
        public async Task<string> FetchAiResponse(string language,string level,string words)
        {

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful assistant."),
                new UserChatMessage($"language:{language} , sentence: get new 10 vocab for level {level} outside following words {words}")
            };

            var requestOptions = new ChatCompletionOptions
            {
                Temperature = 1.0f,
                TopP = 1.0f,
                MaxOutputTokenCount = 1000
            };

            var response = _client.CompleteChat(messages, requestOptions);
            return response.Value.Content[0].Text;
        }
    }
}
