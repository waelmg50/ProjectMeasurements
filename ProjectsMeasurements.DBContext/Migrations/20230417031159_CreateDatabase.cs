using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsMeasurements.DBContext.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContractorNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contractors_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GroupDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Owners_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTypeNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PermissionTypeNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionsTypes_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantsCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    FullCode = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantsCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlantsCategories_PlantsCategories_ParentID",
                        column: x => x.ParentID,
                        principalTable: "PlantsCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantsCategories_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    FullCode = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Projects_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Units_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersGroups_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PermissionNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PermissionTypeID = table.Column<int>(type: "int", nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    FullCode = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionsTypes_PermissionTypeID",
                        column: x => x.PermissionTypeID,
                        principalTable: "PermissionsTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantNo = table.Column<int>(type: "int", nullable: false),
                    PlantCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PlantNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlantNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlantDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    PlantCategoryID = table.Column<int>(type: "int", nullable: false),
                    PlantFullCode = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plants_PlantsCategories_PlantCategoryID",
                        column: x => x.PlantCategoryID,
                        principalTable: "PlantsCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plants_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementsHeaders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementCode = table.Column<int>(type: "int", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    ContractorID = table.Column<int>(type: "int", nullable: false),
                    MeasurementTotalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementsHeaders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeasurementsHeaders_Contractors_ContractorID",
                        column: x => x.ContractorID,
                        principalTable: "Contractors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsHeaders_Owners_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsHeaders_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsHeaders_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupsPermissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsPermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsPermissions_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PlantsDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantID = table.Column<int>(type: "int", nullable: false),
                    PlantHeight = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PlantHeihgtUnitID = table.Column<int>(type: "int", nullable: false),
                    PlantTrunkWidth = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PlantDefaultPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantsDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlantsDetails_Plants_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantsDetails_Units_PlantHeihgtUnitID",
                        column: x => x.PlantHeihgtUnitID,
                        principalTable: "Units",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantsDetails_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementsDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementHeaderID = table.Column<int>(type: "int", nullable: false),
                    PlantDetailID = table.Column<int>(type: "int", nullable: false),
                    PlantQuantity = table.Column<int>(type: "int", nullable: false),
                    PlantUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlantTotalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    LastUserID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementsDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeasurementsDetails_MeasurementsHeaders_MeasurementHeaderID",
                        column: x => x.MeasurementHeaderID,
                        principalTable: "MeasurementsHeaders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsDetails_PlantsDetails_PlantDetailID",
                        column: x => x.PlantDetailID,
                        principalTable: "PlantsDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementsDetails_Users_LastUserID",
                        column: x => x.LastUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_LastUserID",
                table: "Contractors",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LastUserID",
                table: "Groups",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_GroupID",
                table: "GroupsPermissions",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_LastUserID",
                table: "GroupsPermissions",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPermissions_PermissionID",
                table: "GroupsPermissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsDetails_LastUserID",
                table: "MeasurementsDetails",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsDetails_MeasurementHeaderID",
                table: "MeasurementsDetails",
                column: "MeasurementHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsDetails_PlantDetailID",
                table: "MeasurementsDetails",
                column: "PlantDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHeaders_ContractorID",
                table: "MeasurementsHeaders",
                column: "ContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHeaders_LastUserID",
                table: "MeasurementsHeaders",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHeaders_OwnerID",
                table: "MeasurementsHeaders",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHeaders_ProjectID",
                table: "MeasurementsHeaders",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_LastUserID",
                table: "Owners",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_LastUserID",
                table: "Permissions",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentID",
                table: "Permissions",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeID",
                table: "Permissions",
                column: "PermissionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsTypes_LastUserID",
                table: "PermissionsTypes",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_LastUserID",
                table: "Plants",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantCategoryID",
                table: "Plants",
                column: "PlantCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsCategories_LastUserID",
                table: "PlantsCategories",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsCategories_ParentID",
                table: "PlantsCategories",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsDetails_LastUserID",
                table: "PlantsDetails",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsDetails_PlantHeihgtUnitID",
                table: "PlantsDetails",
                column: "PlantHeihgtUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsDetails_PlantID",
                table: "PlantsDetails",
                column: "PlantID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LastUserID",
                table: "Projects",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentID",
                table: "Projects",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_LastUserID",
                table: "Units",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastUserID",
                table: "Users",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserEmail",
                table: "Users",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_GroupID",
                table: "UsersGroups",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_LastUserID",
                table: "UsersGroups",
                column: "LastUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroups_UserID",
                table: "UsersGroups",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsPermissions");

            migrationBuilder.DropTable(
                name: "MeasurementsDetails");

            migrationBuilder.DropTable(
                name: "UsersGroups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "MeasurementsHeaders");

            migrationBuilder.DropTable(
                name: "PlantsDetails");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "PermissionsTypes");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "PlantsCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
