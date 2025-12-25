using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;
using System.Collections.ObjectModel;

namespace ITFundManager.ViewModels;

public partial class UserListViewModel : ObservableObject
{
    private readonly IUserService _userService;

    public ObservableCollection<User> Users { get; } = new();

    [ObservableProperty] bool isBusy;

    public UserListViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        Users.Clear();
        var list = await _userService.GetAllAsync();
        foreach (var u in list) Users.Add(u);
        IsBusy = false;
    }
}
