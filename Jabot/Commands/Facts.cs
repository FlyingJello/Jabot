using System.Threading.Tasks;
using Jabot.Slack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Jabot.Commands
{
    public class Facts
    {
        private readonly ISlackService _slack;

        public Facts(ISlackService slack)
        {
            _slack = slack;
        }

        [FunctionName("Facts")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string channel = req.Form["channel_id"];
            string userId = req.Form["user_id"];

            log.LogInformation($"User '{userId}' requested a fact in channel '{channel}'");

            await _slack.BroadcastMessage(channel, "FUN FACT : 100% DES JABONAIS SONT NOIRS");

            return new OkObjectResult(userId);
        }
    }
}
