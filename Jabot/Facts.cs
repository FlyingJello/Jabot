using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Jabot
{
    public static class Facts
    {
        [FunctionName("Facts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Fact Requested");

            if (!req.Form.TryGetValue("channel_id", out var channel) ||
                !req.Form.TryGetValue("user_id", out var userId))
            {
                return new BadRequestErrorMessageResult("Y0U N33D 4 F0RM");
            }

            await BroadcastMessage(channel, "FUN FACT : 100% DES JABONAIS SONT NOIRS");

            return new OkObjectResult(userId);
        }

        private static async Task BroadcastMessage(string channel, string text)
        {
            var botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");

            var payload = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", botToken),
                new KeyValuePair<string, string>("channel", channel),
                new KeyValuePair<string, string>("text", text)
            };

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage") { Content = new FormUrlEncodedContent(payload) };
            await httpClient.SendAsync(request);
        }
    }
}
