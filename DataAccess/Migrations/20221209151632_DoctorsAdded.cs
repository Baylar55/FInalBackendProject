using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DoctorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorsCount",
                table: "WhyChoose");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "WhyChoose");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "WhyChoose");

            migrationBuilder.DropColumn(
                name: "SatisfiedPatientsCount",
                table: "WhyChoose");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "Speciality",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "DoctorsCount",
                table: "WhyChoose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "WhyChoose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Quality",
                table: "WhyChoose",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SatisfiedPatientsCount",
                table: "WhyChoose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
