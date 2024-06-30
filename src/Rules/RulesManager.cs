using System.Text.Json;

namespace ExpensesAnalyzer.Rules;

public class RulesManager
{
    #region Types

    private class Config
    {
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

    #endregion

    #region Constructors

    public RulesManager(DirectoryManager directoryManager)
    {
        _directoryManager = directoryManager;
    }

    #endregion

    public void Save()
    {
        string jsonString = JsonSerializer.Serialize(_config, SerializerOptions);
        File.WriteAllText(_directoryManager.RulesConfigPath, jsonString);
    }

    public void Load()
    {
        if (File.Exists(_directoryManager.RulesConfigPath))
        {
            string jsonString = File.ReadAllText(_directoryManager.RulesConfigPath);
            _config = JsonSerializer.Deserialize<Config>(jsonString, SerializerOptions) ?? _config;
        }
    }
}