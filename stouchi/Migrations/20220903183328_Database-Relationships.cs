using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stouchi.Migrations
{
    public partial class DatabaseRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Users_UserId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expences_Users_UserId",
                table: "Expences");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Buckets_BucketId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_BucketId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Expences_UserId",
                table: "Expences");

            migrationBuilder.DropIndex(
                name: "IX_Buckets_UserId",
                table: "Buckets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Incomes_BucketId",
                table: "Incomes",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_Expences_UserId",
                table: "Expences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Buckets_UserId",
                table: "Buckets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Users_UserId",
                table: "Buckets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_Users_UserId",
                table: "Expences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Buckets_BucketId",
                table: "Incomes",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "BucketId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
