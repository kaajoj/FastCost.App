using System.Runtime.InteropServices.JavaScript;

namespace FastCost;

public partial class MainPage : ContentPage
{
	decimal allCosts = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private void OnCostChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                var enteredCost = Convert.ToDecimal(e.NewTextValue);        // TODO: try parse

                if (enteredCost > 0)
                {
                    CostBtn.IsEnabled = true;
                    CostBtn.BackgroundColor = Color.Parse("Lime");
                }
                else
                {
                    enteredCost = 0;
                }
            }
            else
            {
                CostText.Text = string.Empty;
                CostBtn.IsEnabled = false;
                CostBtn.BackgroundColor = Color.Parse("Orange");
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
                allCosts += enteredCost;
                SummaryText.Text = $"Expenses in March: {allCosts}";
                CostText.Text = string.Empty;
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to add cost", "Cost value was not valid.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to add cost", "Cost adding failed.", "OK");
            }
        }
    }
}

