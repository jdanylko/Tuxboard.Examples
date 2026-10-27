using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncWidgets.Data.Migrations.TuxboardContext
{
    /// <inheritdoc />
    public partial class TuxboardCore10014 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetSettingId",
                schema: "dbo",
                table: "WidgetSetting",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetPlacementId",
                schema: "dbo",
                table: "WidgetPlacement",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetOptionId",
                schema: "dbo",
                table: "WidgetDefaultOption",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetDefaultId",
                schema: "dbo",
                table: "WidgetDefault",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetId",
                schema: "dbo",
                table: "Widget",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutRowId",
                schema: "dbo",
                table: "LayoutRow",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutId",
                schema: "dbo",
                table: "Layout",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "TabId",
                schema: "dbo",
                table: "DashboardTab",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultWidgetId",
                schema: "dbo",
                table: "DashboardDefaultWidget",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultId",
                schema: "dbo",
                table: "DashboardDefault",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "DashboardId",
                schema: "dbo",
                table: "Dashboard",
                type: "uniqueidentifier",
                unicode: false,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldDefaultValueSql: "(newid())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetSettingId",
                schema: "dbo",
                table: "WidgetSetting",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetPlacementId",
                schema: "dbo",
                table: "WidgetPlacement",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetOptionId",
                schema: "dbo",
                table: "WidgetDefaultOption",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetDefaultId",
                schema: "dbo",
                table: "WidgetDefault",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "WidgetId",
                schema: "dbo",
                table: "Widget",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutRowId",
                schema: "dbo",
                table: "LayoutRow",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutId",
                schema: "dbo",
                table: "Layout",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "TabId",
                schema: "dbo",
                table: "DashboardTab",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultWidgetId",
                schema: "dbo",
                table: "DashboardDefaultWidget",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultId",
                schema: "dbo",
                table: "DashboardDefault",
                type: "uniqueidentifier",
                unicode: false,
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "DashboardId",
                schema: "dbo",
                table: "Dashboard",
                type: "uniqueidentifier",
                unicode: false,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldUnicode: false);
        }
    }
}
