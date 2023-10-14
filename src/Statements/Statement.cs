using System.Globalization;
using CsvHelper;
using ExpensesAnalyzer.Statements.Maps;
using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer.Statements;

public static class Statement
{
    public static IEnumerable<ParsedTransaction> Parse(string path, ParsedTransactionMapping mapping)
    {
        ParsedTransactionMap.CurrentMapping = mapping;
        
        using StreamReader reader = new StreamReader(path);
        using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<ParsedTransactionMap>();
        IEnumerable<ParsedTransaction> records = csv.GetRecords<ParsedTransaction>();

        return records.ToList();
    }
}