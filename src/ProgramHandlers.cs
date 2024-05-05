using ExpensesAnalyzer.Transactions.Models;
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
        _directoryManager?.EnsureDirectories();
    }

    private static void ParseHandler()
    {
        _directoryManager?.EnsureDirectories();
        _transactionManager?.Load();

        _transactionManager?.ParseRawStatements();
        _transactionManager?.PromoteParsedTransactions();

        _transactionManager?.Save();
    }

    private static void AnnotateHandler()
    {
        _directoryManager?.EnsureDirectories();
        _transactionManager?.Load();
        _categoryManager?.Load();

        foreach (Transaction transaction in _transactionManager != null ? _transactionManager.FilterToYear(new DateTime(2022, 1, 1)) : new List<Transaction>())
        {
            if (!string.IsNullOrWhiteSpace(transaction.PrimaryCategory))
            {
                continue;
            }
            
            bool done = Prompt.Confirm("Would like to quit?");
            if (done)
            {
                break;
            }
            
            Console.WriteLine($"Annotate transaction: {transaction}");
            string cat = Prompt.Select("Select a category", _categoryManager?.GetListOfCategories());

            if (!string.IsNullOrWhiteSpace(cat))
            {
                transaction.PrimaryCategory = cat;
                string subCat = Prompt.Select("Select a category", _categoryManager?.GetListOfSubCategories(cat));
            
                if (!string.IsNullOrWhiteSpace(subCat))
                {
                    transaction.SubCategory = subCat;
                }
            }
        }

        _transactionManager?.Save();
        _categoryManager?.Save();
    }
}