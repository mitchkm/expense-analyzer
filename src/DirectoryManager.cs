namespace ExpensesAnalyzer;

public class DirectoryManager
{
    public DirectoryInfo NewStatementsDir { get; private set; }
    public DirectoryInfo StatementsDir { get; private set; }
    public DirectoryInfo StatementsRawDir { get; private set; }
    public DirectoryInfo StatementsParsedDir { get; private set; }
    public DirectoryInfo TransactionsDir { get; private set; }
    public string TransactionConfigPath => $"{TransactionsDir.FullName}\\transactions.config.json";
    public DirectoryInfo CategoriesDir { get; private set; }
    public string CategoriesConfigPath => $"{CategoriesDir.FullName}\\categories.config.json";
    
    public DirectoryInfo RulesDir { get; private set; }
    public string RulesConfigPath => $"{CategoriesDir.FullName}\\rules.config.json";

    public void EnsureDirectories()
    {
        NewStatementsDir = Directory.CreateDirectory("New_Statements");
        StatementsDir = Directory.CreateDirectory("Statements");
        StatementsRawDir = Directory.CreateDirectory("Statements\\Raw");
        StatementsParsedDir = Directory.CreateDirectory("Statements\\Parsed");
        TransactionsDir = Directory.CreateDirectory("Transactions");
        CategoriesDir = Directory.CreateDirectory("Categories");
        RulesDir = Directory.CreateDirectory("Rules");
    }

    // Directory structure
    // .
    // |- config.json
    // |- New_Statements
    // |- Statements
    // |- |- Raw
    // |- |- Parsed
    // |- Transactions
    // |- Categories
    // |- 
}