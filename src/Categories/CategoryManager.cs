using System.Text.Json;
using ExpensesAnalyzer.Categories.Models;

namespace ExpensesAnalyzer.Categories;

public class CategoryManager
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

    private Dictionary<string, Category> _categories = new();

    #endregion

    #region Constructors

    public CategoryManager(DirectoryManager directoryManager)
    {
        _directoryManager = directoryManager;
    }

    #endregion

    public void AddDefaultCategories()
    {
        _categories.Clear();
        // Anything you pay toward keeping a roof over your head is considered a housing expense.
        // That includes rent or mortgage payments, property taxes, HOA dues, and home maintenance costs.
        _categories.Add("Housing", new Category()
        {
            Name = "Housing",
            SubCategoryNames = new HashSet<string>(),
        });
        
        // This budget category includes car payments, registration and DMV fees.
        // Also gas, maintenance, parking, tolls, and public transit.
        _categories.Add("Transportation", new Category()
        {
            Name = "Transportation",
            SubCategoryNames = new HashSet<string>()
            {
                "Gas",
                "Maintenance",
            },
        });
        
        // Groceries, Dining out, Snacks, etc.
        _categories.Add("Food", new Category()
        {
            Name = "Food",
            SubCategoryNames = new HashSet<string>()
            {
                "Grocery",
                "Dining Out",
                "Alcohol",
            },
        });
        
        // Gas, electricity, water, sewage, internet, phone, etc. 
        _categories.Add("Utilities", new Category()
        {
            Name = "Utilities",
            SubCategoryNames = new HashSet<string>()
            {
                "Gas",
                "Water",
                "Electric",
                "Internet",
                "Phone",
                "Sewage",
            },
        });
        
        // Home/Rental, Medical, Life, Auto, etc. 
        _categories.Add("Insurance", new Category()
        {
            Name = "Insurance",
            SubCategoryNames = new HashSet<string>()
            {
                "Home",
                "Rental",
                "Medical",
                "Auto",
                "Life",
            },
        });
        
        // Out of pocket cost, primary care, dental care, vision, prescriptions, over-counter drugs, etc. 
        _categories.Add("Healthcare", new Category()
        {
            Name = "Healthcare",
            SubCategoryNames = new HashSet<string>()
            {
                "Primary Care",
                "Dental Care",
                "Vision Care",
                "Prescriptions",
            },
        });
        
        // Saving, Investing, & Debt
        // Emergency fund, 401(k), IRA, brokerage, car loan, etc. 
        _categories.Add("SID", new Category()
        {
            Name = "SID",
            SubCategoryNames = new HashSet<string>()
            {
                "Savings",
                "Investment",
                "Debt",
            },
        });
        
        // Anything that is considered personal care but not directly essential, and other personal expenses
        _categories.Add("Personal", new Category()
        {
            Name = "Personal",
            SubCategoryNames = new HashSet<string>()
            {
                "Gift",
                "Cloths",
                "Personal Care",
            },
        });
        
        // Event tickets, subscriptions, travel/vacations, family activities, hobbies, video games, computer
        _categories.Add("Recreation", new Category()
        {
            Name = "Recreation",
            SubCategoryNames = new HashSet<string>()
            {
                "Video Game",
                "Card Game",
            },
        });
    }
    
    public List<string> GetListOfCategories()
    {
        return _categories.Keys.ToList();
    }

    public List<string> GetListOfSubCategories(string categoryName)
    {
        return !IsValidCategory(categoryName)
            ? new List<string>()
            : _categories[categoryName].SubCategoryNames.ToList();
    }

    public bool IsValidCategory(string categoryName)
    {
        return _categories.ContainsKey(categoryName);
    }

    public bool IsValidSubCategory(string categoryName, string subCategoryName)
    {
        return IsValidCategory(categoryName) && _categories[categoryName].SubCategoryNames.Contains(subCategoryName);
    }

    public void AddCategory(string categoryName)
    {
        _categories.Add(categoryName, new Category
        {
            Name = categoryName,
        });
    }

    public bool TryAddSubCategory(string categoryName, string subCategoryName)
    {
        if (!IsValidCategory(categoryName))
        {
            return false;
        }

        _categories[categoryName].SubCategoryNames.Add(subCategoryName);
        return true;
    }

    public void Save()
    {
        string jsonString = JsonSerializer.Serialize(_config, SerializerOptions);
        File.WriteAllText(_directoryManager.CategoriesConfigPath, jsonString);

        jsonString = JsonSerializer.Serialize(_categories, SerializerOptions);
        File.WriteAllText($"{_directoryManager.CategoriesDir.FullName}\\AllCategories.json", jsonString);
    }

    public void Load()
    {
        if (File.Exists(_directoryManager.CategoriesConfigPath))
        {
            string jsonString = File.ReadAllText(_directoryManager.CategoriesConfigPath);
            _config = JsonSerializer.Deserialize<Config>(jsonString, SerializerOptions) ?? _config;
        }

        string allCategoriesFilePath = $"{_directoryManager.CategoriesDir.FullName}\\AllCategories.json";
        if (File.Exists(allCategoriesFilePath))
        {
            string jsonString = File.ReadAllText(allCategoriesFilePath);
            _categories = JsonSerializer.Deserialize<Dictionary<string, Category>>(jsonString, SerializerOptions) ??
                          _categories;
        }
    }
}