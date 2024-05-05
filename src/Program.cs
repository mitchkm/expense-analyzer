using System.CommandLine;
using Sharprompt;

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
}