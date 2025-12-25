using ITFundManager.Models;

namespace ITFundManager.Services;

public interface IFundService
{
    Task<List<Fund>> GetAllAsync();
    Task AddAsync(Fund fund);
    Task UpdateAsync(Fund fund);
    Task DeleteAsync(int id);
    Task DecreaseBalanceAsync(int fundId, decimal amount);
    Task<Fund?> GetByIdAsync(int id);


}
