using Acr.UserDialogs;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Services;
using MvvmCross.IoC;

namespace ChatBot.Core
{
    public static class SetupIoC
    {
        public static IMvxIoCProvider RegisterDependencies(IMvxIoCProvider provider)
        {
            provider.LazyConstructAndRegisterSingleton<IChatBotService, ChatBotService>();
            provider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            return provider;
        }
    }
}
