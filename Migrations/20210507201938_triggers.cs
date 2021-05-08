﻿using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSOLiberty.Migrations
{
    public partial class triggers : Migration
    {
        const string SQL_FOLDER = "SQL/";

        const string CYCLE_CHECKER_FILE = "CycleChecker";
        const string SELLER_BOSS_TRIGGER_FILE = "SellerBossTrigger";

        const string SQL_SUFFIX = ".sql";

        const string SELLER_BOSS_TRIGGER = "SELLER_BOSS_TRIGGER";
        const string CYCLE_FUNC_NAME = "CYCLE_CHECKER";

        IEnumerable<string> GetSQLFiles()
        {
            return new string[]
            {
                $"{SQL_FOLDER}{CYCLE_CHECKER_FILE}{SQL_SUFFIX}",
                $"{SQL_FOLDER}{SELLER_BOSS_TRIGGER_FILE}{SQL_SUFFIX}"
            };
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddSellerBossTriggers(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RemoveSellerBossTriggers(migrationBuilder);
        }

        private void AddSellerBossTriggers(MigrationBuilder builder)
        {
            foreach (string path in GetSQLFiles()) builder.Sql(File.ReadAllText(path));
        }

        private void RemoveSellerBossTriggers(MigrationBuilder builder)
        {
            builder.Sql($"DROP TRIGGER {SELLER_BOSS_TRIGGER}");
            builder.Sql($"DROP FUNC {CYCLE_FUNC_NAME}");
        }
    }
}
