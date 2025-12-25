using ITFundManager.ViewModels;

namespace ITFundManager.Views;

public partial class AddExpenseProposalPage : ContentPage
{
    public AddExpenseProposalPage(AddExpenseProposalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
