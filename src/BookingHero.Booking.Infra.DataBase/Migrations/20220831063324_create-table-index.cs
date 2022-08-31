using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHero.Booking.Infra.DataBase.Migrations
{
    public partial class createtableindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Code_CheckIn_CheckOut",
                table: "Reservations",
                columns: new[] { "Code", "CheckIn", "CheckOut" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Name",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Code_CheckIn_CheckOut",
                table: "Reservations");
        }
    }
}
