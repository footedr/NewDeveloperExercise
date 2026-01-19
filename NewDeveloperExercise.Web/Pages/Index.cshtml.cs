using Microsoft.AspNetCore.Mvc.RazorPages;
using NewDeveloperExercise.Core;
using NewDeveloperExercise.Core.AddressBook;
using NewDeveloperExercise.Data;

namespace NewDeveloperExercise.Web.Pages;

public class IndexModel : PageModel
{
	private readonly NewDeveloperExerciseContext _ctx;

	public IndexModel(NewDeveloperExerciseContext ctx)
	{
		_ctx = ctx;
	}

	public async Task OnGet()
	{
		var entry = AddressBookEntry.Create(new AddressBookEntryRequest
		{
			Name = "Test",
			Address = new Address
			{
				City = "Germantown",
				CountryAbbreviation = CountryAbbreviation.US,
				Line1 = "59 Scott Ct.",
				PostalCode = PostalCode.Create("45327"),
				StateAbbreviation = StateAbbreviation.Create("OH"),
				Line2 = "Master Bedroom",
				Line3 = "And basement"
			}
		}, DateTimeOffset.Now);

		_ctx.Add(entry);
		_ctx.SaveChanges();
	}
}
