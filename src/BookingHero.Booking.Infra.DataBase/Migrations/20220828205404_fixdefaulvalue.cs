using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHero.Booking.Infra.DataBase.migrations
{
    public partial class fixdefaulvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7,
                oldDefaultValue: new DateTime(2022, 8, 28, 20, 38, 3, 641, DateTimeKind.Utc).AddTicks(5375));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 38, 3, 641, DateTimeKind.Utc).AddTicks(5375),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7,
                oldDefaultValueSql: "getdate()");
        }
    }
}
