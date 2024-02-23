namespace ExpensesAnalyzer.Statements.Models;

public class ParsedTransaction : IEquatable<ParsedTransaction>
{
    #region Fields

    public DateTime Date;
    public string Description = "";
    public float Amount;
    public string BankDeterminedCategory = "";

    #endregion

    #region Properties

    public bool IsDebit => Amount > 0;

    #endregion

    public void NegateAmount()
    {
        Amount *= -1;
    }

    public override string ToString()
    {
        return $"{Date} | {Amount} | {Description} | {BankDeterminedCategory}";
    }

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