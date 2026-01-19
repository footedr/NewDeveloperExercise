using NewDeveloperExercise.Services.SharedKernel;

namespace NewDeveloperExercise.Services.Core.AddressBook;

public class AddressBookEntry : Entity<Guid>
{
	private readonly List<AddressBookContact> _contacts = [];

	private AddressBookEntry()
	{
	}

	public override required Guid Id { get; init; }
	public required string Name { get; set; }
	public required Address Address { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public string? TimeZone { get; set; }
	public required DateTimeOffset CreatedAt { get; init; }
	public DateTimeOffset UpdatedAt { get; set; }
	public DateTimeOffset? DeletedAt { get; set; }
	public IReadOnlyList<AddressBookContact> Contacts => _contacts;

	public static AddressBookEntry Create(AddressBookEntryRequest request, DateTimeOffset now)
	{
		var addressBookEntry = new AddressBookEntry
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			Address = request.Address,
			Latitude = request.Latitude,
			Longitude = request.Longitude,
			TimeZone = request.TimeZone,
			CreatedAt = now,
			UpdatedAt = now
		};

		addressBookEntry.UpdateContacts(request.Contacts);

		addressBookEntry.RecordEvent(new AddressBookEntryCreated(addressBookEntry.Id, addressBookEntry.Address));

		return addressBookEntry;
	}

	public void UpdateAddress(Address address)
	{
		if (Address == address)
		{
			return;
		}

		Address = address;
		RecordEvent(new AddressBookEntryUpdated(Id, Address));
	}

	public void UpdateContacts(IReadOnlyList<AddressBookContact> contacts)
	{
		_contacts.Clear();
		_contacts.AddRange(contacts);
	}
}

public abstract record AddressBookEvent(Guid Id) : IEvent;
public record AddressBookEntryCreated(Guid Id, Address? Address) : AddressBookEvent(Id);
public record AddressBookEntryUpdated(Guid Id, Address? Address) : AddressBookEvent(Id);

public record AddressBookContact
{
	public required string Name { get; set; }
	public PhoneNumber? PhoneNumber { get; set; }
	public EmailAddress? EmailAddress { get; set; }
	public bool IsPrimary { get; set; }
}
