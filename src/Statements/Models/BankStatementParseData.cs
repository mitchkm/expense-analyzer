namespace ExpensesAnalyzer.Statements.Models;

public class BankStatementParseData
{
    #region Fields

    public string BankName = string.Empty;
    public ParsedTransactionMapping ParseMapping = new ();
    public bool DebitAmountsAreNegative = false;

    #endregion
}