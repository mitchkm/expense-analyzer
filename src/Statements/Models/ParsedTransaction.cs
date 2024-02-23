namespace ExpensesAnalyzer.Statements.Models;

public class ParsedTransaction : IEquatable<ParsedTransaction>
{
    #region Properties

    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public float Amount { get; set; }
    public string BankDeterminedCategory { get; set; } = string.Empty;

    #endregion

    public bool Equals(ParsedTransaction? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Date.Equals(other.Date)
               && Description == other.Description
               && Amount.Equals(other.Amount);
    }

    public bool GetIsDebit()
    {
        return Amount > 0;
    }

    public void NegateAmount()
    {
        Amount *= -1;
    }

    public override string ToString()
    {
        return $"{Date} | {Amount} | {Description} | {BankDeterminedCategory}";
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType().IsInstanceOfType(GetType()) && Equals((ParsedTransaction)obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(Date, Description, Amount);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }
}