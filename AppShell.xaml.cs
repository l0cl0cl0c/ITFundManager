using ITFundManager.Services;

namespace ITFundManager;

public partial class AppShell : Shell
{
    private readonly AppState _appState;

    public AppShell(AppState appState)
    {
        InitializeComponent();
        _appState = appState;

        // Quyết định trang đầu
        if (_appState.IsLoggedIn)
            GoToAsync("//fundlist");
        else
            GoToAsync("//login");
    }
}
