using System.Threading.Tasks;
using Jabot.Slack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Jabot.Commands
{
    public class Say
    {
        private readonly ISlackService _slack;

        public Say(ISlackService slack)
        {
            _slack = slack;
        }

        [FunctionName("Say")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string channel = req.Form["channel_id"];
            string userId = req.Form["user_id"];
            string text = req.Form["text"];

            log.LogInformation($"User {userId} said {text}");

            await _slack.BroadcastMessage(channel, text);

            return new OkResult();
        }
    }
}
