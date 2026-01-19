using Microsoft.EntityFrameworkCore;
using NewDeveloperExercise.Services.SharedKernel;

namespace NewDeveloperExercise.Services.Data;

public class NewDeveloperExerciseContext(DbContextOptions<NewDeveloperExerciseContext> options) : DbContext(options)
{
	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);

		configurationBuilder.Properties(typeof(StringValueObject<>), builder =>
		{
			builder.HaveConversion<string>()
				.HaveMaxLength(450);
		});

		configurationBuilder.Properties<string>().HaveMaxLength(450);

		configurationBuilder.Properties<Enum>()
			.HaveConversion<string>()
			.HaveMaxLength(450);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AddressBookConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}

