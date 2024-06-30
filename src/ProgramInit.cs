using ExpensesAnalyzer.Categories;
using ExpensesAnalyzer.Rules;
using ExpensesAnalyzer.Transactions;

namespace ExpensesAnalyzer;

internal static partial class Program
{
    private static DirectoryManager? _directoryManager = null;
    private static TransactionManager? _transactionManager = null;
    private static CategoryManager? _categoryManager = null;
    private static RulesManager? _rulesManager = null;

    private static void InitManagers()
    {
        _directoryManager = new DirectoryManager();
        _directoryManager.EnsureDirectories();
        _transactionManager = new TransactionManager(_directoryManager);
        _categoryManager = new CategoryManager(_directoryManager);
        _rulesManager = new RulesManager(_directoryManager);
    } 
}