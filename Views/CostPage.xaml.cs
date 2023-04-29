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

    public CostPage() 
    {
        InitializeComponent();

        // string appDataPath = FileSystem.AppDataDirectory; 
        // string randomFileName = $"{Path.GetRandomFileName()}.costs.txt";

        // LoadCost(Path.Combine(appDataPath, randomFileName));
        // LoadCost("");
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        if (!string.IsNullOrEmpty(CostValue))
        {
            ((CostModel)BindingContext).Value = decimal.Parse(CostValue);
            ((CostModel)BindingContext).Date = DateTime.UtcNow;
        }
        CostValueEditor.Text = CostValue;

        if (((CostModel)BindingContext).Value != 0)
            CostValueEditor.Text = ((CostModel)BindingContext).Value.ToString(CultureInfo.InvariantCulture);
    }

    private async Task LoadCostAsync(string id)
    {
        var cost = await App.CostRepository.GetCostAsync(Int32.Parse(id));
        var costModel = cost.Adapt<CostModel>();

        // var costModel = new Models.Cost
        // {
        //     // FileName = fileName
        //     // Id = Int32.Parse(id)
        // };
        //
        // if (File.Exists(fileName))
        // {
        //     costModel.Date = File.GetCreationTime(fileName);
        //     costModel.Description = File.ReadAllText(fileName);
        //     costModel.Value = decimal.Parse(File.ReadAllText(fileName));
        // }

        BindingContext = costModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is CostModel costModel)
        {
            // File.WriteAllText(cost.FileName, DescriptionEditor.Text);
            // File.WriteAllText(cost.FileName, CostValueEditor.Text);
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
            // if (File.Exists(cost.FileName))
            //     File.Delete(cost.FileName);
        }

        await Shell.Current.GoToAsync("..");
    }
}