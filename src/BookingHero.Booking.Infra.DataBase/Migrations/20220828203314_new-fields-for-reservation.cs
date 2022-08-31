using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHero.Booking.Infra.DataBase.migrations
{
    public partial class newfieldsforBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 28, 20, 33, 14, 197, DateTimeKind.Utc).AddTicks(1387));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");
        }
    }
}
