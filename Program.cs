using System.Globalization;
using System.Text.Json;
using CsvHelper;
using ExpensesAnalyzer.StatementParsing.Maps;
using ExpensesAnalyzer.StatementParsing.Models;

namespace ExpensesAnalyzer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Setup map
            ParsedTransactionMapping capitolOneMapping  = new ()
            {
                DateIndex = 0,
                DescriptionIndex = 3,
                AmountIndex = 5,
                BankDeterminedCategoryIndex = 4,
            };

            ParsedTransactionMapping chaseMapping = new ()
            {
                DateIndex = 0,
                DescriptionIndex = 2,
                AmountIndex = 5,
                BankDeterminedCategoryIndex = 3,
            };

            ParsedTransactionMapping wfMapping = new ()
            {
                DateIndex = 0,
                DescriptionIndex = 4,
                AmountIndex = 1,
            };

            ParsedTransactionMap.CurrentMapping = wfMapping;
            
            // using StreamReader reader = new StreamReader("./TestData/capitolone-2022-statement.csv");
            // using StreamReader reader = new StreamReader("./TestData/chase-2022-statement.csv");
            using StreamReader reader = new StreamReader("./TestData/wellsfargo-test.csv");
            using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ParsedTransactionMap>();
            IEnumerable<ParsedTransaction>? records = csv.GetRecords<ParsedTransaction>();
            foreach (ParsedTransaction record in records)
            {
                record.NegateAmount();
                Console.WriteLine(record);
                
            }

            JsonSerializerOptions options = new ()
            {
                IncludeFields = true,
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(wfMapping, options);
            Console.WriteLine(json);
            ParsedTransactionMapping test = JsonSerializer.Deserialize<ParsedTransactionMapping>(json, options);
            Console.WriteLine(test.DescriptionIndex);
        }
    }
}