using Sharprompt;

namespace ExpensesAnalyzer;

internal static partial class Program
{
    private static void RootHandler()
    {
        // TODO What do we do if we call just root command?
        Console.WriteLine("Hello World!");
        Prompt.Input<string>("Enter test string?", validators: new[] { Validators.MinLength(4) });
    }

    private static void InitHandler()
    {
        // TODO setup necessary folders run to setup an empty directory.
    }

    private static void ParseHandler()
    {
        _transactionManager?.ParseRawStatements();
        _transactionManager?.PromoteParsedTransactions();
        _transactionManager?.Save();
    }
    
    private static void AnnotateHandler()
    {
        
    }
}