namespace ExpensesAnalyzer.StatementParsing.Models;

public class BankStatementParseData
{
    #region Fields

    public string BankName = string.Empty;
    public string RawStatementDirectory = string.Empty;
    public string ProcessedTransactionDataDirectory = string.Empty;
    public ParsedTransactionMapping ParseMapping = new ();
    public bool DebitAmountsAreNegative = false;

    #endregion
}