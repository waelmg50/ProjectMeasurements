using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsMeasurements.DBContext.Migrations
{
    /// <inheritdoc />
    public partial class MeasurementsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeasurementTypeID",
                table: "MeasurementsHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MeasurementsTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementsTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeasurementsTypes_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHeaders_MeasurementTypeID",
                table: "MeasurementsHeaders",
                column: "MeasurementTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsTypes_LastUserID",
                table: "MeasurementsTypes",
                column: "LastUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsHeaders_MeasurementsTypes_MeasurementTypeID",
                table: "MeasurementsHeaders",
                column: "MeasurementTypeID",
                principalTable: "MeasurementsTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsHeaders_MeasurementsTypes_MeasurementTypeID",
                table: "MeasurementsHeaders");

            migrationBuilder.DropTable(
                name: "MeasurementsTypes");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementsHeaders_MeasurementTypeID",
                table: "MeasurementsHeaders");

            migrationBuilder.DropColumn(
                name: "MeasurementTypeID",
                table: "MeasurementsHeaders");
        }
    }
}
