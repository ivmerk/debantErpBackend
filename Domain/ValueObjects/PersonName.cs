using DebantErp.Domain.Base;
using System.Text.RegularExpressions;

namespace DebantErp.Domain.ValueObjects;

public class PersonName : ValueObject
{
    public string First { get; }
    public string Middle { get; }
    public string Last { get; }

    public string FullName => $"{First} {Middle} {Last}".Trim();

    protected PersonName() { }

    public PersonName(string first, string middle, string last)
    {
        if (string.IsNullOrWhiteSpace(first))
            throw new ArgumentException("First name is required", nameof(first));
        
        if (string.IsNullOrWhiteSpace(last))
            throw new ArgumentException("Last name is required", nameof(last));

        First = first.Trim();
        Middle = middle?.Trim() ?? string.Empty;
        Last = last.Trim();
        if (!IsOnlyLetters(First))
            throw new ArgumentException("First name must contain only letters.", nameof(first));

        if (!string.IsNullOrEmpty(Middle) && !IsOnlyLetters(Middle))
            throw new ArgumentException("Middle name must contain only letters.", nameof(middle));

        if (!IsOnlyLetters(Last))
            throw new ArgumentException("Last name must contain only letters.", nameof(last));

    }

    public PersonName(string first, string last) : this(first, string.Empty, last) { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Middle;
        yield return Last;
    }

    public override string ToString() => FullName;
    private static bool IsOnlyLetters(string input)
    {
      return Regex.IsMatch(input, @"^[\p{L}]+$");
    }
}


