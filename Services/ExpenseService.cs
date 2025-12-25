using ITFundManager.Models;
using SQLite;

namespace ITFundManager.Services;

public class ExpenseService : IExpenseService
{
    private readonly SQLiteConnection _conn;

    public ExpenseService(IAppDatabase db)
    {
        _conn = db.Conn;
        _conn.CreateTable<Expense>();
    }

    public Task AddAsync(Expense expense)
    {
        _conn.Insert(expense);
        return Task.CompletedTask;
    }
}
