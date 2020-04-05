using System.Threading.Tasks;

namespace ChatBot.Core.Interfaces
{
    public interface IChatBotService
    {
        void Initialize(string proxyHost, int proxyPort, string proxyUsername = null, string proxyPassword = null);
        Task<string> Send(string msg);
    }
}
