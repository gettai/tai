using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Data.Models;

namespace Tai.Core.Data
{
    public class TaiDbContext : DbContext
    {
        /// <summary>
        /// 应用
        /// </summary>
        public DbSet<Models.App> Apps { get; set; }
        /// <summary>
        /// 应用活动时间日志
        /// </summary>
        public DbSet<AppActiveTimeLog> AppActiveTimeLogs { get; set; }
        /// <summary>
        /// 应用闲置时间日志
        /// </summary>
        public DbSet<AppInactiveTimeLog> AppInactiveTimeLogs { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// 网页活动时间日志
        /// </summary>
        public DbSet<WebActiveTimeLog> WebActiveTimeLogs { get; set; }
        /// <summary>
        /// 网页闲置时间日志
        /// </summary>
        public DbSet<WebInactiveTimeLog> WebInactiveTimeLogs { get; set; }
        /// <summary>
        /// 站点
        /// </summary>
        public DbSet<Website> WebSites { get; set; }

        private readonly static string _dbPath = Path.Combine(FileHelper.GetAppRunDirectory(), "data", "tai.sqlite");
        public TaiDbContext() : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = _dbPath,
                ForeignKeys = true,
            }.ConnectionString
        }, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<TaiDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
