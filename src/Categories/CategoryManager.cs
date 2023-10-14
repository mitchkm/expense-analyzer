using ExpensesAnalyzer.Categories.Models;

namespace ExpensesAnalyzer.Categories;

public class CategoryManager
{
    private Dictionary<string, Category> _categories = new ();

    public bool LoadData(DirectoryInfo directory)
    {
        return true;
    }

    public bool IsValidCategory(string category)
    {
        return _categories.ContainsKey(category);
    }
    
    public bool IsValidSubCategory(string category, string subCategory)
    {
        return IsValidCategory(category) && _categories[category].SubCategoryNames.Contains(subCategory);
    }
}