using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CloudTgBotCore3
{
    public static class TelegramEndpoint
    {
        [FunctionName("TelegramEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "EndpointName")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Starting to process a message");
            log.LogInformation("Request: {headers}", req.Headers);
            string botApiKey;
            try
            {
                // Gets a variable, in local environment from local.settings, in Azure from Functions environment variables
                botApiKey = Environment.GetEnvironmentVariable("TelegramBotApiKey");
                log.LogInformation(botApiKey);
            }
            catch (Exception)
            {
                log.LogError("No Telegram bot key defined");
                return new InternalServerErrorResult();
            }

            var botClient = new Telegram.Bot.TelegramBotClient(botApiKey);

            string jsonContent = await req.ReadAsStringAsync();

            // Class provided by the Telegram.Bot library
            Update update;
            try
            {
                update = JsonConvert.DeserializeObject<Update>(jsonContent);
                log.LogInformation("Parse succeeded!");
            }
            catch (Exception)
            {
                log.LogError("Parse failed :(");
                return new BadRequestResult();
            }

            var message = update.Message;

            // Handling the message
            if (message.Text == "/moro")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "No moro moro");
                return new OkResult();
            }

            log.LogInformation("Could not process the following message:", jsonContent);

            return new OkResult();
        }
    }
}
