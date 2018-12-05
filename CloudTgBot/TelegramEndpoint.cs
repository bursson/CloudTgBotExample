using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace CloudTgBot
{
    public static class TelegramEndpoint
    {
        [FunctionName("TelegramEndpoint")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "EndpointName")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("Starting to process a message");

            string botApiKey;
            try
            {
                botApiKey = Environment.GetEnvironmentVariable("TelegramBotApiKey");
            }
            catch (Exception e)
            {
                log.Error("No Telegram bot key defined");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            var botClient = new Telegram.Bot.TelegramBotClient(botApiKey);

            string jsonContent = await req.Content.ReadAsStringAsync();

            // Class provided by the Telegram.Bot library
            Update update;

            try
            {
                update = JsonConvert.DeserializeObject<Update>(jsonContent);
                log.Info("Parse succeeded!");
            }
            catch (Exception e)
            {
                log.Error("Parse failed :(");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var message = update.Message;

            // Handling the message

            if (message.Text == "/moro")
            {
               await  botClient.SendTextMessageAsync(message.Chat.Id, "No moro moro");
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            log.Info("Could not process the following message:");
            log.Info(jsonContent);

            return new HttpResponseMessage(HttpStatusCode.OK);


        }
    }
}
