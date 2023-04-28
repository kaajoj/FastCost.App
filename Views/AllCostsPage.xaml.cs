using FastCost.Models;

namespace FastCost.Views;

public partial class AllCostsPage : ContentPage
{
    public AllCostsPage()
	{
        InitializeComponent();

        BindingContext = new AllCosts();
    }

    protected override void OnAppearing()
    {
        ((AllCosts)BindingContext)?.LoadCosts();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CostPage));
    }

    private async void costsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
}