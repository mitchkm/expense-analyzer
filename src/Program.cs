using System.Text.Json;
using System.CommandLine;
using Sharprompt;
using ExpensesAnalyzer.Statements;
using ExpensesAnalyzer.Statements.Models;

namespace ExpensesAnalyzer;

internal static partial class Program
{
    // ReSharper disable once ArrangeTypeMemberModifiers
    static async Task Main(string[] args)
    {
        InitManagers();

        // Create commands
        RootCommand rootCommand = new("Root Command!");
        // rootCommand.SetHandler(RootHandler);

        Command initCommand = new("init",
            "Run initial setup, creating the necessary expected folders and files.");
        initCommand.SetHandler(InitHandler);
        
        Command parseCommand = new("parse",
            "Parse all raw statement CSVs to uniform parsed transaction data.");
        parseCommand.SetHandler(ParseHandler);
        
        Command annotateCommand = new("annotate",
            "Interactive annotation tool for current parsed transaction data.");
        annotateCommand.SetHandler(AnnotateHandler);

        // Attach sub commands
        rootCommand.AddCommand(initCommand);
        rootCommand.AddCommand(parseCommand);
        rootCommand.AddCommand(annotateCommand);

        await rootCommand.InvokeAsync(args);
    }

    static void OldMain(string[] args)
    {
        // Setup map
        ParsedTransactionMapping capitolOneMapping = new()
        {
            DateIndex = 0,
            DescriptionIndex = 3,
            AmountIndex = 5,
            BankDeterminedCategoryIndex = 4,
        };

        ParsedTransactionMapping chaseMapping = new()
        {
            DateIndex = 0,
            DescriptionIndex = 2,
            AmountIndex = 5,
            BankDeterminedCategoryIndex = 3,
        };

        ParsedTransactionMapping wfMapping = new()
        {
            DateIndex = 0,
            DescriptionIndex = 4,
            AmountIndex = 1,
        };

        // using StreamReader reader = new StreamReader("./TestData/capitolone-2022-statement.csv");
        // using StreamReader reader = new StreamReader("./TestData/chase-2022-statement.csv");
        // using StreamReader reader = new StreamReader("./TestData/wellsfargo-test.csv");

        IEnumerable<ParsedTransaction> records =
            Statement.Parse("../TestData/capitolone-2022-statement.csv", capitolOneMapping);
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