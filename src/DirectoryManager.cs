namespace ExpensesAnalyzer;

public class DirectoryManager
{
    public DirectoryInfo NewStatementsDir { get; private set; }
    public DirectoryInfo StatementsDir { get; private set; }
    public DirectoryInfo TransactionsDir { get; private set; }
    public string TransactionConfigPath => $"{TransactionsDir.FullName}\\transactions.config.json";
    public DirectoryInfo CategoriesDir { get; private set; }
    
    public void CreateDirectoriesIfMissing()
    {
        NewStatementsDir = Directory.CreateDirectory("New_Statements");
        StatementsDir = Directory.CreateDirectory("Statements");
        Directory.CreateDirectory("Statements\\Raw");
        Directory.CreateDirectory("Statements\\Parsed");
        TransactionsDir = Directory.CreateDirectory("Transactions");
        CategoriesDir = Directory.CreateDirectory("Categories");
    }
    
    // Directory structure
    // .
    // |- config.json
    // |- New_Statements
    // |- Statements
    // |- |- Raw
    // |- |- Parsed
    // |- |- banks.json
    // |- Transactions
    // |- Categories
    // |- 
}