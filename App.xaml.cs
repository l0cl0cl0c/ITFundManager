using ITFundManager.Services;
using ITFundManager.Views;

namespace ITFundManager;

public partial class App : Application
{
    private readonly AppState _appState;
    private readonly IServiceProvider _services;

    public App(AppState appState, IServiceProvider services)
    {
        InitializeComponent();
        _appState = appState;
        _services = services;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Page startPage = _appState.IsLoggedIn
            ? _services.GetRequiredService<AppShell>()
            : new NavigationPage(
                _services.GetRequiredService<LoginPage>());

        return new Window(startPage);
    }
}
