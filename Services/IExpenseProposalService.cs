using ITFundManager.Models;

namespace ITFundManager.Services;

public interface IExpenseProposalService
{
    Task<List<ExpenseProposal>> GetAllAsync();

    Task AddAsync(ExpenseProposal proposal); // ✅ THÊM

    Task ApproveAsync(ExpenseProposal proposal, string approvedBy);
    Task RejectAsync(ExpenseProposal proposal, string approvedBy);
    Task<List<ExpenseProposal>> GetHistoryAsync();

}
