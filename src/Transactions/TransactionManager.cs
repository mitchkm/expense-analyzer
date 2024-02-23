using ExpensesAnalyzer.Statements.Models;
using ExpensesAnalyzer.Transactions.Models;

namespace ExpensesAnalyzer.Transactions;

public class TransactionManager
{
    private DirectoryManager _directoryManager;
    private Dictionary<string, Dictionary<string, List<ParsedTransaction>>> _parsedTransactions = new ();
    private List<Transaction> _allTransactions = new ();

    public TransactionManager(DirectoryManager directoryManager)
    {
        _directoryManager = directoryManager;
    }
}