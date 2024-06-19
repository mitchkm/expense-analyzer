using System.Globalization;
using System.Text.Json;
using CsvHelper;
using ExpensesAnalyzer.Statements;
using ExpensesAnalyzer.Statements.Models;
using ExpensesAnalyzer.Transactions.Models;

namespace ExpensesAnalyzer.Transactions;

public class TransactionManager
{
    #region Types

    private class Config
    {
        #region Properties

        public Dictionary<string, BankStatementParseData> BankParseData { get; set; } = new();

        #endregion
    }

    #endregion

    #region Constants

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
    };

    #endregion

    #region Fields

    private readonly DirectoryManager _directoryManager;
    private Config _config = new();

    private readonly Dictionary<string, Dictionary<string, List<ParsedTransaction>>> _parsedTransactions = new();
    private HashSet<Transaction> _allTransactions = new();

    #endregion

    #region Constructors

    public TransactionManager(DirectoryManager directoryManager)
    {
        _directoryManager = directoryManager;
    }

    #endregion

    public void AddWellKnownBankParseData()
    {
        _config.BankParseData.Add("wells-fargo", new BankStatementParseData()
        {
            BankName = "Wells Fargo",
            BankId = "wells-fargo",
            DebitAmountsAreNegative = true,
            ParseMapping = new ParsedTransactionMapping()
            {
                DateIndex = 0,
                DescriptionIndex = 4,
                AmountIndex = 1,
            },
        });
        _config.BankParseData.Add("chase", new BankStatementParseData()
        {
            BankName = "Chase",
            BankId = "chase",
            DebitAmountsAreNegative = false,
            ParseMapping = new ParsedTransactionMapping()
            {
                DateIndex = 0,
                DescriptionIndex = 2,
                AmountIndex = 5,
                BankDeterminedCategoryIndex = 3,
            },
        });
        _config.BankParseData.Add("capital-one", new BankStatementParseData()
        {
            BankName = "Capital One",
            BankId = "capital-one",
            DebitAmountsAreNegative = false,
            ParseMapping = new ParsedTransactionMapping()
            {
                DateIndex = 0,
                DescriptionIndex = 3,
                AmountIndex = 5,
                BankDeterminedCategoryIndex = 4,
            },
        });
        _config.BankParseData.Add("citi", new BankStatementParseData()
        {
            BankName = "Citi Bank",
            BankId = "citi",
            DebitAmountsAreNegative = false,
            ParseMapping = new ParsedTransactionMapping()
            {
                DateIndex = 1,
                DescriptionIndex = 2,
                AmountIndex = 3,
            },
        });
        // _config.BankParseData.Add("wells-fargo", new BankStatementParseData()
        // {
        //     BankName = "Wells Fargo",
        //     BankId = "wells-fargo",
        //     DebitAmountsAreNegative = false,
        //     ParseMapping = new ParsedTransactionMapping()
        //     {
        //         DateIndex = 0,
        //         DescriptionIndex = 4,
        //         AmountIndex = 1,
        //     },
        // });
    }

    public List<Transaction> FilterToYear(DateTime year)
    {
        return _allTransactions.Where((transaction) => transaction.Date.Year == year.Year).ToList();
    }
    
    public void PromoteParsedTransactions()
    {
        foreach ((string bankId, BankStatementParseData parseData) in _config.BankParseData)
        {
            if (!_parsedTransactions.TryGetValue(bankId,
                    out Dictionary<string, List<ParsedTransaction>>? parsedTransactionsByFile))
            {
                continue;
            }

            foreach ((string _, List<ParsedTransaction> parsedTransactions) in parsedTransactionsByFile)
            {
                foreach (ParsedTransaction parsedTransaction in parsedTransactions)
                {
                    Transaction transaction = new(parsedTransaction)
                    {
                        BankId = parseData.BankId,
                        BankName = parseData.BankName,
                        Modifier = parseData.DefaultModifier,
                    };
                    _allTransactions.Add(transaction);
                }
            }
        }
    }

    public void ParseRawStatements(bool overwriteExisting = false)
    {
        foreach ((string bankId, BankStatementParseData parseData) in _config.BankParseData)
        {
            DirectoryInfo rawStatementsDir =
                Directory.CreateDirectory($"{_directoryManager.StatementsDir.FullName}\\Raw\\{bankId}");
            foreach (FileInfo statementFileInfo in rawStatementsDir.GetFiles())
            {
                if (!statementFileInfo.Extension.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                if (!_parsedTransactions.TryGetValue(bankId,
                        out Dictionary<string, List<ParsedTransaction>>? parsedTransactionsByFile))
                {
                    parsedTransactionsByFile = new Dictionary<string, List<ParsedTransaction>>();
                    _parsedTransactions.Add(bankId, parsedTransactionsByFile);
                }

                if (parsedTransactionsByFile.ContainsKey(statementFileInfo.Name) && !overwriteExisting)
                {
                    continue;
                }

                List<ParsedTransaction> transactions =
                    StatementsHelper.Parse(statementFileInfo.FullName, parseData.ParseMapping).ToList();

                if (parseData.DebitAmountsAreNegative)
                {
                    foreach (ParsedTransaction parsedTransaction in transactions)
                    {
                        parsedTransaction.NegateAmount();
                    }
                }

                parsedTransactionsByFile.TryAdd(statementFileInfo.Name, transactions);
            }
        }
    }

    public void Save()
    {
        string jsonString = JsonSerializer.Serialize(_config, SerializerOptions);
        File.WriteAllText(_directoryManager.TransactionConfigPath, jsonString);

        foreach ((string bankId, Dictionary<string, List<ParsedTransaction>> transactionsPerFile) in
                 _parsedTransactions)
        {
            DirectoryInfo parsedStatementsDir =
                Directory.CreateDirectory($"{_directoryManager.StatementsDir.FullName}\\Parsed\\{bankId}");
            foreach ((string rawStatementName, List<ParsedTransaction> transactions) in transactionsPerFile)
            {
                WriteTransactionCsv(transactions, $"{parsedStatementsDir.FullName}\\{rawStatementName}");
            }
        }

        WriteTransactionCsv(_allTransactions, $"{_directoryManager.TransactionsDir}\\transactions.csv");
    }

    public void Load()
    {
        if (File.Exists(_directoryManager.TransactionConfigPath))
        {
            string jsonString = File.ReadAllText(_directoryManager.TransactionConfigPath);
            _config = JsonSerializer.Deserialize<Config>(jsonString, SerializerOptions) ?? _config;
        }

        ReadParsedStatements();
        _allTransactions = ReadTransactionCsv<Transaction>($"{_directoryManager.TransactionsDir}\\transactions.csv")
            .ToHashSet();
    }

    private void ReadParsedStatements()
    {
        foreach ((string bankId, BankStatementParseData _) in _config.BankParseData)
        {
            DirectoryInfo parsedStatementsDir =
                Directory.CreateDirectory($"{_directoryManager.StatementsDir.FullName}\\Parsed\\{bankId}");
            foreach (FileInfo parsedStatementFile in parsedStatementsDir.GetFiles())
            {
                if (!parsedStatementFile.Extension.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                if (!_parsedTransactions.TryGetValue(bankId,
                        out Dictionary<string, List<ParsedTransaction>>? parsedTransactionsByFile))
                {
                    parsedTransactionsByFile = new Dictionary<string, List<ParsedTransaction>>();
                    _parsedTransactions.Add(bankId, parsedTransactionsByFile);
                }

                parsedTransactionsByFile.TryAdd(
                    parsedStatementFile.Name,
                    ReadTransactionCsv<ParsedTransaction>(parsedStatementFile.FullName));
            }
        }
    }

    private void WriteTransactionCsv<T>(IEnumerable<T> transactions, string path) where T : ParsedTransaction
    {
        using StreamWriter writer = new(path);
        using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(transactions);
    }

    private List<T> ReadTransactionCsv<T>(string path) where T : ParsedTransaction
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        using StreamReader reader = new(path);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<T>().ToList();
    }
}