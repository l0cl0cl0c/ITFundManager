using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Services;
using Microsoft.Maui.Controls;

namespace ITFundManager.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly AppState _appState;
    private readonly IServiceProvider _services;

    public LoginViewModel(
        IUserService userService,
        AppState appState,
        IServiceProvider services)
    {
        _userService = userService;
        _appState = appState;
        _services = services;
    }

    // ======================
    // PROPERTIES
    // ======================

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    // ======================
    // LOGIN COMMAND
    // ======================

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            // ✅ VALIDATE
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current!.MainPage!
                    .DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin", "OK");
                return;
            }

            // ✅ AUTH
            var user = await _userService.AuthenticateAsync(
                Username.Trim(),
                Password);

            if (user == null)
            {
                await Application.Current!.MainPage!
                    .DisplayAlert("Lỗi", "Sai tài khoản hoặc mật khẩu", "OK");
                return;
            }

            // ✅ LƯU TRẠNG THÁI
            _appState.Login(user);

            // ✅ CHUYỂN SANG APP SHELL (RESET NAVIGATION)
            Application.Current.MainPage =
                _services.GetRequiredService<AppShell>();
        }
        finally
        {
            IsBusy = false;
        }
    }
}
