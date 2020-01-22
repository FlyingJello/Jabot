using System.Threading.Tasks;

namespace Jabot.Slack
{
    public interface ISlackService
    {
        Task BroadcastMessage(string channel, string message);
    }
}
