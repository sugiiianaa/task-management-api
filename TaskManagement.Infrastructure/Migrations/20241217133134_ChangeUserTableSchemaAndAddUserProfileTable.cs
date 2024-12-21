using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserTableSchemaAndAddUserProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_username",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "name");

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.id);
                    table.ForeignKey(
                        name: "f_k_users__user_profile_profile_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_profile");

            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "username");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }
    }
}
