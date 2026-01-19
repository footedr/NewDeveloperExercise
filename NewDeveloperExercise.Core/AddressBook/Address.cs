using NewDeveloperExercise.SharedKernel;
using System.Text.RegularExpressions;

namespace NewDeveloperExercise.Core.AddressBook;

public record Address
{
	public string? Line1 { get; init; }

	public string? Line2 { get; init; }

	public string? Line3 { get; init; }

	public required string City { get; init; }

	public required StateAbbreviation StateAbbreviation { get; init; }

	public required PostalCode PostalCode { get; init; }

	public required CountryAbbreviation CountryAbbreviation { get; init; } = CountryAbbreviation.US;

	private static string? Normalize(string? s)
	{
		return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
	}

	public virtual bool Equals(Address? other)
	{
		if (other is null)
		{
			return false;
		}

		return Normalize(Line1) == Normalize(other.Line1) &&
			   Normalize(Line2) == Normalize(other.Line2) &&
			   Normalize(Line3) == Normalize(other.Line3) &&
			   Normalize(City) == Normalize(other.City) &&
			   StateAbbreviation == other.StateAbbreviation &&
			   PostalCode == other.PostalCode &&
			   CountryAbbreviation == other.CountryAbbreviation;
	}

	public override int GetHashCode()
	{
		string?[] components =
		[
			Normalize(Line1),
			Normalize(Line2),
			Normalize(Line3),
			Normalize(City),
			StateAbbreviation.Value,
			PostalCode.Value,
			CountryAbbreviation.Value
		];

		return components.GetDeterministicHashCode();
	}
	private static string? NormalizeLine1(string? line1)
	{
		if (string.IsNullOrWhiteSpace(line1))
		{
			return string.Empty;
		}

		var noPunctuation = Regex.Replace(line1.Trim().ToUpperInvariant(), @"[^\w\s]", "");
		var suffixes = new[] { "AVE", "AVENUE", "BLVD", "BOULEVARD", "CT", "COURT", "DR", "DRIVE", "HIGHWAY", "HWY", "LANE", "LN", "PARKWAY", "PKWY", "PL", "PLACE", "RD", "ROAD", "ST", "STREET", "WAY" };
		var words = noPunctuation.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

		var filteredWords = words
			.Where(w => !suffixes.Contains(w))
			.ToArray();

		return string.Join(' ', filteredWords);
	}

	public bool Matches(Address? other)
	{
		if (other is null)
		{
			return false;
		}

		return NormalizeLine1(Line1) == NormalizeLine1(other.Line1) &&
			string.Equals(Normalize(City), Normalize(other.City), StringComparison.OrdinalIgnoreCase) &&
			StateAbbreviation == other.StateAbbreviation &&
			PostalCode == other.PostalCode &&
			CountryAbbreviation == other.CountryAbbreviation;
	}
}