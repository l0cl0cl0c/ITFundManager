using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;

using static Microsoft.Maui.ApplicationModel.Permissions;

namespace ITFundManager.ViewModels;

public partial class AddEditUserViewModel : ObservableObject
{
    private readonly IUserService _userService;

    [ObservableProperty] int id;
    [ObservableProperty] string fullName;
    [ObservableProperty] string accountName;
    [ObservableProperty] string email;
    [ObservableProperty] string phone;
    [ObservableProperty] string position;
    [ObservableProperty] string department;
    [ObservableProperty] string password;
    [ObservableProperty] bool isBusy;

    public AddEditUserViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public void LoadFrom(User u)
    {
        Id = u.Id;
        FullName = u.FullName;
        AccountName = u.AccountName;
        Email = u.Email;
        Phone = u.Phone;
        Position = u.Position;
        Department = u.Department;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            if (Id == 0)
            {
                var u = new User
                {
                    FullName = FullName,
                    AccountName = AccountName,
                    Email = Email,
                    Phone = Phone,
                    Position = Position,
                    Department = Department
                };
                await _userService.CreateUserAsync(u, Password ?? "123456");
            }
            else
            {
                var u = await _userService.GetByIdAsync(Id);
                if (u != null)
                {
                    u.FullName = FullName;
                    u.AccountName = AccountName;
                    u.Email = Email;
                    u.Phone = Phone;
                    u.Position = Position;
                    u.Department = Department;
                    await _userService.UpdateAsync(u);
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
