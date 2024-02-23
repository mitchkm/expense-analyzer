using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer.Transactions.Models;

public class Transaction : ParsedTransaction
{
    #region Properties

    public string BankId { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string PrimaryCategory { get; set; } = string.Empty;
    public string SubCategory { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = new();
    public float Modifier { get; set; } = 1.0f;
    public string Note { get; set; } = string.Empty;

    #endregion

    #region Constructors

    public Transaction(ParsedTransaction parsedTransaction)
    {
        Date = parsedTransaction.Date;
        Description = parsedTransaction.Description;
        Amount = parsedTransaction.Amount;
        BankDeterminedCategory = parsedTransaction.BankDeterminedCategory;
    }

    #endregion
}