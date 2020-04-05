using ChatBot.Core;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.IoC;

namespace ChatBot.UWP
{
    public class Setup : MvxFormsWindowsSetup<ChatBot.Core.App, ChatBot.App>
    {
        protected override IMvxIoCProvider InitializeIoC()
        {
            var provider = base.InitializeIoC();
            return SetupIoC.RegisterDependencies(provider);
        }
    }
}
