using Android.Content;
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

    // [ObservableProperty]
    // public string Test { get; set; }

    // string _amount;
    // public string Amount
    // {
    //     get => _amount;
    //     set
    //     {
    //         OnPropertyChanged();
    //         _amount = value;
    //         OnPropertyChanged();
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

    private string _selectedCategory;

    private Label _previousSelectedLabel;
    private Color _previousSelectedLabelColor;


    public CostPage() 
    {
        InitializeComponent();
        // this.categoriesGrid.SelectionChanged += OnCategorySelected;
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
            if (await DisplayAlert("Delete cost",
                "Would you like to delete cost " + cost.Value + cost.Description + "?",
                "Yes", "No"))
            {
                    await App.CostRepository.DeleteCostAsync(cost);
                    await Shell.Current.GoToAsync("..");
            }
        }
    }

    private void OnCategorySelected(object sender, TappedEventArgs args)
    {
        if (_previousSelectedLabel != null && _previousSelectedLabel.BackgroundColor == Color.Parse("CadetBlue"))
        {
            _previousSelectedLabel.BackgroundColor = _previousSelectedLabelColor;
            _previousSelectedLabel.Handler.UpdateValue("Background");
        }

        Label selectedLabel = (Label)sender;
        _previousSelectedLabel = selectedLabel;
        _previousSelectedLabelColor = selectedLabel.BackgroundColor;

        selectedLabel.BackgroundColor = Color.Parse("CadetBlue");
        selectedLabel.Handler.UpdateValue("Background");

        _selectedCategory = args.Parameter?.ToString();

        if (_selectedCategory != null) 
            ((CostModel)BindingContext).CategoryId = int.Parse(_selectedCategory);

        // var id = selectedLabel.Id;
        // var text = selectedLabel.Text;
    }
}