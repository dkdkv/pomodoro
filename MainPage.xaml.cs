using System.Timers;
using Timer = System.Timers.Timer;

namespace Pomodoro
{
    public partial class MainPage : ContentPage
    {
        private const string WorkTimeKey = "workTime";
        private const string BreakTimeKey = "breakTime";
        private Timer _timer;
        private readonly TimeSpan _workTime;  // 25 minutes
        private TimeSpan _breakTime;  // 5 minutes
        private TimeSpan _currentTime;
        private readonly bool _isBreakTime = false;

        public MainPage()
        {
            InitializeComponent();

            _workTime = TimeSpan.FromMinutes(Preferences.Get(WorkTimeKey, 25));
            _breakTime = TimeSpan.FromMinutes(Preferences.Get(BreakTimeKey, 5));
            _currentTime = _workTime;

            StartButton.Text = "Let's work";

            UpdateTimerLabel();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            if (_timer != null && _timer.Enabled) return; // Если таймер уже запущен, ничего не делаем

            _currentTime = _workTime; // Время работы

            _timer = new Timer(1000);  // интервалы в 1 секунду
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();

            UpdateTimerLabel();
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            if (_timer == null || !_timer.Enabled) return; // Если таймер уже остановлен, ничего не делаем

            _timer.Stop();
            _timer = null;
            _currentTime = _workTime; // Сброс до времени работы
            UpdateTimerLabel();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            UpdateTimerLabel();

            if (!(_currentTime.TotalSeconds <= 0)) return;
            _timer.Stop();

            StartButton.Text = _isBreakTime ? "Let's work" : "Time to rest";
        }

        private void UpdateTimerLabel()
        {
            Dispatcher.Dispatch(() =>
            {
                TimerLabel.Text = $"{(int)_currentTime.TotalMinutes:00}:{_currentTime.Seconds:00}";
                StatusLabel.Text = _isBreakTime ? "Break Time" : "Work Time";
            });
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }
    }
}