using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskGFL.Migrations
{
    public partial class defaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "FolderName", "ParentFolderId" },
                values: new object[] { 1, "Creating Digital Images", null });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "FolderName", "ParentFolderId" },
                values: new object[] { 2, "Resources", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "FolderName", "ParentFolderId" },
                values: new object[] { 3, "Evidence", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "FolderName", "ParentFolderId" },
                values: new object[] { 4, "Graphic Products", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "FolderId", "FolderName", "ParentFolderId" },
                values: new object[,]
                {
                    { 5, "Primary Sources", 2 },
                    { 6, "Secondary Sources", 2 },
                    { 7, "Process", 4 },
                    { 8, "Final Product", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "FolderId",
                keyValue: 1);
        }
    }
}
