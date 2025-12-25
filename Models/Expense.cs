using SQLite;
using System;

namespace ITFundManager.Models;

public class Expense
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public int FundId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = string.Empty;

    // 🔗 liên kết đề xuất
    public int ExpenseProposalId { get; set; }
}
