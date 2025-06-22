using System.Threading.Tasks;

namespace StudyBuddy;

public partial class SummaryPage : ContentPage
{
	public SummaryPage(string text)
	{
		InitializeComponent();
		TestResultTxt.Text = text;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}