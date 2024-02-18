using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Star_Security_Services.Migrations
{
    public partial class Reintialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "Varchar(50)", nullable: false),
                    AdminEmail = table.Column<string>(type: "Varchar(max)", nullable: false),
                    AdminContact = table.Column<string>(type: "Varchar(50)", nullable: false),
                    AdminImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "Varchar(50)", nullable: false),
                    ClientProfession = table.Column<string>(type: "Varchar(50)", nullable: false),
                    ClientImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientReview = table.Column<string>(type: "Varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "Varchar(150)", nullable: false),
                    Message = table.Column<string>(type: "Varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Dept_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dept_name = table.Column<string>(type: "Varchar(70)", nullable: false),
                    Dept_location = table.Column<string>(type: "Varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Dept_id);
                });

            migrationBuilder.CreateTable(
                name: "empRoles",
                columns: table => new
                {
                    Role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_name = table.Column<string>(type: "Varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empRoles", x => x.Role_id);
                });

            migrationBuilder.CreateTable(
                name: "industries",
                columns: table => new
                {
                    IndustryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryName = table.Column<string>(type: "Varchar(50)", nullable: false),
                    IndustryImage = table.Column<string>(type: "Varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_industries", x => x.IndustryId);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "Varchar(150)", nullable: false),
                    ServiceImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "vacancies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostName = table.Column<string>(type: "Varchar(150)", nullable: false),
                    JobDescription = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Task1 = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Task2 = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Task3 = table.Column<string>(type: "Varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "Varchar(50)", nullable: false),
                    ProjectImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Project_enddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_clients_Client_id",
                        column: x => x.Client_id,
                        principalTable: "clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emp_name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Emp_email = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Emp_contact = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Emp_address = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Emp_qualification = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Emp_Grade = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Emp_age = table.Column<int>(type: "int", nullable: false),
                    EmpImage = table.Column<string>(type: "Varchar(max)", nullable: false),
                    Dep_id = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeCode);
                    table.ForeignKey(
                        name: "FK_employees_departments_Dep_id",
                        column: x => x.Dep_id,
                        principalTable: "departments",
                        principalColumn: "Dept_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employees_empRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "empRoles",
                        principalColumn: "Role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_Dep_id",
                table: "employees",
                column: "Dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_RoleID",
                table: "employees",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Client_id",
                table: "Projects",
                column: "Client_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "industries");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "vacancies");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "empRoles");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
