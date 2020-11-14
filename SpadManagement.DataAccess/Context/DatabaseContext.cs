using SpadManagement.Model.Entities;
using SpadManagement.Model.Entities.Base.Authorization;
using SpadManagement.Model.Entities.Base.Log;
using SpadManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadManagement.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString)
        {
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        #region Singleton
        private static DatabaseContext _instance;

        public static DatabaseContext GetInstance()
        {
            //if (_instance == null)
            //{
            _instance = new DatabaseContext();
            //}
            //else
            //{
            //    _instance.Dispose();
            //    _instance = new DatabaseContext();
            //}
            return _instance;
        }
        #endregion

        #region AspNetUser
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        #endregion

        public DbSet<Log> Logs { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }

        public DbSet<AuthorizationItem> AuthorizationItems { get; set; }
        public DbSet<AuthorizationItemsRelation> AuthorizationItemsRelations { get; set; }

        public DbSet<SystemParameter> SystemParameters { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<InstagramContract> InstagramContracts { get; set; }
        public DbSet<WebsiteContract> WebsiteContracts { get; set; }

        public DbSet<InstagramContractPlan> InstagramContractPlans { get; set; }
        public DbSet<WebsiteContractItem> WebsiteContractItems { get; set; }
        public DbSet<PlanType> PlanTypes { get; set; }
        public DbSet<PlanDurationPrice> PlanDurationPrices { get; set; }
        public DbSet<GeoDivision> GeoDivisions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>().ToTable("Contracts");
            modelBuilder.Entity<InstagramContract>().ToTable("Contracts_Instagram");
            modelBuilder.Entity<WebsiteContract>().ToTable("Contracts_WebsiteContract");
        }
    }
}
