using Microsoft.EntityFrameworkCore.Migrations;
using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Models.Security;

#nullable disable

namespace ProjectsMeasurements.DBContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Units", columns: new[] { nameof(Unit.UnitNameEn) , nameof(Unit.UnitNameAr) }, values: new object[,] { { "M", "م" }, { "CM", "سم" } });
            migrationBuilder.InsertData(table: "PermissionsTypes", columns: new[] { nameof(PermissionsType.PermissionTypeNameEn), nameof(PermissionsType.PermissionTypeNameAr) }, values: new object[,] { { "Menu", "قائمة" }, { "Form", "شاشة" }, { "Button", "زرار" } });
            migrationBuilder.InsertData(table: "Users", columns: new[] { nameof(User.UserName), nameof(User.UserPassword), nameof(User.IsAdmin), nameof(User.UserEmail), nameof(User.IsEmailVerified) }, values: new object[,] { { "admin", Utilities.Downloader.Download("123456", Utilities.ConstantValues.BaseURL).Item2, true, "fakemail@fakemail.com", false } });
            //Insert permessions
            //Insert PlantsCategories
            //Insert Plants
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Units", keyColumn: nameof(Unit.UnitNameEn), keyValue: "M");
            migrationBuilder.DeleteData(table: "Units", keyColumn: nameof(Unit.UnitNameEn), keyValue: "CM");
            migrationBuilder.DeleteData(table: "PermissionsTypes", keyColumn: nameof(PermissionsType.PermissionTypeNameEn), keyValue: "Menu");
            migrationBuilder.DeleteData(table: "PermissionsTypes", keyColumn: nameof(PermissionsType.PermissionTypeNameEn), keyValue: "Form");
            migrationBuilder.DeleteData(table: "PermissionsTypes", keyColumn: nameof(PermissionsType.PermissionTypeNameEn), keyValue: "Button");

        }
    }
}
