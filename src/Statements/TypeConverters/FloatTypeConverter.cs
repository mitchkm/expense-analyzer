using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ExpensesAnalyzer.Statements.TypeConverters;

// ReSharper disable once ClassNeverInstantiated.Global
public class FloatTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        bool result = float.TryParse(text, out float value);
        return result ? value : 0;
    }
}