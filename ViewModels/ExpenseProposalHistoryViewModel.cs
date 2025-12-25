using CommunityToolkit.Mvvm.ComponentModel;
using ITFundManager.Models;
using ITFundManager.Services;
using System.Collections.ObjectModel;

namespace ITFundManager.ViewModels;

public partial class ExpenseProposalHistoryViewModel : ObservableObject
{
    private readonly IExpenseProposalService _service;

    public ObservableCollection<ExpenseProposal> History { get; } = new();

    public ExpenseProposalHistoryViewModel(IExpenseProposalService service)
    {
        _service = service;
    }

    public async Task LoadAsync()
    {
        History.Clear();

        var list = await _service.GetAllAsync();

        foreach (var p in list)
        {
            if (p.Status != ProposalStatus.Pending)
                History.Add(p);
        }
    }
}
