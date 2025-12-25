using SQLite;
using System;

namespace ITFundManager.Models;

public enum ProposalStatus
{
    Pending = 0,   // Chờ duyệt
    Approved = 1,  // Đã duyệt
    Rejected = 2   // Từ chối
}

public class ExpenseProposal
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;     // Tiêu đề đề xuất
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ProposalStatus Status { get; set; } = ProposalStatus.Pending;

    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public int FundId { get; set; }        // 🔑 QUỸ ĐƯỢC ĐỀ XUẤT CHI
    public string? Reason { get; set; }    // Lý do chi
    public string? CreatedBy { get; set; } // Người đề xuất

}
