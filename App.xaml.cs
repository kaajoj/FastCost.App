using FastCost.DAL;
using FastCost.Services;

namespace FastCost;

public partial class App : Application
{
    internal static CostRepository CostRepository;
    internal static CategoryRepository CategoryRepository;
    internal static AllCostsService AllCostsService;

    public App(CostRepository costRepository, CategoryRepository categoryRepository, AllCostsService allCostsService)
	{
        InitializeComponent();

        CostRepository = costRepository;
        CategoryRepository = categoryRepository;
        AllCostsService = allCostsService;
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell());
    }
}
