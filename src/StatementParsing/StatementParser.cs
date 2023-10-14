using System.Globalization;
using CsvHelper;
using ExpensesAnalyzer.StatementParsing.Maps;
using ExpensesAnalyzer.StatementParsing.Models;

namespace ExpensesAnalyzer.StatementParsing;

public static class StatementParser
{
    public static IEnumerable<ParsedTransaction> ParseStatement(string path, ParsedTransactionMapping mapping)
    {
        ParsedTransactionMap.CurrentMapping = mapping;
        
        using StreamReader reader = new StreamReader(path);
        using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<ParsedTransactionMap>();
        IEnumerable<ParsedTransaction> records = csv.GetRecords<ParsedTransaction>();

        return records.ToList();
    }
}