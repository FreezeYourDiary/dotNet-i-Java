using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Exchange = table.Column<string>(type: "TEXT", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    Sector = table.Column<string>(type: "TEXT", nullable: true),
                    MarketCapitalization = table.Column<string>(type: "TEXT", nullable: false),
                    Week52High = table.Column<string>(type: "TEXT", nullable: false),
                    Week52Low = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Currency", "Exchange", "MarketCapitalization", "Name", "Sector", "Symbol", "Week52High", "Week52Low" },
                values: new object[] { 1, "USD", "NYSE", "231723270000", "International Business Machines", null, "IBN", "266.45", "157.33" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
