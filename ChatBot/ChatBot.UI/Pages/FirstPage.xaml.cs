using ChatBot.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace ChatBot.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : MvxContentPage<FirstPageViewModel>
    {
        public FirstPage()
        {
            InitializeComponent();
        }
    }
}