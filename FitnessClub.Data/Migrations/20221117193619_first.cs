 using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "Clients",
                type: "datetime2",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AddColumn<int>(
                name: "FitnessClubId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Membership",
                table: "Clients",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfMembershipId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Expired = table.Column<DateTime>(type: "date", nullable: false),
                    Money = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountSession",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionToken = table.Column<string>(type: "nvarchar(384)", maxLength: 384, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    TimeClosed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSession", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_AccountSession_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FitnessClubId",
                table: "Clients",
                column: "FitnessClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeOfMembershipId",
                table: "Clients",
                column: "TypeOfMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSession_AccountId",
                table: "AccountSession",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_EmployeeTypes_TypeOfMembershipId",
                table: "Clients",
                column: "TypeOfMembershipId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_FitnessClubs_FitnessClubId",
                table: "Clients",
                column: "FitnessClubId",
                principalTable: "FitnessClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_EmployeeTypes_TypeOfMembershipId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_FitnessClubs_FitnessClubId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "AccountSession");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "FitnessClubs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Clients_FitnessClubId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TypeOfMembershipId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FitnessClubId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Membership",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TypeOfMembershipId",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Clients",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "BirthDay",
                table: "Clients",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 255);
        }
    }
}
