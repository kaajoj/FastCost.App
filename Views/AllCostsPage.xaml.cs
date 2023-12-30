using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    public AllCostsPage()
	{
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs state)
    {
        base.OnNavigatedTo(state);

        var currentMonth = DateTime.UtcNow.Date.Month;

        ((AllCosts)BindingContext)?.Costs.Clear();
        var costs = await App.AllCostsService.LoadCostsByMonth(currentMonth);
        foreach (CostModel cost in costs.OrderBy(cost => cost.Date))
        {
            ((AllCosts)BindingContext)?.Costs.Add(cost);
        }

        //((AllCosts)BindingContext)?.Sum = 0;
        var sum = await App.AllCostsService.GetSum(currentMonth);
        SumText.Text = $"Total sum: {sum}";
        // SumText.Text = $"Total sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(currentMonth).Result.ToString()).Result}";
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

            await Shell.Current.GoToAsync($"{nameof(CostPage)}?{nameof(CostPage.ItemId)}={cost.Id}");

            // Unselect the UI
            costsCollection.SelectedItem = null;
        }
    }

    private async void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;

        ((AllCosts)BindingContext)?.Costs.Clear();
        var costs = await App.AllCostsService.LoadCostsByMonth(selectedDate.Month);
        foreach (CostModel cost in costs.OrderBy(cost => cost.Date))
        {
            ((AllCosts)BindingContext)?.Costs.Add(cost);
        }

        // SumText.Text = $"Sum:  {Task.Run(() => ((AllCosts)BindingContext)?.GetSum(selectedDate.Month).Result.ToString()).Result}";
        var sum = await App.AllCostsService.GetSum(selectedDate.Month);
        SumText.Text = $"Total sum: {sum}";
    }
}