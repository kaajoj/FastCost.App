using FastCost.DAL;

namespace FastCost;

public partial class App : Application
{
    internal static CostRepository CostRepository;
    internal static CategoryRepository CategoryRepository;

    public App(CostRepository costRepository, CategoryRepository categoryRepository)
	{
        InitializeComponent();

		MainPage = new AppShell();

        CostRepository = costRepository;
        CategoryRepository = categoryRepository;
    }
}
