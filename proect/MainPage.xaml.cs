namespace proect
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Основные данные
            var name = Preferences.Get("user_name", "Не указано");
            var height = Preferences.Get("user_height", "Не указан");
            var weight = Preferences.Get("user_weight", "Не указан");
            var age = Preferences.Get("user_age", "Не указан");

            UserNameLabel.Text = $"Имя: {name}";
            UserHeightLabel.Text = $"Рост: {height} см";
            UserWeightLabel.Text = $"Вес: {weight} кг";
            UserAgeLabel.Text = $"Возраст: {age} лет";

            // Данные прогресса
            var currentWeight = Preferences.Get("current_weight", "Не указан");
            var currentHeight = Preferences.Get("current_height", "Не указан");
            var measurementDate = Preferences.Get("measurement_date", "Не указана");

            CurrentWeightLabel.Text = $"Текущий вес: {currentWeight} кг";
            CurrentHeightLabel.Text = $"Текущий рост: {currentHeight} см";
            MeasurementDateLabel.Text = $"Дата измерения: {measurementDate}";
        }

        private void OnSaveNameClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                if (!IsValidName(NameEntry.Text))
                {
                    DisplayAlert("Ошибка", "Имя должно содержать только буквы", "OK");
                    return;
                }

                Preferences.Set("user_name", NameEntry.Text);
                NameEntry.Text = "";
                LoadUserData();
            }
        }

        // Навигация на страницу прогресса
        private async void OnGoToProgressClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ProgressPage");
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private async void ongosetting(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }

        private bool IsValidName(string name)
        {
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUserData();
        }
    }
}