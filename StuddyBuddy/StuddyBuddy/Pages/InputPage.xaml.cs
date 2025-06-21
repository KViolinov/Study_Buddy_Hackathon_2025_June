namespace StuddyBuddy.Pages;

public partial class InputPage : ContentPage
{
	public InputPage()
	{
        InitializeComponent();
    }

 
    private async void ClickedSummaryButton(object sender, EventArgs e)
    {
        
    }
    private async void ClickedQuizButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Quiz());
    }
    private void ClickedFlashCardButton(object sender, EventArgs e)
    {

    }
    private void ClickedResightButton(object sender, EventArgs e)
    {

    }
}