using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Course_Portal_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "CourseBook");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "CourseBook",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
