using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class AddEditUserPage : ContentPage
{
    public AddEditUserPage(AddEditUserViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
