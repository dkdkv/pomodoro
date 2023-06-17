namespace Pomodoro
{
    public partial class SettingsPage : ContentPage
    {
        private const string WorkTimeKey = "workTime";
        private const string BreakTimeKey = "breakTime";

        public SettingsPage()
        {
            InitializeComponent();

            // Set initial values for sliders and display labels
            WorkTimeSlider.Value = Preferences.Get(WorkTimeKey, 25);
            BreakTimeSlider.Value = Preferences.Get(BreakTimeKey, 5);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            WorkTimeLabel.Text = $"{WorkTimeSlider.Value:0} minutes";
            BreakTimeLabel.Text = $"{BreakTimeSlider.Value:0} minutes";
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Save new preferences
            Preferences.Set(WorkTimeKey, (int)WorkTimeSlider.Value);
            Preferences.Set(BreakTimeKey, (int)BreakTimeSlider.Value);

            // Navigate back to main page
            await Shell.Current.GoToAsync("//MainPage");
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateLabels();
        }
    }
}