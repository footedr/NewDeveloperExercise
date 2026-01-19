using NewDeveloperExercise.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace NewDeveloperExercise.Core;

public record PhoneNumber : StringValueObject<PhoneNumber>, IStringValueObject<PhoneNumber>
{
	private PhoneNumber(string value) : base(value) { }

	public static bool TryCreate(string? value, [NotNullWhen(returnValue: true)] out PhoneNumber? phoneNumber, [NotNullWhen(returnValue: false)] out string? errorMessage)
	{
		// Validating phone numbers can be complicated, especially when you factor in international
		// numbers and extensions.  Here we are just making sure that there are between 10 and 20 digits,
		// and allow for a handlful of common separators.
		const string validSeparators = "+ -()x./";
		if (value == null
			|| value.Count(char.IsNumber) < 10
			|| value.Count(char.IsNumber) > 20
			|| value.Any(digit => !char.IsNumber(digit) && !validSeparators.Contains(digit)))
		{
			phoneNumber = null;
			errorMessage = $"Invalid phone number: {value ?? "NULL"}";
			return false;
		}

		phoneNumber = new PhoneNumber(value);
		errorMessage = null;
		return true;
	}
}