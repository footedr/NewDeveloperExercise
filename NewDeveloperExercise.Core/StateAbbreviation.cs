using NewDeveloperExercise.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace NewDeveloperExercise.Core;

public record StateAbbreviation : StringValueObject<StateAbbreviation>, IStringValueObject<StateAbbreviation>
{
	private StateAbbreviation(string value) : base(value) { }

	public static bool TryCreate(string? value, [NotNullWhen(returnValue: true)] out StateAbbreviation? stateAbbreviation, [NotNullWhen(returnValue: false)] out string? errorMessage)
	{
		if (value == null
			|| value.Length != 2
			|| value.Any(c => !char.IsLetter(c)))
		{
			stateAbbreviation = null;
			errorMessage = $"Invalid state abbreviation: {value ?? "NULL"}";
			return false;
		}

		stateAbbreviation = new StateAbbreviation(value.ToUpperInvariant());
		errorMessage = null;
		return true;
	}

	public static implicit operator StateAbbreviation?(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}

		return Create(value);
	}
}