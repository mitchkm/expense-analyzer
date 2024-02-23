﻿using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer.Transactions.Models;

public class Transaction : ParsedTransaction
{
    #region Fields

    public string Bank = string.Empty;
    public string PrimaryCategory = string.Empty;
    public string SubCategory = string.Empty;
    public List<string> Flags = new();
    public float Modifier = 1.0f;
    public string Note = string.Empty;

    #endregion

    #region Constructors

    public Transaction()
    {
    }

    public Transaction(ParsedTransaction parsedTransaction)
    {
        Date = parsedTransaction.Date;
        Description = parsedTransaction.Description;
        Amount = parsedTransaction.Amount;
        BankDeterminedCategory = parsedTransaction.BankDeterminedCategory;
    }

    #endregion
}