using NewDeveloperExercise.Services.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace NewDeveloperExercise.Services.Core;

public record PostalCode : StringValueObject<PostalCode>, IStringValueObject<PostalCode>
{
	private PostalCode(string value) : base(value) { }

	public static bool TryCreate(string? value, [NotNullWhen(returnValue: true)] out PostalCode? postalCode, [NotNullWhen(returnValue: false)] out string? errorMessage)
	{
		// International postal codes can have a wide variety of formats.  Here we assume a postal code is valid
		// if it has 2-9 alphanumeric digits, with maybe some ' ' or '-' characters used a separators.
		static bool isValidDigit(char c) => char.IsDigit(c) || char.IsLetter(c);

		static bool isValidSeparator(char c) => " -".Contains(c);

		if (value == null
			|| value.Count(isValidDigit) < 2
			|| value.Count(isValidDigit) > 9
			|| value.Any(c => !isValidDigit(c) && !isValidSeparator(c)))
		{
			postalCode = null;
			errorMessage = $"Invalid postal code: {value ?? "NULL"}";
			return false;
		}

		postalCode = new PostalCode(value);
		errorMessage = null;
		return true;
	}
}