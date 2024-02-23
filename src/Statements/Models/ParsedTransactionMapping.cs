namespace ExpensesAnalyzer.Statements.Models;

public class ParsedTransactionMapping
{
    #region Properties

    public short DateIndex { get; set; } = -1;
    public short DescriptionIndex { get; set; } = -1;
    public short AmountIndex { get; set; } = -1;
    public short BankDeterminedCategoryIndex { get; set; } = -1;

    #endregion
}