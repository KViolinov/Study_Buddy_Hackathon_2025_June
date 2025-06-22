using StudyBuddy.Data;
using StudyBuddy.Data.Models;
using StudyBuddy.Data.Models.SubModels.Quiz;
using StudyBuddy.Data.Models.SubModels.QuizModels;

namespace StudyBuddy;

public partial class InputPage : ContentPage
{
    private Service _service { get; set; }
    private string _type { get; set; }
	public InputPage()
	{
		InitializeComponent();
        _service = new Service();
	}
    private async void ClickedSummaryButton(object sender, EventArgs e)
    {
        _type = "summary";
        SummaryBtn.BackgroundColor = Color.FromHex("#044179");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        FlashCardsBtn.BackgroundColor = Color.FromHex("#0769C3");
        //ResightBtn.BackgroundColor = Color.FromHex("#0769C3");
    }
    private async void ClickedQuizButton(object sender, EventArgs e)
    {
        _type = "quiz";
        QuizBtn.BackgroundColor = Color.FromHex("#044179");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
        QuizBtn.BackgroundColor = Color.FromHex("#0769C3");
    }
    private void ClickedFlashCardButton(object sender, EventArgs e)
    {
        _type = "flashcards";
    }
    private void ClickedResightButton(object sender, EventArgs e)
    {
        _type = "resight";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        InputModel inputModel = new InputModel();
        inputModel.inputText = TextEntryTxt.Text;
        inputModel.type = _type;
        inputModel.userId = 1;
        inputModel.inputSourceType = "normal text";

        OutputModel outputModel = await _service.InputOutputTransfer(inputModel);
        QuizModel quizzes = (QuizModel)outputModel.data.parsed_object;
        int points = 0;
        if(_type == "quiz")
        {
            await Navigation.PushAsync(new QuizPage(quizzes));
        }
        Console.WriteLine();
    }

}