using System.Threading.Tasks;

namespace StudyBuddy
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            string url = "https://kvb-bg.com/Study_Buddy/";
            await Launcher.OpenAsync(url);
        }
    }

}
