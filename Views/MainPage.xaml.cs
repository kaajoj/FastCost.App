namespace FastCost.Views;

public partial class MainPage : ContentPage
{
	decimal _allCosts = 0;
    public decimal CostValue { get; private set; }

    public MainPage()
	{
		InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        var costs = await App.CostRepository.GetCostsAsync();
        // var sum = costs.Sum(c => c.Value);

        var currentMonth = DateTime.UtcNow.Date.Month;
        var currentMonthName = DateTime.UtcNow.Date.ToString("MMMM");
        var costsInCurrentMonth = costs.Where(c => c.Date.Month == currentMonth).Sum(c => c.Value);

        SummaryText.Text = $"Expenses in {currentMonthName}: {costsInCurrentMonth}";
    }

    private void OnCostChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                Decimal.TryParse(e.NewTextValue, out var enteredCost);
                // var enteredCost = Convert.ToDecimal(e.NewTextValue);
                
                if (enteredCost > 0)
                {
                    CostBtn.IsEnabled = true;
                    CostBtn.BackgroundColor = Color.Parse("Lime");
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
                // DisplayAlert("Information", "Entered amount is lower than 0", "OK");
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

