using FastCost.DAL;
using FastCost.Services;

namespace FastCost;

public partial class App : Application
{
    internal static CostRepository CostRepository;
    internal static CategoryRepository CategoryRepository;
    internal static AllCostsService AllCostsService;

    [Obsolete("'Application.MainPage.set' is obsolete: 'This property is deprecated. " +
        "Initialize your application by overriding Application.CreateWindow rather than setting MainPage." +
        " To modify the root page in an active application, use Windows[0].Page for applications with" +
        " a single window. For applications with multiple windows, use Application.Windows to identify and" +
        " update the root page on the correct window.  Additionally, each element features a Window property," +
        " accessible when it's part of the current window.'")]
    public App(CostRepository costRepository, CategoryRepository categoryRepository, AllCostsService allCostsService)
	{
        InitializeComponent();


		MainPage = new AppShell();

        CostRepository = costRepository;
        CategoryRepository = categoryRepository;
        AllCostsService = allCostsService;
    }
}
