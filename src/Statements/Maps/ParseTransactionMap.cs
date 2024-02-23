using CsvHelper.Configuration;
using ExpensesAnalyzer.Statements.Models;
using ExpensesAnalyzer.Statements.TypeConverters;

namespace ExpensesAnalyzer.Statements.Maps;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ParseTransactionMap : ClassMap<ParsedTransaction>
{
    #region Fields

    public static ParsedTransactionMapping CurrentMapping = new();

    #endregion

    #region Constructors

    public ParseTransactionMap()
    {
        if (CurrentMapping.DateIndex != -1)
        {
            Map(m => m.Date)
                .Index(CurrentMapping.DateIndex);
        }

        if (CurrentMapping.DescriptionIndex != -1)
        {
            Map(m => m.Description)
                .Index(CurrentMapping.DescriptionIndex);
        }

        if (CurrentMapping.AmountIndex != -1)
        {
            Map(m => m.Amount)
                .Index(CurrentMapping.AmountIndex)
                .TypeConverter<FloatTypeConverter>();
        }

        if (CurrentMapping.BankDeterminedCategoryIndex != -1)
        {
            Map(m => m.BankDeterminedCategory)
                .Index(CurrentMapping.BankDeterminedCategoryIndex);
        }
    }

    #endregion
}