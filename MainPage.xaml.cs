namespace Animal_Matching_Game
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<string> animalEmoji = [
     
                "🧛‍","🧛‍",
                "🐍","🐍",
                "🐌","🐌",
                "🐱‍🐉","🐱‍🐉",
                "🐱‍👓","🐱‍👓",
                "👻","👻",
                "🙉","🙉",
                "🐶","🐶"

            ];

            foreach (var button in AnimalButtons.Children.OfType<Button>()) 
            { 
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTrick);

        }

        int tenthOfSecondsElapsed = 0;
        private bool TimerTrick()
        {
            if (!this.IsLoaded) return false;
            tenthOfSecondsElapsed++;

            TimeElapsed.Text = "Time Elapsed: " +
                (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            if (PlayAgainButton.IsVisible) 
            {
                tenthOfSecondsElapsed = 0;
                return false;
            }
            return true;
    
        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked) 
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false)) 
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;    
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text)
                        && (!string.IsNullOrWhiteSpace(buttonClicked.Text)))
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}
