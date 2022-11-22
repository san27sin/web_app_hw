using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_EmployeeTypes_TypeOfMembershipId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes");

            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                newName: "TypeOfMembership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOfMembership",
                table: "TypeOfMembership",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_TypeOfMembership_TypeOfMembershipId",
                table: "Clients",
                column: "TypeOfMembershipId",
                principalTable: "TypeOfMembership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_TypeOfMembership_TypeOfMembershipId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOfMembership",
                table: "TypeOfMembership");

            migrationBuilder.RenameTable(
                name: "TypeOfMembership",
                newName: "EmployeeTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_EmployeeTypes_TypeOfMembershipId",
                table: "Clients",
                column: "TypeOfMembershipId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
