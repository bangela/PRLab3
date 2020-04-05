using Acr.UserDialogs;
using ChatBot.Core.Common;
using ChatBot.Core.Interfaces;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading;

namespace ChatBot.Core.ViewModels
{
    public class FirstPageViewModel : MvxNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IChatBotService _ChatBotService;
        private readonly IMvxMainThreadAsyncDispatcher _dispatcher;

        public FirstPageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IUserDialogs userDialogs,
            IChatBotService ChatBotService, IMvxMainThreadAsyncDispatcher dispatcher) : base(provider, navigationService)
        {
            _userDialogs = userDialogs;
            _ChatBotService = ChatBotService;
            _dispatcher = dispatcher;
            Messages = new MvxObservableCollection<Message>();
            _ChatBotService.Initialize("213.226.11.149", 41878);
        }

        #region Properties
        private MvxObservableCollection<Message> _messages;
        public MvxObservableCollection<Message> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        private string _msg;
        public string Msg
        {
            get => _msg;
            set => SetProperty(ref _msg, value);
        }
        #endregion

        #region Commands
        private IMvxCommand _sendCommand;
        public IMvxCommand SendCommand => _sendCommand ?? (_sendCommand = new MvxCommand(Send));
        #endregion

        #region Private methods
        public void Send()
        {
            var Thread = new Thread(new ThreadStart(SendThreadMethod));
            Thread.Start();
            var msg = new Message { Data = _msg, Type = Enums.MessageType.USER };
            Messages.Add(msg);
            Msg = null;
        }

        private void SendThreadMethod()
        {
            var receivedMsgTask = _ChatBotService.Send(_msg);
            receivedMsgTask.Wait();
            var receivedMsg = receivedMsgTask.Result;
            Messages.Add(new Message { Data = receivedMsg, Type = Enums.MessageType.BOT });
        }
        #endregion
    }
}
