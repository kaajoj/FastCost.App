using FastCost.DAL;
using FastCost.DAL.Entities;

namespace FastCost.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
[QueryProperty(nameof(CostValue), nameof(CostValue))]
public partial class CostPage : ContentPage
{
    private readonly CostRepository _costRepository;

    public string ItemId
    {
        set { _ = LoadCostAsync(value); }
    }

    public string CostValue { get; set; }

    // string amount;
    // public string Amount
    // {
    //     get => amount;
    //     set { amount = value; }
    // }
    //
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

    protected override void OnNavigatedTo(NavigatedToEventArgs state)
    {
        // base.OnNavigatedTo(state);

        if (!string.IsNullOrEmpty(CostValue))
        {
            ((Models.Cost)BindingContext).Value = decimal.Parse(CostValue);
            ((Models.Cost)BindingContext).Date = DateTime.UtcNow;
        }
        CostValueEditor.Text = CostValue;
        CostValueEditor.Text = (((Models.Cost)BindingContext).Value).ToString();
    }

    public CostPage(CostRepository costRepository) 
    {
        _costRepository = costRepository;
        InitializeComponent();

        // string appDataPath = FileSystem.AppDataDirectory; 
        // string randomFileName = $"{Path.GetRandomFileName()}.costs.txt";

        // LoadCost(Path.Combine(appDataPath, randomFileName));
        // LoadCost("");
    }

    private async Task LoadCostAsync(string id)
    {
        var costModel = await _costRepository.GetCostAsync(Int32.Parse(id));

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
        if (BindingContext is Models.Cost cost)
        {
            // File.WriteAllText(cost.FileName, DescriptionEditor.Text);
            // File.WriteAllText(cost.FileName, CostValueEditor.Text);
            await _costRepository.SaveCostAsync(cost);
        }
        
        // await Shell.Current.GoToAsync("..");
        await Shell.Current.GoToAsync($"//allCosts", true);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Cost cost)
        {
            await _costRepository.DeleteCostAsync(cost);
            // if (File.Exists(cost.FileName))
            //     File.Delete(cost.FileName);
        }

        await Shell.Current.GoToAsync("..");
    }
}