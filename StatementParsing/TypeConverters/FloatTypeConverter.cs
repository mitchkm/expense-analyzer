﻿using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ExpensesAnalyzer.StatementParsing.TypeConverters;

// ReSharper disable once ClassNeverInstantiated.Global
public class FloatTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        float value = 0;
        float.TryParse(text, out value);
        return value;
    }
}