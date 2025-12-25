using ITFundManager.Models;

namespace ITFundManager.Services;

public interface IExpenseService
{
    Task AddAsync(Expense expense);
}
