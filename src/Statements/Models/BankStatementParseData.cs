namespace ExpensesAnalyzer.Statements.Models;

public class BankStatementParseData
{
    #region Properties

    public string BankName { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public ParsedTransactionMapping ParseMapping { get; set; } = new();
    public bool DebitAmountsAreNegative { get; set; } = false;
    public float DefaultModifier { get; set; } = 1.0f;

    #endregion
}