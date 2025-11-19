using DebantErp.Domain.Base;
using System.Text.RegularExpressions;

namespace DebantErp.Domain.ValueObjects;
public class Email : ValueObject
{
    public string Value { get; private set; }

    // Constructor for serialization/EF if needed
    //protected Email() { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty", nameof(value));
        
        if (!value.Contains("@") || !value.Contains(".")) 
            throw new ArgumentException("Invalid email format", nameof(value));

        Value = value.Trim().ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}

