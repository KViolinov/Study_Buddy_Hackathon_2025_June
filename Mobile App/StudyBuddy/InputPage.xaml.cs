using StudyBuddy.Data;
using StudyBuddy.Data.Models;
using StudyBuddy.Data.Models.SubModels;
using StudyBuddy.Data.Models.SubModels.FlashcardModels;
using StudyBuddy.Data.Models.SubModels.Quiz;
using StudyBuddy.Data.Models.SubModels.QuizModels;
using System.Threading.Tasks;

namespace StudyBuddy;

public partial class InputPage : ContentPage
{
    private Service _service { get; set; }
    private string _type { get; set; }
	public InputPage()
	{
		InitializeComponent();
        _service = new Service();
        _type = "summary";
        SummaryBtn.BackgroundColor = Color.FromHex("#044179");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        ResightBtn.BackgroundColor = Color.FromHex("#0769C3");

    }
    private async void ClickedSummaryButton(object sender, EventArgs e)
    {
        _type = "summary";
        SummaryBtn.BackgroundColor = Color.FromHex("#044179");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        ResightBtn.BackgroundColor = Color.FromHex("#0769C3");
    }
    private async void ClickedQuizButton(object sender, EventArgs e)
    {
        _type = "quiz";
        SummaryBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#044179");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        ResightBtn.BackgroundColor = Color.FromHex("#0769C3");
    }
    private void ClickedFlashCardButton(object sender, EventArgs e)
    {
        _type = "flashcards";
        SummaryBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#044179");
        ResightBtn.BackgroundColor = Color.FromHex("#0769C3");
    }
    private async void ClickedResightButton(object sender, EventArgs e)
    {
        _type = "resight";
        SummaryBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        ResightBtn.BackgroundColor = Color.FromHex("#044179");

        await DisplayAlert("Sorry", "We did not have enough time for it.", "Ok");

        _type = "quiz";
        SummaryBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#044179");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        ResightBtn.BackgroundColor = Color.FromHex("#0769C3");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        SelectPanel.IsVisible = !SelectPanel.IsVisible;
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        //Setting up the InputModel
        InputModel inputModel = new InputModel();
        inputModel.inputText = TextEntryTxt.Text;
        inputModel.type = _type;
        inputModel.userId = 1;
        inputModel.inputSourceType = "normal text";

        OutputModel outputModel = await _service.InputOutputTransfer(inputModel);
        int points = 0;
        if(_type == "quiz")
        {
            List<Quiz> quizzes = (List<Quiz>)outputModel.data.parsed_object;
            await Navigation.PushAsync(new QuizPage(quizzes));
        }
        else if(_type == "summary")
        {
            Summary output = (Summary)outputModel.data.parsed_object;
            await Navigation.PushAsync(new SummaryPage(output.summary));    
        }
        else if(_type == "flashcards")
        {
            FlashcardsCollection collection = (FlashcardsCollection)outputModel.data.parsed_object;
            await Navigation.PushAsync(new FlashCardPage(collection));
        }
            Console.WriteLine();
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        await DisplayAlert("Error", "Feature not implemented!", "Ok");
    }
}