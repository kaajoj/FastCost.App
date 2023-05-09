using System.Globalization;
using FastCost.DAL.Entities;
using FastCost.Models;
using Mapster;

namespace FastCost.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
[QueryProperty(nameof(CostValue), nameof(CostValue))]
public partial class CostPage : ContentPage
{
    public string ItemId
    {
        set { _ = LoadCostAsync(value); }
    }

    public string CostValue { get; set; }

    // string amount;
    // public string Amount
    // {
    //     get => amount;
    //     set
    //     {
    //         amount = value;
    //         // OnPropertyChanged();
    //     }
    // }

    // Retrieving data
    // the first argument for the QueryPropertyAttribute specifies the name of the property that will receive the data,
    // with the second argument specifying the parameter ID
    // [QueryProperty(nameof(AstroName), "bodyName")]

    // string astroName;
    // public string AstroName
    // {
    //     get => astroName;
    //     set
    //     {
    //         astroName = value;
    //     }
    // }

    private string selectedCategory;

    public CostPage() 
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        if (!string.IsNullOrEmpty(CostValue))
        {
            ((CostModel)BindingContext).Value = decimal.Parse(CostValue);
            ((CostModel)BindingContext).Date = DateTime.UtcNow;
        }

        if (((CostModel)BindingContext).Value >= 0)
        {
            CostValueEditor.Text = ((CostModel)BindingContext).Value.ToString();
        }
        else
        {
            CostValueEditor.Text = string.Empty;
            ((CostModel)BindingContext).Date = DateTime.UtcNow;
        }
    }

    private async Task LoadCostAsync(string id)
    {
        var cost = await App.CostRepository.GetCostAsync(int.Parse(id));
        var costModel = cost.Adapt<CostModel>();

        BindingContext = costModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is CostModel costModel)
        {
            costModel.Date = DateTime.UtcNow;

            var cost = costModel.Adapt<Cost>();
            await App.CostRepository.SaveCostAsync(cost);
        }
        
        // await Shell.Current.GoToAsync("..");
        await Shell.Current.GoToAsync($"//allCosts", true);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is CostModel costModel)
        {
            var cost = costModel.Adapt<Cost>();
            await App.CostRepository.DeleteCostAsync(cost);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void OnCategorySelected(object sender, TappedEventArgs args)
    {
        selectedCategory = args.Parameter?.ToString();
        Label selectedLabel = (Label)sender;
        selectedLabel.BackgroundColor = Color.Parse("CadetBlue"); 

        // przesłanie wartości wybranej kategorii do bazy danych
    }
}