using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataAccess.Migrations
{
    public partial class DbNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adm_api_user",
                columns: table => new
                {
                    apiuserid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    username = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    project = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    applicant = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    expires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adm_api_user", x => x.apiuserid);
                });

            migrationBuilder.CreateTable(
                name: "adm_goal",
                columns: table => new
                {
                    goalid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    register_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_tasks = table.Column<int>(type: "int", nullable: false),
                    percentege = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adm_goal", x => x.goalid);
                });

            migrationBuilder.CreateTable(
                name: "adm_state",
                columns: table => new
                {
                    stateid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adm_state", x => x.stateid);
                });

            migrationBuilder.CreateTable(
                name: "adm_task",
                columns: table => new
                {
                    taskid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    register_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_important = table.Column<bool>(type: "bit", nullable: false),
                    goalid = table.Column<int>(type: "int", nullable: false),
                    stateid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adm_task", x => x.taskid);
                    table.ForeignKey(
                        name: "fk_goa_sta",
                        column: x => x.stateid,
                        principalTable: "adm_state",
                        principalColumn: "stateid");
                    table.ForeignKey(
                        name: "fk_goa_tas",
                        column: x => x.goalid,
                        principalTable: "adm_goal",
                        principalColumn: "goalid");
                });

            migrationBuilder.InsertData(
                table: "adm_api_user",
                columns: new[] { "apiuserid", "applicant", "expires", "password", "project", "role", "username" },
                values: new object[] { 1, "Neitek", new DateTime(2100, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "lTLCFRbrKDUnl2moiKUHVg==", "Neitek", "Admin", "EGQV8z6yuZVrTlZrqBCUlQ==" });

            migrationBuilder.InsertData(
                table: "adm_state",
                columns: new[] { "stateid", "name" },
                values: new object[] { 1, "Abierta" });

            migrationBuilder.InsertData(
                table: "adm_state",
                columns: new[] { "stateid", "name" },
                values: new object[] { 2, "Completada" });

            migrationBuilder.CreateIndex(
                name: "pk_apu_idx",
                table: "adm_api_user",
                column: "apiuserid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_goa_idx",
                table: "adm_goal",
                column: "goalid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_sta_idx",
                table: "adm_state",
                column: "stateid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_goa_sta_idx",
                table: "adm_task",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "fk_goa_tas_idx",
                table: "adm_task",
                column: "goalid");

            migrationBuilder.CreateIndex(
                name: "pk_tas_idx",
                table: "adm_task",
                column: "taskid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adm_api_user");

            migrationBuilder.DropTable(
                name: "adm_task");

            migrationBuilder.DropTable(
                name: "adm_state");

            migrationBuilder.DropTable(
                name: "adm_goal");
        }
    }
}
