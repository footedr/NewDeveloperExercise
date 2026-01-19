using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewDeveloperExercise.Services.Core;
using NewDeveloperExercise.Services.Core.AddressBook;

namespace NewDeveloperExercise.Services.Data;

internal class AddressBookConfiguration : IEntityTypeConfiguration<AddressBookEntry>
{
	public void Configure(EntityTypeBuilder<AddressBookEntry> builder)
	{
		builder.ToTable("AddressBookEntries");
		builder.HasIndex(x => x.Id);

		builder.OwnsOne(_ => _.Address, addressBuilder =>
		{
			addressBuilder.Property(x => x!.StateAbbreviation)
				.IsRequired()
				.HasConversion(
					state => state.Value,
					value => StateAbbreviation.Create(value));

			addressBuilder.Property(x => x!.PostalCode)
				.IsRequired()
				.HasConversion(
					postal => postal.Value,
					value => PostalCode.Create(value));

			addressBuilder.Property(x => x!.CountryAbbreviation)
				.IsRequired()
				.HasConversion(
					country => country.Value,
					value => CountryAbbreviation.Create(value));
		});

		builder.OwnsMany(x => x.Contacts, contactBuilder =>
		{
			contactBuilder.ToTable("AddressBookEntryContacts");
			contactBuilder.Property(c => c.Name);
			contactBuilder.Property(c => c.IsPrimary);

			contactBuilder.Property(c => c.PhoneNumber)
				.HasConversion(
					phone => phone == null ? null : phone.Value,
					value => value == null ? null : PhoneNumber.Create(value));

			contactBuilder.Property(c => c.EmailAddress)
				.HasConversion(
					email => email == null ? null : email.Value,
					value => value == null ? null : EmailAddress.Create(value));
		});
	}
}
