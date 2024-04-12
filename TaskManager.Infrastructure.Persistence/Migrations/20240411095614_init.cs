using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllowedEditProject",
                table: "roles",
                newName: "RoleEdit");

            migrationBuilder.RenameColumn(
                name: "AllowedDeleteProject",
                table: "roles",
                newName: "RoleDelete");

            migrationBuilder.RenameColumn(
                name: "AllowedAddUsersProject",
                table: "roles",
                newName: "RoleAdd");

            migrationBuilder.AddColumn<Guid>(
                name: "ExecutorId",
                table: "tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BoardAdd",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BoardAddTasks",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BoardDelete",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BoardEdit",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectAddUsers",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectDelete",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectDeleteUsers",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProjectEdit",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_ExecutorId",
                table: "tasks",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_users_ExecutorId",
                table: "tasks",
                column: "ExecutorId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_users_ExecutorId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_ExecutorId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "BoardAdd",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "BoardAddTasks",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "BoardDelete",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "BoardEdit",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ProjectAddUsers",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ProjectDelete",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ProjectDeleteUsers",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "ProjectEdit",
                table: "roles");

            migrationBuilder.RenameColumn(
                name: "RoleEdit",
                table: "roles",
                newName: "AllowedEditProject");

            migrationBuilder.RenameColumn(
                name: "RoleDelete",
                table: "roles",
                newName: "AllowedDeleteProject");

            migrationBuilder.RenameColumn(
                name: "RoleAdd",
                table: "roles",
                newName: "AllowedAddUsersProject");
        }
    }
}
