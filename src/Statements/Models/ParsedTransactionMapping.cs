namespace ExpensesAnalyzer.Statements.Models;

public struct ParsedTransactionMapping
{
    public short DateIndex = -1;
    public short DescriptionIndex = -1;
    public short AmountIndex = -1;
    public short BankDeterminedCategoryIndex = -1;

    public ParsedTransactionMapping()
    {
    }
}