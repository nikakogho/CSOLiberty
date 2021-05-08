using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace CSOLiberty.Migrations
{
    public partial class AddedOrderTrigger : Migration
    {
        const string SQL_FOLDER = "SQL/";
        const string SQL_SUFFIX = ".sql";

        const string ORDER_DEPTH_CHECKER_FILE = "OrderDepthTrigger";
        const string ORDER_DEPTH_CHECKER = "ORDER_DEPTH_TRIGGER";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = $"{SQL_FOLDER}{ORDER_DEPTH_CHECKER_FILE}{SQL_SUFFIX}";

            migrationBuilder.Sql(File.ReadAllText(path));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP TRIGGET {ORDER_DEPTH_CHECKER}");
        }
    }
}
