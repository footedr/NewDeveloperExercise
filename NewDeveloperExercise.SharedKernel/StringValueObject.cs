using System.Diagnostics.CodeAnalysis;

namespace NewDeveloperExercise.SharedKernel;

public interface IStringValueObject
{
	string Value { get; }
}

public interface IStringValueObject<T> : IStringValueObject
	where T : IStringValueObject<T>
{
	abstract static bool TryCreate(string? value, [NotNullWhen(true)] out T? parsedValue, [NotNullWhen(false)] out string? errorMessage);
}

public abstract record StringValueObject<T> where T : IStringValueObject<T>
{
	private StringValueObject()
	{
		Value = default!;
	}

	protected StringValueObject(string value)
	{
		Value = value;
	}

	public string Value { get; private set; }

	public static T Create(string? value)
	{
		if (!T.TryCreate(value, out var valueObject, out var errorMessage))
		{
			throw new FormatException(errorMessage);
		}

		return valueObject;
	}

	// This is a special parse method that will be automatically discovered by ASP.NET for parameter binding
	public static bool TryParse(string value, out T? result)
	{
		return T.TryCreate(value, out result, out var _);
	}

	public static explicit operator string(StringValueObject<T> valueObject) => valueObject.Value;
}

public static class StringValueObjectExtensions
{
	public static string Unwrap(this IStringValueObject value)
	{
		return value.Value;
	}
}