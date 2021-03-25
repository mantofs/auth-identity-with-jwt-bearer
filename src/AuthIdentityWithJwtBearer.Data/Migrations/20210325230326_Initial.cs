using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthIdentityWithJwtBearer.Data.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "TEXT", nullable: false),
            Username = table.Column<string>(type: "varchar(30)", nullable: false),
            Password = table.Column<string>(type: "varchar(30)", nullable: false),
            Role = table.Column<string>(type: "varchar(30)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
          });

      migrationBuilder.InsertData("Users",
      columns: new string[] { "Id", "Username", "Password", "Role" },
      values: new object[] { Guid.NewGuid(), "bob", "bob", "manager" });

      migrationBuilder.InsertData("Users",
      columns: new string[] { "Id", "Username", "Password", "Role" },
      values: new object[] { Guid.NewGuid(), "bil", "bil", "employee" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
