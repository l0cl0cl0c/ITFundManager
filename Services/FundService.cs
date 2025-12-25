using ITFundManager.Models;
using SQLite;

namespace ITFundManager.Services;

public class FundService : IFundService
{
    private readonly SQLiteConnection _conn;

    public FundService(IAppDatabase db)
    {
        _conn = db.Conn;
        _conn.CreateTable<Fund>();
    }

    public Task<List<Fund>> GetAllAsync()
        => Task.FromResult(_conn.Table<Fund>().ToList());

    public Task AddAsync(Fund fund)
    {
        _conn.Insert(fund);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Fund fund)
    {
        _conn.Update(fund);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _conn.Delete<Fund>(id);
        return Task.CompletedTask;
    }
    public Task DecreaseBalanceAsync(int fundId, decimal amount)
    {
        var fund = _conn.Find<Fund>(fundId);
        if (fund == null) return Task.CompletedTask;

        fund.Balance -= amount;
        _conn.Update(fund);

        return Task.CompletedTask;
    }
    public Task<Fund?> GetByIdAsync(int id)
    {
        var fund = _conn.Find<Fund>(id);
        return Task.FromResult(fund);
    }


}
