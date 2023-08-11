namespace FastCost.Views;

public partial class MainPage : ContentPage
{
    public decimal CostValue { get; private set; }

    public MainPage()
	{
		InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        var currentMonth = DateTime.UtcNow.Date.Month;
        var costs = await App.CostRepository.GetCostsByMonth(currentMonth);

        var costsInCurrentMonth = costs.Sum(c => c.Value);

        var currentMonthName = DateTime.UtcNow.Date.ToString("MMMM");
        SummaryText.Text = $"Expenses in {currentMonthName}: {costsInCurrentMonth}";
    }

    private void OnCostChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                decimal.TryParse(e.NewTextValue, out var enteredCost);
                // var enteredCost = Convert.ToDecimal(e.NewTextValue);
                
                if (enteredCost > 0)
                {
                    CostBtn.IsEnabled = true;
                    CostBtn.BackgroundColor = Color.Parse("Lime");
                    CostBtn.Handler.UpdateValue("Background");
                }
                else
                {
                    // enteredCost = 0;
                }
            }
            else
            {
                CostText.Text = string.Empty;
                CostBtn.IsEnabled = false;
                CostBtn.BackgroundColor = Color.Parse("LightGray");
                CostBtn.Handler.UpdateValue("Background");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void OnCostEntered(object sender, EventArgs e)
	{
        SemanticScreenReader.Announce(CostBtn.Text);

        // if (await DisplayAlert(
        //         "Add cost",
        //         "Would you like to add cost " + CostText.Text + "?",
        //         "Yes",
        //         "No"))
        {
            try
            {
                var enteredCost = Convert.ToDecimal(CostText.Text);
                CostText.Text = string.Empty;
                CostValue = enteredCost;
                await Shell.Current.GoToAsync($"{nameof(CostPage)}?{nameof(CostPage.CostValue)}={CostValue.ToString()}", true);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to add cost", "Cost value was not valid.", "OK");
                throw;
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to add cost", "Cost adding failed.", "OK");
                throw;
            }
        }
    }
}

