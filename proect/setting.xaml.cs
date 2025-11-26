namespace proect;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();
        LoadSavedData();
    }

    private void LoadSavedData()
    {
        NameEntry.Text = Preferences.Get("user_name", "");
        HeightEntry.Text = Preferences.Get("user_height", "");
        WeightEntry.Text = Preferences.Get("user_weight", "");
        AgeEntry.Text = Preferences.Get("user_age", "");
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        // Ñáğàñûâàåì öâåò òåêñòà
        LableSetting.TextColor = Colors.Red;

        // Âàëèäàöèÿ èìåíè (òîëüêî áóêâû è ïğîáåëû)
        if (!string.IsNullOrWhiteSpace(NameEntry.Text) && !IsValidName(NameEntry.Text))
        {
            LableSetting.Text = "Îøèáêà: Èìÿ äîëæíî ñîäåğæàòü òîëüêî áóêâû";
            return;
        }

        // Âàëèäàöèÿ ğîñòà (òîëüêî öèôğû, îò 50 äî 250)
        if (!string.IsNullOrWhiteSpace(HeightEntry.Text) && !IsValidNumber(HeightEntry.Text, 50, 250))
        {
            LableSetting.Text = "Îøèáêà: Ğîñò äîëæåí áûòü ÷èñëîì îò 50 äî 250 ñì";
            return;
        }

        // Âàëèäàöèÿ âåñà (òîëüêî öèôğû, îò 20 äî 300)
        if (!string.IsNullOrWhiteSpace(WeightEntry.Text) && !IsValidNumber(WeightEntry.Text, 20, 300))
        {
            LableSetting.Text = "Îøèáêà: Âåñ äîëæåí áûòü ÷èñëîì îò 20 äî 300 êã";
            return;
        }

        // Âàëèäàöèÿ âîçğàñòà (òîëüêî öèôğû, îò 1 äî 150)
        if (!string.IsNullOrWhiteSpace(AgeEntry.Text) && !IsValidNumber(AgeEntry.Text, 1, 150))
        {
            LableSetting.Text = "Îøèáêà: Âîçğàñò äîëæåí áûòü ÷èñëîì îò 1 äî 150 ëåò";
            return;
        }

        // Åñëè âñå äàííûå âàëèäíû - ñîõğàíÿåì
        Preferences.Set("user_name", NameEntry.Text ?? "");
        Preferences.Set("user_height", HeightEntry.Text ?? "");
        Preferences.Set("user_weight", WeightEntry.Text ?? "");
        Preferences.Set("user_age", AgeEntry.Text ?? "");

        LableSetting.Text = "Äàííûå óñïåøíî ñîõğàíåíû!";
        LableSetting.TextColor = Colors.Green;
    }

    // Ïğîâåğêà èìåíè (òîëüêî áóêâû è ïğîáåëû)
    private bool IsValidName(string name)
    {
        return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
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

    private async void ongoback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    // Îáğàáîò÷èêè ğåàëüíîé âàëèäàöèè ïğè ââîäå
    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (!string.IsNullOrEmpty(e.NewTextValue) && !IsValidName(e.NewTextValue))
        {
            // Îòêàòûâàåì ê ïğåäûäóùåìó âàëèäíîìó çíà÷åíèş
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

    private void OnWeightTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (!string.IsNullOrEmpty(e.NewTextValue) && !IsValidNumberInput(e.NewTextValue))
        {
            entry.Text = e.OldTextValue;
        }
    }

    private void OnAgeTextChanged(object sender, TextChangedEventArgs e)
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
}
