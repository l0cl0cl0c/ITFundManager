using ITFundManager.Models;
using SQLite;

namespace ITFundManager.Services;

public class ExpenseProposalService : IExpenseProposalService
{
    private readonly SQLiteConnection _conn;
    private readonly IExpenseService _expenseService;
    private readonly IFundService _fundService;

    public ExpenseProposalService(
        IAppDatabase db,
        IExpenseService expenseService,
        IFundService fundService)
    {
        _conn = db.Conn;
        _conn.CreateTable<ExpenseProposal>();

        _expenseService = expenseService;
        _fundService = fundService;
    }

    public Task<List<ExpenseProposal>> GetAllAsync()
    {
        var list = _conn.Table<ExpenseProposal>()
                        .OrderByDescending(x => x.CreatedAt)
                        .ToList();

        return Task.FromResult(list);
    }

    public Task AddAsync(ExpenseProposal proposal)
    {
        proposal.Status = ProposalStatus.Pending;
        proposal.CreatedAt = DateTime.Now;

        _conn.Insert(proposal);
        return Task.CompletedTask;
    }

    // ============================
    // ✅ DUYỆT ĐỀ XUẤT
    // ============================
    public async Task ApproveAsync(ExpenseProposal proposal, string approvedBy)
    {
        // 🔒 LẤY BẢN GHI THẬT TỪ DB
        var dbProposal = _conn.Find<ExpenseProposal>(proposal.Id);
        if (dbProposal == null)
            throw new Exception("Đề xuất không tồn tại");

        if (dbProposal.Status != ProposalStatus.Pending)
            return;

        // 🔒 LẤY ĐÚNG QUỸ CỦA ĐỀ XUẤT
        var fund = await _fundService.GetByIdAsync(dbProposal.FundId);
        if (fund == null)
            throw new Exception("Quỹ không tồn tại");

        if (fund.Balance < dbProposal.Amount)
            throw new Exception("Số dư quỹ không đủ");

        // ✅ CẬP NHẬT TRẠNG THÁI ĐỀ XUẤT
        dbProposal.Status = ProposalStatus.Approved;
        dbProposal.ApprovedBy = approvedBy;
        dbProposal.ApprovedAt = DateTime.Now;
        _conn.Update(dbProposal);

        // ✅ TẠO PHIẾU CHI
        var expense = new Expense
        {
            Title = dbProposal.Title,
            Amount = dbProposal.Amount,
            FundId = fund.Id,
            CreatedBy = approvedBy,
            ExpenseProposalId = dbProposal.Id,
            CreatedAt = DateTime.Now
        };

        await _expenseService.AddAsync(expense);

        // ✅ TRỪ TIỀN QUỸ
        await _fundService.DecreaseBalanceAsync(fund.Id, dbProposal.Amount);
    }

    // ============================
    // ❌ TỪ CHỐI ĐỀ XUẤT
    // ============================
    public Task RejectAsync(ExpenseProposal proposal, string approvedBy)
    {
        var dbProposal = _conn.Find<ExpenseProposal>(proposal.Id);
        if (dbProposal == null)
            return Task.CompletedTask;

        if (dbProposal.Status != ProposalStatus.Pending)
            return Task.CompletedTask;

        dbProposal.Status = ProposalStatus.Rejected;
        dbProposal.ApprovedBy = approvedBy;
        dbProposal.ApprovedAt = DateTime.Now;

        _conn.Update(dbProposal);
        return Task.CompletedTask;
    }
}
