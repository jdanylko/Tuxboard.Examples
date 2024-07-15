using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _09UserDashboard.Data.Migrations.TuxboardContext
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboard",
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
                name: "LayoutType",
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
                        principalTable: "Dashboard",
                        principalColumn: "DashboardId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetDefault",
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
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetPlan",
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
                        principalTable: "Plan",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_WidgetPlan_Widget",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "Layout",
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
                        principalTable: "DashboardTab",
                        principalColumn: "TabId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetDefaultOption",
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
                        principalTable: "WidgetDefault",
                        principalColumn: "WidgetDefaultId");
                });

            migrationBuilder.CreateTable(
                name: "DashboardDefault",
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
                        principalTable: "Layout",
                        principalColumn: "LayoutId");
                    table.ForeignKey(
                        name: "FK_DashboardDefault_Plan",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "LayoutRow",
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
                        principalTable: "LayoutType",
                        principalColumn: "LayoutTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LayoutRow_Layout_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layout",
                        principalColumn: "LayoutId");
                });

            migrationBuilder.CreateTable(
                name: "DashboardDefaultWidget",
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
                        principalTable: "DashboardDefault",
                        principalColumn: "DefaultId");
                    table.ForeignKey(
                        name: "FK_DashboardDefaultWidget_LayoutRow",
                        column: x => x.LayoutRowId,
                        principalTable: "LayoutRow",
                        principalColumn: "LayoutRowId");
                    table.ForeignKey(
                        name: "FK_DashboardDefaultWidget_Widget",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetPlacement",
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
                        principalTable: "LayoutRow",
                        principalColumn: "LayoutRowId");
                    table.ForeignKey(
                        name: "FK_WidgetPlacement_Widget1",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "WidgetId");
                });

            migrationBuilder.CreateTable(
                name: "WidgetSetting",
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
                        principalTable: "WidgetDefault",
                        principalColumn: "WidgetDefaultId");
                    table.ForeignKey(
                        name: "FK_WidgetSetting_WidgetPlacement",
                        column: x => x.WidgetPlacementId,
                        principalTable: "WidgetPlacement",
                        principalColumn: "WidgetPlacementId");
                });

            migrationBuilder.InsertData(
                table: "Layout",
                columns: new[] { "LayoutId", "LayoutIndex", "TabId" },
                values: new object[] { new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), 1, null });

            migrationBuilder.InsertData(
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
                table: "Widget",
                columns: new[] { "WidgetId", "CanDelete", "Description", "GroupName", "ImageUrl", "Moveable", "Name", "Permission", "Title", "UseSettings", "UseTemplate" },
                values: new object[,]
                {
                    { new Guid("1885170c-7c48-4557-abc7-bc06d3fc51ee"), false, "Display General Information", "", "", false, "generalinfo", 0, "General Info", false, false },
                    { new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396"), true, "A Simple Hello World Widget", "", "", true, "helloworld", 0, "Hello World", true, true },
                    { new Guid("ee84443b-7ee7-4754-bb3c-313cc0da6039"), true, "Demonstration of data table", "", "", true, "table", 0, "Sample Table", true, true }
                });

            migrationBuilder.InsertData(
                table: "DashboardDefault",
                columns: new[] { "DefaultId", "LayoutId", "PlanId" },
                values: new object[] { new Guid("0d96a18e-90b8-4a9f-9df1-126653d68fe6"), new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), null });

            migrationBuilder.InsertData(
                table: "LayoutRow",
                columns: new[] { "LayoutRowId", "LayoutId", "LayoutTypeId", "RowIndex" },
                values: new object[] { new Guid("d58afcd2-2007-4fd0-87a9-93c85c667f3f"), new Guid("5267da05-afe4-4753-9cee-d5d32c2b068e"), 4, 0 });

            migrationBuilder.InsertData(
                table: "WidgetDefault",
                columns: new[] { "WidgetDefaultId", "DefaultValue", "SettingIndex", "SettingName", "SettingTitle", "SettingType", "WidgetId" },
                values: new object[,]
                {
                    { new Guid("046f4aa8-5e45-4c86-b2f8-cbf3e42647e7"), "Sample Table", 1, "widgettitle", "Title", (short)0, new Guid("ee84443b-7ee7-4754-bb3c-313cc0da6039") },
                    { new Guid("5c85537a-1319-48ed-a475-83d3dc3e7a8d"), "Projects", 1, "widgettitle", "Title", (short)0, new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396") }
                });

            migrationBuilder.InsertData(
                table: "DashboardDefaultWidget",
                columns: new[] { "DefaultWidgetId", "ColumnIndex", "DashboardDefaultId", "LayoutRowId", "WidgetId", "WidgetIndex" },
                values: new object[] { new Guid("d21e94cf-86a9-4058-bb72-f269728ac8ad"), 0, new Guid("0d96a18e-90b8-4a9f-9df1-126653d68fe6"), new Guid("d58afcd2-2007-4fd0-87a9-93c85c667f3f"), new Guid("c9a9db53-14ca-4551-87e7-f9656f39a396"), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefault_LayoutId",
                table: "DashboardDefault",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefault_PlanId",
                table: "DashboardDefault",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_DashboardDefaultId",
                table: "DashboardDefaultWidget",
                column: "DashboardDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_LayoutRowId",
                table: "DashboardDefaultWidget",
                column: "LayoutRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDefaultWidget_WidgetId",
                table: "DashboardDefaultWidget",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardTab_DashboardId",
                table: "DashboardTab",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Layout_TabId",
                table: "Layout",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutRow_LayoutId",
                table: "LayoutRow",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_LayoutRow_LayoutTypeId",
                table: "LayoutRow",
                column: "LayoutTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetDefault_WidgetId",
                table: "WidgetDefault",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetDefaultOption_WidgetDefaultId",
                table: "WidgetDefaultOption",
                column: "WidgetDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlacement_LayoutRowId",
                table: "WidgetPlacement",
                column: "LayoutRowId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlacement_WidgetId",
                table: "WidgetPlacement",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetPlan_PlanId",
                table: "WidgetPlan",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetSetting_WidgetDefaultId",
                table: "WidgetSetting",
                column: "WidgetDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetSetting_WidgetPlacementId",
                table: "WidgetSetting",
                column: "WidgetPlacementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardDefaultWidget");

            migrationBuilder.DropTable(
                name: "WidgetDefaultOption");

            migrationBuilder.DropTable(
                name: "WidgetPlan");

            migrationBuilder.DropTable(
                name: "WidgetSetting");

            migrationBuilder.DropTable(
                name: "DashboardDefault");

            migrationBuilder.DropTable(
                name: "WidgetDefault");

            migrationBuilder.DropTable(
                name: "WidgetPlacement");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "LayoutRow");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.DropTable(
                name: "LayoutType");

            migrationBuilder.DropTable(
                name: "Layout");

            migrationBuilder.DropTable(
                name: "DashboardTab");

            migrationBuilder.DropTable(
                name: "Dashboard");
        }
    }
}
