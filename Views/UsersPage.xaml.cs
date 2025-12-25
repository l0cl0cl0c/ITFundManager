using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class UsersPage : ContentPage
{
    private readonly UserListViewModel vm;
    public UsersPage(UserListViewModel viewModel)
    {
        InitializeComponent();
        vm = viewModel;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.LoadAsync();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var page = Handler.MauiContext.Services.GetService<AddEditUserPage>();
        await Navigation.PushAsync(page);
    }
}
