namespace FastCost.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
[QueryProperty(nameof(Amount), nameof(Amount))]
public partial class CostPage : ContentPage
{
    public string ItemId
    {
        set { LoadCost(value); }
    }

    public string Amount { get; set; }

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
        base.OnNavigatedTo(state);

        if (!string.IsNullOrEmpty(Amount))
        {
            ((Models.Cost)BindingContext).Value = decimal.Parse(Amount);
        }
        AmountEditor.Text = Amount;
        AmountEditor.Text = ((decimal)((Models.Cost)BindingContext).Value).ToString();
    }

    public CostPage() 
    { 
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory; 
        string randomFileName = $"{Path.GetRandomFileName()}.costs.txt";

        LoadCost(Path.Combine(appDataPath, randomFileName));
    }

    private void LoadCost(string fileName)
    {
        var costModel = new Models.Cost
        {
            FileName = fileName
        };

        if (File.Exists(fileName))
        {
            costModel.Date = File.GetCreationTime(fileName);
            costModel.Description = File.ReadAllText(fileName);
            costModel.Value = decimal.Parse(File.ReadAllText(fileName));
        }

        BindingContext = costModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Cost cost)
        {
            // File.WriteAllText(cost.FileName, DescriptionEditor.Text);
            File.WriteAllText(cost.FileName, AmountEditor.Text);
        }
        
        // await Shell.Current.GoToAsync("..");
        await Shell.Current.GoToAsync($"//allCosts", true);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Cost cost)
        {
            // Delete the file.
            if (File.Exists(cost.FileName))
                File.Delete(cost.FileName);
        }

        await Shell.Current.GoToAsync("..");
    }
}