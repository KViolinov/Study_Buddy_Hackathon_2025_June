using StudyBuddy.Data.Models;
using StudyBuddy.Data.Models.SubModels.Quiz;
using StudyBuddy.Data.Models.SubModels.QuizModels;
using System.Threading.Tasks;

namespace StudyBuddy;

public partial class QuizPage : ContentPage
{
	public int points {  get; set; }
    public string correctAns { get; set; }
    public int counter { get; set; } = 0;
    public QuizModel _questions { get; set; }

    public QuizPage(QuizModel questions)
    {
        InitializeComponent();
        _questions = questions;
        Question();
    }

    public async Task Question()
	{
        List<Quiz> questionsForThePage = _questions.Quiz.ToList();

        if (questionsForThePage.Count > 0 && questionsForThePage.Count > counter)
        {
            Quiz current = questionsForThePage[counter];
            QuestionLabel.Text = current.Question;
            Option_1.Text = current.Options[0];
            Option_2.Text = current.Options[1];
            Option_3.Text = current.Options[2];
            Option_4.Text = current.Options[3];

            correctAns = current.CorrectAnswer;
            Points.Text = points.ToString();
        }
        else
        {
            await DisplayAlert("Finished", $"You got {points} from {questionsForThePage.Count}", "Ok");
            await Navigation.PopAsync();
        }
    }

    private void Option_1_Clicked(object sender, EventArgs e)
    {
        if(correctAns == Option_1.Text)
        {
            points++;
        }
        counter++;
        Question();
    }

    private void Option_2_Clicked(object sender, EventArgs e)
    {
        if (correctAns == Option_2.Text)
        {
            points++;
        }
        counter++;
        Question();
    }

    private void Option_3_Clicked(object sender, EventArgs e)
    {
        if (correctAns == Option_3.Text)
        {
            points++;
        }
        counter++;
        Question();
    }

    private void Option_4_Clicked(object sender, EventArgs e)
    {
        if (correctAns == Option_4.Text)
        {
            points++;
        }
        counter++;
        Question();
    }
}