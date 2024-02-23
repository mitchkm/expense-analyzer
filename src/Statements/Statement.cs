using System.Globalization;
using CsvHelper;
using ExpensesAnalyzer.Statements.Maps;
using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer.Statements;

public static class Statement
{
    public static List<ParsedTransaction> Parse(string path, ParsedTransactionMapping mapping)
    {
        ParseTransactionMap.CurrentMapping = mapping;
        
        using StreamReader reader = new (path);
        using CsvReader csv = new (reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<ParseTransactionMap>();
        IEnumerable<ParsedTransaction> records = csv.GetRecords<ParsedTransaction>();

        return records.ToList();
    }
}