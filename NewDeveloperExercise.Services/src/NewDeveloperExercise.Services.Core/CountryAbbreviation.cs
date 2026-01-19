using NewDeveloperExercise.Services.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace NewDeveloperExercise.Services.Core;

public record CountryAbbreviation : StringValueObject<CountryAbbreviation>, IStringValueObject<CountryAbbreviation>
{
	private CountryAbbreviation(string value) : base(value) { }

	public static bool TryCreate(string? value, [NotNullWhen(returnValue: true)] out CountryAbbreviation? countryAbbreviation, [NotNullWhen(returnValue: false)] out string? errorMessage)
	{
		if (value == null
			|| value.Length != 2
			|| value.Any(c => !char.IsLetter(c)))
		{
			countryAbbreviation = null;
			errorMessage = $"Invalid country abbreviation: {value ?? "NULL"}";
			return false;
		}

		countryAbbreviation = new CountryAbbreviation(value.ToUpperInvariant());
		errorMessage = null;
		return true;
	}

	public static CountryAbbreviation US { get; } = Create("US");
}