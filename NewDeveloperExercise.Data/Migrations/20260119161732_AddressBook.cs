using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewDeveloperExercise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddressBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressBookEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Address_Line2 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Address_Line3 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Address_StateAbbreviation = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Address_CountryAbbreviation = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBookEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressBookEntryContacts",
                columns: table => new
                {
                    AddressBookEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBookEntryContacts", x => new { x.AddressBookEntryId, x.Id });
                    table.ForeignKey(
                        name: "FK_AddressBookEntryContacts_AddressBookEntries_AddressBookEntryId",
                        column: x => x.AddressBookEntryId,
                        principalTable: "AddressBookEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntries_Id",
                table: "AddressBookEntries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressBookEntryContacts");

            migrationBuilder.DropTable(
                name: "AddressBookEntries");
        }
    }
}
