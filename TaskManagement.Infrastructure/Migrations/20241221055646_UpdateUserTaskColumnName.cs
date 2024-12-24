using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTaskColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_user_tasks_users_assigned_user_id",
                table: "user_tasks");

            migrationBuilder.RenameColumn(
                name: "assigned_user_id",
                table: "user_tasks",
                newName: "task_owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_tasks_assigned_user_id",
                table: "user_tasks",
                newName: "IX_user_tasks_task_owner_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_user_tasks_users_task_owner_id",
                table: "user_tasks",
                column: "task_owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_user_tasks_users_task_owner_id",
                table: "user_tasks");

            migrationBuilder.RenameColumn(
                name: "task_owner_id",
                table: "user_tasks",
                newName: "assigned_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_tasks_task_owner_id",
                table: "user_tasks",
                newName: "IX_user_tasks_assigned_user_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_user_tasks_users_assigned_user_id",
                table: "user_tasks",
                column: "assigned_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
