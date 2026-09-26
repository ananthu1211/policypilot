using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PolicyPilot.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PolicyNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    HolderName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PolicyId = table.Column<int>(type: "integer", nullable: false),
                    ClaimDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "Id", "HolderName", "PolicyNumber", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "Ava Patel", "POL-1001", "Active", "Auto" },
                    { 2, "Liam Chen", "POL-1002", "Active", "Home" },
                    { 3, "Noah Williams", "POL-1003", "Active", "Health" },
                    { 4, "Emma Garcia", "POL-1004", "Lapsed", "Life" },
                    { 5, "Olivia Brown", "POL-1005", "Active", "Auto" },
                    { 6, "Mason Davis", "POL-1006", "Cancelled", "Home" },
                    { 7, "Sophia Miller", "POL-1007", "Active", "Health" },
                    { 8, "James Wilson", "POL-1008", "Active", "Life" },
                    { 9, "Isabella Moore", "POL-1009", "Lapsed", "Auto" },
                    { 10, "Ethan Taylor", "POL-1010", "Active", "Home" },
                    { 11, "Mia Anderson", "POL-1011", "Active", "Health" },
                    { 12, "Lucas Thomas", "POL-1012", "Cancelled", "Life" }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ClaimDate", "Description", "PolicyId", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 1, 14), "Rear bumper damage from a parking lot collision.", 1, "Approved" },
                    { 2, new DateOnly(2026, 6, 2), "Windshield cracked by highway debris.", 1, "UnderReview" },
                    { 3, new DateOnly(2026, 2, 8), "Kitchen water leak damaged the floor.", 2, "Submitted" },
                    { 4, new DateOnly(2025, 11, 19), "Emergency room visit for a broken wrist.", 3, "Approved" },
                    { 5, new DateOnly(2026, 4, 27), "Follow-up physical therapy sessions.", 3, "UnderReview" },
                    { 6, new DateOnly(2025, 9, 3), "Beneficiary update requested after a name change.", 4, "Rejected" },
                    { 7, new DateOnly(2026, 3, 11), "Hail damage to the hood and roof.", 5, "Submitted" },
                    { 8, new DateOnly(2026, 7, 21), "Side mirror replaced after a sideswipe.", 5, "Approved" },
                    { 9, new DateOnly(2025, 12, 16), "Storm lifted shingles off the garage.", 6, "Rejected" },
                    { 10, new DateOnly(2026, 5, 9), "Annual specialist consultation.", 7, "Approved" },
                    { 11, new DateOnly(2026, 1, 30), "Accidental death rider documentation review.", 8, "UnderReview" },
                    { 12, new DateOnly(2026, 8, 4), "Waiver of premium request during disability.", 8, "Submitted" },
                    { 13, new DateOnly(2025, 10, 22), "Towed after an engine failure on the freeway.", 9, "Approved" },
                    { 14, new DateOnly(2026, 2, 18), "Burst pipe in the basement.", 10, "UnderReview" },
                    { 15, new DateOnly(2026, 6, 15), "Prescription reimbursement for a covered condition.", 11, "Submitted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PolicyNumber",
                table: "Policies",
                column: "PolicyNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Policies");
        }
    }
}
