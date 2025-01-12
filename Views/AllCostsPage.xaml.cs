using FastCost.Models;
using Mapster;

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
        
        var currentDate = ((AllCosts)BindingContext).SelectedDate;

        ((AllCosts)BindingContext)?.Costs.Clear();
        var costs = await App.AllCostsService.LoadCostsByMonth(currentDate);
        foreach (CostModel cost in costs.Adapt<List<CostModel>>().OrderBy(cost => cost.Date))
        {
            ((AllCosts)BindingContext)?.Costs.Add(cost);
        }

        ((AllCosts)BindingContext).Sum = await App.AllCostsService.GetSum(currentDate);
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
        var costs = await App.AllCostsService.LoadCostsByMonth(selectedDate);
        foreach (CostModel cost in costs.Adapt<List<CostModel>>().OrderBy(cost => cost.Date))
        {
            ((AllCosts)BindingContext)?.Costs.Add(cost);
        }

        ((AllCosts)BindingContext).Sum = await App.AllCostsService.GetSum(selectedDate);
    }
}