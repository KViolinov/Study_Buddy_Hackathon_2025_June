using StudyBuddy.Data.Models.SubModels.FlashcardModels;
using System.Threading.Tasks;

namespace StudyBuddy;

public partial class FlashCardPage : ContentPage
{
	private int counter = 0;
	private string answear {  get; set; }
	public FlashcardsCollection _collection {  get; set; }

	public FlashCardPage(FlashcardsCollection flashcards)
	{
		InitializeComponent();
		answear = "";
		_collection = new FlashcardsCollection();
		_collection = flashcards;
		if(flashcards != null
			&& flashcards.Flashcards.Count > counter)
		{
			FlashcardItem model = flashcards.Flashcards[counter];
			QuestionTxt.Text = model.Question;
			answear = model.Answer;
		}

	}

	public async void FlashCardLogic(FlashcardsCollection flashcards)
	{
        if (flashcards != null
            && flashcards.Flashcards.Count > counter)
        {
            FlashcardItem model = flashcards.Flashcards[counter];
            QuestionTxt.Text = model.Question;
            answear = model.Answer;
        }
		else
		{
			await DisplayAlert("Finished", "End of AI responses", "Ok");
			await Navigation.PopAsync();
		}
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
		ResultTxt.Text = answear;
    }

    private async void Next_Clicked(object sender, EventArgs e)
    {
        ResultTxt.Text = answear;
		await Task.Delay(500);
		counter++;
        FlashCardLogic(_collection);
    }
}