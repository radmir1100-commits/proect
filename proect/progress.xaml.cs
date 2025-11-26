namespace proect;

public partial class progress : ContentPage
{
    public progress()
    {
        InitializeComponent();
        LoadSavedProgressData();
        MeasurementDatePicker.Date = DateTime.Now; // Óñòàíàâëèâàåì òåêóùóş äàòó ïî óìîë÷àíèş
    }

    // Çàãğóçêà ñîõğàíåííûõ äàííûõ ïğîãğåññà
    private void LoadSavedProgressData()
    {
        // Çàãğóæàåì òåêóùèå çíà÷åíèÿ â ïîëÿ ââîäà
        CurrentWeightEntry.Text = Preferences.Get("current_weight", "");
        CurrentHeightEntry.Text = Preferences.Get("current_height", "");

        // Çàãğóæàåì äàòó (åñëè åñòü ñîõğàíåííàÿ)
        var savedDate = Preferences.Get("measurement_date", "");
        if (DateTime.TryParse(savedDate, out DateTime date))
        {
            MeasurementDatePicker.Date = date;
        }

        // Îáíîâëÿåì îòîáğàæåíèå ñîõğàíåííûõ äàííûõ
        UpdateSavedDataDisplay();
    }

    // Îáíîâëåíèå îòîáğàæåíèÿ ñîõğàíåííûõ äàííûõ
    private void UpdateSavedDataDisplay()
    {
        var weight = Preferences.Get("current_weight", "Íå óêàçàí");
        var height = Preferences.Get("current_height", "Íå óêàçàí");
        var date = Preferences.Get("measurement_date", "Íå óêàçàíà");

        SavedWeightLabel.Text = $"Âåñ: {weight} êã";
        SavedHeightLabel.Text = $"Ğîñò: {height} ñì";
        SavedDateLabel.Text = $"Äàòà èçìåğåíèÿ: {date}";
    }

    // Ñîõğàíåíèå ïğîãğåññà
    private void OnSaveProgressClicked(object sender, EventArgs e)
    {
        ProgressLabel.TextColor = Colors.Red;

        // Âàëèäàöèÿ âåñà (òîëüêî öèôğû, îò 20 äî 300)
        if (!string.IsNullOrWhiteSpace(CurrentWeightEntry.Text) && !IsValidNumber(CurrentWeightEntry.Text, 20, 300))
        {
            ProgressLabel.Text = "Îøèáêà: Âåñ äîëæåí áûòü ÷èñëîì îò 20 äî 300 êã";
            return;
        }

        // Âàëèäàöèÿ ğîñòà (òîëüêî öèôğû, îò 50 äî 250)
        if (!string.IsNullOrWhiteSpace(CurrentHeightEntry.Text) && !IsValidNumber(CurrentHeightEntry.Text, 50, 250))
        {
            ProgressLabel.Text = "Îøèáêà: Ğîñò äîëæåí áûòü ÷èñëîì îò 50 äî 250 ñì";
            return;
        }

        // Ñîõğàíÿåì äàííûå
        Preferences.Set("current_weight", CurrentWeightEntry.Text ?? "");
        Preferences.Set("current_height", CurrentHeightEntry.Text ?? "");
        Preferences.Set("measurement_date", MeasurementDatePicker.Date.ToString("dd.MM.yyyy"));

        ProgressLabel.Text = "Äàííûå ïğîãğåññà ñîõğàíåíû!";
        ProgressLabel.TextColor = Colors.Green;

        // Îáíîâëÿåì îòîáğàæåíèå
        UpdateSavedDataDisplay();
    }

    // Ïğîâåğêà ÷èñëà (òîëüêî öèôğû è â äèàïàçîíå)
    private bool IsValidNumber(string numberText, int min, int max)
    {
        if (int.TryParse(numberText, out int number))
        {
            return number >= min && number <= max;
        }
        return false;
    }

    // Îáğàáîò÷èêè ââîäà (òîëüêî öèôğû)
    private void OnWeightTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (!string.IsNullOrEmpty(e.NewTextValue) && !IsValidNumberInput(e.NewTextValue))
        {
            entry.Text = e.OldTextValue;
        }
    }

    private void OnHeightTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (!string.IsNullOrEmpty(e.NewTextValue) && !IsValidNumberInput(e.NewTextValue))
        {
            entry.Text = e.OldTextValue;
        }
    }

    // Ïğîâåğêà ââîäà öèôğ (òîëüêî öèôğû)
    private bool IsValidNumberInput(string text)
    {
        return text.All(char.IsDigit);
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    // Îáíîâëÿåì äàííûå ïğè êàæäîì ïîÿâëåíèè ñòğàíèöû
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadSavedProgressData();
    }
}