namespace StudyBuddy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CurrentItem = Items[0].Items[1];
        }
    }
}
