using System.Text.Json;
using System.CommandLine;
using ExpensesAnalyzer.Statements;
using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            RootCommand rootCommand = new RootCommand("Root Command!");
            rootCommand.SetHandler(() =>
            {
                Console.WriteLine("Hello World!");
            });

            await rootCommand.InvokeAsync(args);
            OldMain(args);
        }
        
        static void OldMain(string[] args)
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
            
            // using StreamReader reader = new StreamReader("./TestData/capitolone-2022-statement.csv");
            // using StreamReader reader = new StreamReader("./TestData/chase-2022-statement.csv");
            // using StreamReader reader = new StreamReader("./TestData/wellsfargo-test.csv");

            IEnumerable<ParsedTransaction> records = Statement.Parse("../TestData/capitolone-2022-statement.csv", capitolOneMapping);
            foreach (ParsedTransaction record in records)
            {
                // record.NegateAmount();
                Console.WriteLine(record);
                
            }

            var bankStatementData = new BankStatementParseData()
            {
                BankName = "Wells Fargo",
                DebitAmountsAreNegative = true,
                ParseMapping = wfMapping,
                ProcessedTransactionDataDirectory = "processed",
                RawStatementDirectory = "raw",
            };
            
            JsonSerializerOptions options = new()
            {
                IncludeFields = true,
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(bankStatementData, options);
            Console.WriteLine(json);
            BankStatementParseData test = JsonSerializer.Deserialize<BankStatementParseData>(json, options)!;
            Console.WriteLine(test?.BankName);
        }
    }
}