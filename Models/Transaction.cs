using SQLite;
using System;


namespace ITFundManager.Models;


public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }


    public int FundId { get; set; }


    public decimal Amount { get; set; }


    public string Note { get; set; }


    public DateTime Date { get; set; } = DateTime.UtcNow;
}