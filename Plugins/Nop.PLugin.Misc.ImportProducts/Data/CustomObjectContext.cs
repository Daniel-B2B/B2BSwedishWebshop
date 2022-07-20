using Nop.Core;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.PLugin.Misc.ImportProducts.Data
{
    public class CustomObjectContext : DbContext, IDbContext
    {
        #region Ctor
        public CustomObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
        #endregion

        #region Properties
        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        #endregion

        #region Methods

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }
        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = default(int?), params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void Install()
        {
            // Create Table

            //var dbScript = CreateDatabaseScript();
            //Database.ExecuteSqlCommand(dbScript);
            //SaveChanges();
        }

        public void Uninstall()
        {
            //var productSellTable = this.GetTableName<GroupBuyProduct>();
            //var customTierPrice = this.GetTableName<CustomTierPriceModel>();
            //var testimonialTableName = this.GetTableName<NewsLetterSubscriptionGroupBuy>();

            //this.DropPluginTable(customTierPrice);
            //this.DropPluginTable(productSellTable);
            //this.DropPluginTable(testimonialTableName);
        }

        #endregion

        #region Utilities
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomProductMap());
            //modelBuilder.Configurations.Add(new CustomTierPriceMap());
            //modelBuilder.Configurations.Add(new NewsLetterSubscriptionGroupBuyMap());
            Database.SetInitializer<CustomObjectContext>(null);
            //modelBuilder.Entity<ProductSellModel>().Ignore(p => p.IsJCourouselEnabled);
            base.OnModelCreating(modelBuilder);
        }



        #endregion
    }
}


