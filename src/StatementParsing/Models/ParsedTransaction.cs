namespace ExpensesAnalyzer.StatementParsing.Models;

public class ParsedTransaction
{
    #region Fields

    public DateTime Date;
    public string Description = "";
    public float Amount;
    public string BankDeterminedCategory = "";

    #endregion

    #region Properties

    public bool IsDebit => Amount > 0;

    #endregion

    public void NegateAmount()
    {
        Amount *= -1;
    }

    public override string ToString()
    {
        return $"{Date} | {Amount} | {Description} | {BankDeterminedCategory}";
    }
}