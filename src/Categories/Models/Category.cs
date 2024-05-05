namespace ExpensesAnalyzer.Categories.Models;

public class Category
{
    #region Fields

    public string Name { get; set; } = string.Empty;
    public HashSet<string> SubCategoryNames { get; set; } = new();

    #endregion
}