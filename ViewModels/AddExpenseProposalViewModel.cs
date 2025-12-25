using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITFundManager.Models;
using ITFundManager.Services;

namespace ITFundManager.ViewModels;

[QueryProperty(nameof(Fund), "Fund")]
public partial class AddExpenseProposalViewModel : ObservableObject
{
    private readonly IExpenseProposalService _service;

    public Fund? Fund { get; set; }

    [ObservableProperty] private string title;
    [ObservableProperty] private decimal amount;

    public AddExpenseProposalViewModel(IExpenseProposalService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Fund == null)
        {
            await Application.Current!.MainPage!
                .DisplayAlert("Lỗi", "Không xác định được quỹ", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Title))
        {
            await Application.Current!.MainPage!
                .DisplayAlert("Lỗi", "Vui lòng nhập tiêu đề", "OK");
            return;
        }

        if (Amount <= 0)
        {
            await Application.Current!.MainPage!
                .DisplayAlert("Lỗi", "Số tiền phải > 0", "OK");
            return;
        }

        var proposal = new ExpenseProposal
        {
            Title = $"{Fund.Name} - {Title}", // gắn tên quỹ vào tiêu đề
            Amount = Amount,
            Status = ProposalStatus.Pending,
            CreatedAt = DateTime.Now
        };

        await _service.AddAsync(proposal);

        await Shell.Current.GoToAsync("//fundlist");
    }
}
