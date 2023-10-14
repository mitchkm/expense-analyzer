namespace ExpensesAnalyzer.Categories.Models;

public class Category
{
    #region Fields

    public string Name = string.Empty;
    public List<string> SubCategoryNames = new();

    #endregion
}