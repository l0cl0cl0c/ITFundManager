using ITFundManager.Models;
using ITFundManager.ViewModels;

namespace ITFundManager.Views;

[QueryProperty(nameof(Fund), "Fund")]
public partial class AddFundPage : ContentPage
{
    private AddFundViewModel _viewModel;

    public Fund Fund
    {
        set
        {
            if (value != null)
                _viewModel.LoadForEdit(value);
        }
    }

    public AddFundPage()
    {
        InitializeComponent();

        _viewModel = Application.Current!
            .Handler!
            .MauiContext!
            .Services
            .GetRequiredService<AddFundViewModel>();

        BindingContext = _viewModel;
    }
}
