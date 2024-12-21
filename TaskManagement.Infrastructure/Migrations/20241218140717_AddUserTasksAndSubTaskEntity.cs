using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTasksAndSubTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_users__user_profile_profile_id",
                table: "user_profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profile",
                table: "user_profile");

            migrationBuilder.RenameTable(
                name: "user_profile",
                newName: "user_profiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profiles",
                table: "user_profiles",
                column: "id");

            migrationBuilder.CreateTable(
                name: "user_tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    expected_finish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    assigned_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tasks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_user_tasks_users_assigned_user_id",
                        column: x => x.assigned_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_tasks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_sub_tasks__user_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "user_tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sub_tasks_task_id",
                table: "sub_tasks",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_tasks_assigned_user_id",
                table: "user_tasks",
                column: "assigned_user_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_user_profiles_users_id",
                table: "user_profiles",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_user_profiles_users_id",
                table: "user_profiles");

            migrationBuilder.DropTable(
                name: "sub_tasks");

            migrationBuilder.DropTable(
                name: "user_tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profiles",
                table: "user_profiles");

            migrationBuilder.RenameTable(
                name: "user_profiles",
                newName: "user_profile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profile",
                table: "user_profile",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "f_k_users__user_profile_profile_id",
                table: "user_profile",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
