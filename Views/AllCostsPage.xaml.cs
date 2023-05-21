using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    private AllCosts _allCosts;

    public AllCostsPage()
	{
        InitializeComponent();
        _allCosts = new AllCosts();
        BindingContext = _allCosts;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;
        // var currentMonth = 4;
        ((AllCosts)BindingContext)?.LoadCostsByMonth(currentMonth);
        // ((AllCosts)BindingContext)?.LoadCosts();

        SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CostPage));
    }

    private async void CostsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the cost model
            var cost = (CostModel)e.CurrentSelection[0];

            // Should navigate to "CostPage?ItemId=path\on\device\XYZ.costs.txt"
            await Shell.Current.GoToAsync($"{nameof(CostPage)}?{nameof(CostPage.ItemId)}={cost.Id}");

            // Unselect the UI
            costsCollection.SelectedItem = null;
        }
    }

    private async void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        await _allCosts.LoadCostsByMonth(selectedDate.Month);
        SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
    }
}