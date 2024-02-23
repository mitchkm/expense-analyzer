using ExpensesAnalyzer.Categories;
using ExpensesAnalyzer.Transactions;

namespace ExpensesAnalyzer;

internal static partial class Program
{
    private static DirectoryManager? _directoryManager = null;
    private static TransactionManager? _transactionManager = null;
    private static CategoryManager? _categoryManager = null;

    private static void InitManagers()
    {
        _directoryManager = new DirectoryManager();
        _transactionManager = new TransactionManager(_directoryManager);
        _categoryManager = new CategoryManager(_directoryManager);
    } 
}