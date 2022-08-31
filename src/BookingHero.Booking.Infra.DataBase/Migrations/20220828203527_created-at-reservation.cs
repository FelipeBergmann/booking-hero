using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHero.Booking.Infra.DataBase.migrations
{
    public partial class createdatBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 35, 26, 985, DateTimeKind.Utc).AddTicks(4645),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 28, 20, 33, 14, 197, DateTimeKind.Utc).AddTicks(1387));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 33, 14, 197, DateTimeKind.Utc).AddTicks(1387),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(3)",
                oldPrecision: 3,
                oldDefaultValue: new DateTime(2022, 8, 28, 20, 35, 26, 985, DateTimeKind.Utc).AddTicks(4645));
        }
    }
}
