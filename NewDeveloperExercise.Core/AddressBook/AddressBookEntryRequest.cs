namespace NewDeveloperExercise.Core.AddressBook;

public record AddressBookEntryRequest
{
	public required string Name { get; init; }
	public required Address Address { get; init; }
	public double? Latitude { get; init; }
	public double? Longitude { get; init; }
	public string? TimeZone { get; init; }
	public IReadOnlyList<AddressBookContact> Contacts { get; init; } = [];
}