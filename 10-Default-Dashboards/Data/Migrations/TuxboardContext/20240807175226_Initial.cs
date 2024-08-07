using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DefaultDashboards.Data.Migrations.TuxboardContext
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Dashboard",
                schema: "dbo",
                columns: table => new
                {
                    DashboardId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    SelectedTab = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.DashboardId);
                });

            migrationBuilder.CreateTable(
                name: "DashboardRole",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutType",
                schema: "dbo",
                columns: table => new
                {
                    LayoutTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Layout = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutType", x => x.LayoutTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                schema: "dbo",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                schema: "dbo",
                columns: table => new
                {
                    WidgetId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    GroupName = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    Moveable = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    UseSettings = table.Column<bool>(type: "bit", nullable: false),
                    UseTemplate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.WidgetId);
                });

            migrationBuilder.CreateTable(
                name: "DashboardTab",
                schema: "dbo",
                columns: table => new
                {
                    TabId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    DashboardId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    TabTitle = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TabIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardTab", x => x.TabId);
                    table.ForeignKey(
                        name: "FK_DashboardTab_Dashboard",
                        column: x => x.DashboardId,
                        principalSchema: "dbo",
                        principalTable: "Dashboard",
                        principalColumn: "DashboardId");
                });

            migrationBuilder.CreateTable(
                name: "DashboardRoleClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardRoleClaim_DashboardRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "DashboardRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DashboardUserLogin",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_DashboardUserLogin_DashboardUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "DashboardUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DashboardUserRole",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_DashboardUserRole_DashboardRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "DashboardRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DashboardUserRole_DashboardUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "DashboardUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DashboardUserToken",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_DashboardUserToken_DashboardUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "DashboardUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim_DashboardUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "DashboardUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetDefault",
                schema: "dbo",
                columns: table => new
                {
                    WidgetDefaultId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    WidgetId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    SettingName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SettingTitle = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SettingType = table.Column<short>(type: "smallint", nullable: false),
                    DefaultValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    SettingIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetDefault", x => x.WidgetDefaultId);
                    table.ForeignKey(
                        name: "FK_WidgetDefault_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "dbo",
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetPlan",
                schema: "dbo",
                columns: table => new
                {
                    WidgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetPlan", x => new { x.WidgetId, x.PlanId });
                    table.ForeignKey(
                        name: "FK_WidgetPlan_Plan",
                        column: x => x.PlanId,
                        principalSchema: "dbo",
                        principalTable: "Plan",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_WidgetPlan_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "dbo",
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "Layout",
                schema: "dbo",
                columns: table => new
                {
                    LayoutId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    TabId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: true),
                    LayoutIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layout", x => x.LayoutId);
                    table.ForeignKey(
                        name: "FK_DashboardLayout_DashboardTab",
                        column: x => x.TabId,
                        principalSchema: "dbo",
                        principalTable: "DashboardTab",
                        principalColumn: "TabId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetDefaultOption",
                schema: "dbo",
                columns: table => new
                {
                    WidgetOptionId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    WidgetDefaultId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    SettingLabel = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    SettingValue = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    SettingIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetSettingOption", x => x.WidgetOptionId);
                    table.ForeignKey(
                        name: "FK_WidgetDefaultOption_WidgetDefault",
                        column: x => x.WidgetDefaultId,
                        principalSchema: "dbo",
                        principalTable: "WidgetDefault",
                        principalColumn: "WidgetDefaultId");
                });

            migrationBuilder.CreateTable(
                name: "DashboardDefault",
                schema: "dbo",
                columns: table => new
                {
                    DefaultId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    LayoutId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardDefault", x => x.DefaultId);
                    table.ForeignKey(
                        name: "FK_DashboardDefault_Layout",
                        column: x => x.LayoutId,
                        principalSchema: "dbo",
                        principalTable: "Layout",
                        principalColumn: "LayoutId");
                    table.ForeignKey(
                        name: "FK_DashboardDefault_Plan",
                        column: x => x.PlanId,
                        principalSchema: "dbo",
                        principalTable: "Plan",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "LayoutRow",
                schema: "dbo",
                columns: table => new
                {
                    LayoutRowId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    LayoutId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: true),
                    LayoutTypeId = table.Column<int>(type: "int", nullable: false),
                    RowIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutRow", x => x.LayoutRowId);
                    table.ForeignKey(
                        name: "FK_LayoutRow_LayoutType",
                        column: x => x.LayoutTypeId,
                        principalSchema: "dbo",
                        principalTable: "LayoutType",
                        principalColumn: "LayoutTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LayoutRow_Layout_LayoutId",
                        column: x => x.LayoutId,
                        principalSchema: "dbo",
                        principalTable: "Layout",
                        principalColumn: "LayoutId");
                });

            migrationBuilder.CreateTable(
                name: "RoleDefaultDashboards",
                schema: "dbo",
                columns: table => new
                {
                    DefaultDashboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDefaultDashboards", x => new { x.DefaultDashboardId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleDefaultDashboards_DashboardDefault_DefaultDashboardId",
                        column: x => x.DefaultDashboardId,
                        principalSchema: "dbo",
                        principalTable: "DashboardDefault",
                        principalColumn: "DefaultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDefaultDashboards_DashboardRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "DashboardRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DashboardDefaultWidget",
                schema: "dbo",
                columns: table => new
                {
                    DefaultWidgetId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    DashboardDefaultId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    LayoutRowId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    WidgetId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    ColumnIndex = table.Column<int>(type: "int", nullable: false),
                    WidgetIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardDefaultWidget", x => x.DefaultWidgetId);
                    table.ForeignKey(
                        name: "FK_DashboardDefaultWidget_DashboardDefault",
                        column: x => x.DashboardDefaultId,
                        principalSchema: "dbo",
                        principalTable: "DashboardDefault",
                        principalColumn: "DefaultId");
                    table.ForeignKey(
                        name: "FK_DashboardDefaultWidget_LayoutRow",
                        column: x => x.LayoutRowId,
                        principalSchema: "dbo",
                        principalTable: "LayoutRow",
                        principalColumn: "LayoutRowId");
                    table.ForeignKey(
                        name: "FK_DashboardDefaultWidget_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "dbo",
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetPlacement",
                schema: "dbo",
                columns: table => new
                {
                    WidgetPlacementId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    LayoutRowId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    WidgetId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    ColumnIndex = table.Column<int>(type: "int", nullable: false),
                    WidgetIndex = table.Column<int>(type: "int", nullable: false),
                    Collapsed = table.Column<bool>(type: "bit", nullable: false),
                    UseSettings = table.Column<bool>(type: "bit", nullable: false),
                    UseTemplate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetPlacement", x => x.WidgetPlacementId);
                    table.ForeignKey(
                        name: "FK_WidgetPlacement_LayoutRow1",
                        column: x => x.LayoutRowId,
                        principalSchema: "dbo",
                        principalTable: "LayoutRow",
                        principalColumn: "LayoutRowId");
                    table.ForeignKey(
                        name: "FK_WidgetPlacement_Widget1",
                        column: x => x.WidgetId,
                        principalSchema: "dbo",
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetSetting",
                schema: "dbo",
                columns: table => new
                {
                    WidgetSettingId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    WidgetPlacementId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    WidgetDefaultId = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 36, nullable: false),
                    Value = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetSetting", x => x.WidgetSettingId);
                    table.ForeignKey(
                        name: "FK_WidgetSetting_WidgetDefault",
                        column: x => x.WidgetDefaultId,
                        principalSchema: "dbo",
                        principalTable: "WidgetDefault",
                        principalColumn: "WidgetDefaultId");
                    table.ForeignKey(
                        name: "FK_WidgetSetting_WidgetPlacement",
                        column: x => x.WidgetPlacementId,
                        principalSchema: "dbo",
                        principalTable: "WidgetPlacement",
                        principalColumn: "WidgetPlacementId");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Layout",
                columns: new[] { "LayoutId", "LayoutIndex", "TabId" },
                values: new object[] { new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), 1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LayoutType",
                columns: new[] { "LayoutTypeId", "Layout", "Title" },
                values: new object[,]
                {
                    { 1, "col-4/col-4/col-4", "Three Columns, Equal" },
                    { 2, "col-3/col-6/col-3", "Three Columns, 50% Middle" },
                    { 3, "col-3/col-3/col-3/col-3", "Four Columns, 25%" },
                    { 4, "col-6/col-6", "Two Columns, 50%" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Widget",
                columns: new[] { "WidgetId", "CanDelete", "Description", "GroupName", "ImageUrl", "Moveable", "Name", "Permission", "Title", "UseSettings", "UseTemplate" },
                values: new object[,]
                {
                    { new Guid("1885170c-7c48-4557-abc7-bc06d3fc51ee"), false, "Display General Information", "", "", false, "generalinfo", 0, "General Info", false, false },
                    { new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396"), true, "A Simple Hello World Widget", "", "", true, "helloworld", 0, "Hello World", true, true },
                    { new Guid("ee84443b-7ee7-4754-bb3c-313cc0da6039"), true, "Demonstration of data table", "", "", true, "table", 0, "Sample Table", true, true }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DashboardDefault",
                columns: new[] { "DefaultId", "LayoutId", "PlanId" },
                values: new object[] { new Guid("0d96a18e-90b8-4a9f-9df1-126653d68fe6"), new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LayoutRow",
                columns: new[] { "LayoutRowId", "LayoutId", "LayoutTypeId", "RowIndex" },
                values: new object[] { new Guid("d58afcd2-2007-4fd0-87a9-93c85c667f3f"), new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), 4, 0 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "WidgetDefault",
                columns: new[] { "WidgetDefaultId", "DefaultValue", "SettingIndex", "SettingName", "SettingTitle", "SettingType", "WidgetId" },
                values: new object[,]
                {
                    { new Guid("046f4aa8-5e45-4c86-b2f8-cbf3e42647e7"), "Sample Table", 1, "widgettitle", "Title", (short)0, new Guid("ee84443b-7ee7-4754-bb3c-313cc0da6039") },
                    { new Guid("5c85537a-1319-48ed-a475-83d3dc3e7a8d"), "Projects", 1, "widgettitle", "Title", (short)0, new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "DashboardDefaultWidget",
                columns: new[] { "DefaultWidgetId", "ColumnIndex", "DashboardDefaultId", "LayoutRowId", "WidgetId", "WidgetIndex" },
                values: new object[] { new Guid("d21e94cf-86a9-4058-bb72-f269728ac8ad"), 0, new Guid("0d96a18e-90b8-4a9f-9df1-126653d68fe6"), new Guid("d58afcd2-2007-4fd0-87a9-93c85c667f3f"), new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396"), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefault_LayoutId",
                schema: "dbo",
                table: "DashboardDefault",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefault_PlanId",
                schema: "dbo",
                table: "DashboardDefault",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_DashboardDefaultId",
                schema: "dbo",
                table: "DashboardDefaultWidget",
                column: "DashboardDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_LayoutRowId",
                schema: "dbo",
                table: "DashboardDefaultWidget",
                column: "LayoutRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_WidgetId",
                schema: "dbo",
                table: "DashboardDefaultWidget",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardRoleClaim_RoleId",
                schema: "dbo",
                table: "DashboardRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardTab_DashboardId",
                schema: "dbo",
                table: "DashboardTab",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardUserLogin_UserId",
                schema: "dbo",
                table: "DashboardUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardUserRole_RoleId",
                schema: "dbo",
                table: "DashboardUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim_UserId",
                schema: "dbo",
                table: "IdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Layout_TabId",
                schema: "dbo",
                table: "Layout",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutRow_LayoutId",
                schema: "dbo",
                table: "LayoutRow",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutRow_LayoutTypeId",
                schema: "dbo",
                table: "LayoutRow",
                column: "LayoutTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDefaultDashboards_RoleId",
                schema: "dbo",
                table: "RoleDefaultDashboards",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetDefault_WidgetId",
                schema: "dbo",
                table: "WidgetDefault",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetDefaultOption_WidgetDefaultId",
                schema: "dbo",
                table: "WidgetDefaultOption",
                column: "WidgetDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlacement_LayoutRowId",
                schema: "dbo",
                table: "WidgetPlacement",
                column: "LayoutRowId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlacement_WidgetId",
                schema: "dbo",
                table: "WidgetPlacement",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlan_PlanId",
                schema: "dbo",
                table: "WidgetPlan",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetSetting_WidgetDefaultId",
                schema: "dbo",
                table: "WidgetSetting",
                column: "WidgetDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetSetting_WidgetPlacementId",
                schema: "dbo",
                table: "WidgetSetting",
                column: "WidgetPlacementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardDefaultWidget",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardRoleClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardUserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardUserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardUserToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleDefaultDashboards",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WidgetDefaultOption",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WidgetPlan",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WidgetSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardDefault",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WidgetDefault",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WidgetPlacement",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Plan",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LayoutRow",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Widget",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LayoutType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Layout",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DashboardTab",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Dashboard",
                schema: "dbo");
        }
    }
}
