using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHero.Booking.Infra.DataBase.migrations
{
    public partial class createdatBooking2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2(7)",
                precision: 7,
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 38, 3, 641, DateTimeKind.Utc).AddTicks(5375),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldDefaultValue: new DateTime(2022, 8, 28, 20, 35, 26, 985, DateTimeKind.Utc).AddTicks(4645));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 35, 26, 985, DateTimeKind.Utc).AddTicks(4645),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldPrecision: 7,
                oldDefaultValue: new DateTime(2022, 8, 28, 20, 38, 3, 641, DateTimeKind.Utc).AddTicks(5375));
        }
    }
}
