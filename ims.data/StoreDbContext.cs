using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ims.data.Entities;

namespace ims.data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<FactoryToStore> FactoryToStores { get; set; }
        public DbSet<MachineCode> MachineCodes { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Sole> Soles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StoreToShop> StoreToShops { get; set; }
        public DbSet<StoreStock> StoreStocks { get; set; }
        public DbSet<ShopStock> ShopStocks { get; set; }
        public DbSet<ShoeModel> ShoeModels { get; set; }
        public DbSet<Store> Stores { get; set; }
        //public Stock Stock { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ActionType> ActionType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserAction> UserAction { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Workflow> Workflow { get; set; }
        public DbSet<WorkflowType> WorkflowType { get; set; }
        public DbSet<WorkItem> WorkItem { get; set; }
        public DbSet<WorkflowDocuments> WorkflowDocuments { get; set; }

        public DbSet<PurchaseToStore> PurchaseToStores { get; set; }
        public DbSet<ShopToStore> ShopToStores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ShoeTransactionList> ShoeTransactionLists { get; set; }
        public DbSet<SalesReport> SalesReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //         optionsBuilder.UseNpgsql("Host=localhost;Database=cfcs;Username=postgres;Password=postgres");
            }
        }

        public bool ContextOwnsConnection { get; }

        public void SaveChanges(string username, UserAction userAction)
        {
            UserAction.Add(userAction);
            base.SaveChanges();
            var auditEnries = OnBeforeSaveChanges(username, userAction.Id);
            base.SaveChanges();
            OnAfterSaveChanges(auditEnries);
        }

        public UserAction SaveChanges(string username, int actionType)
        {
            var userAction = new UserAction
            {
                Actiontypeid = actionType,
                Username = username,
                Timestamp = DateTime.Now.Ticks
            };
            UserAction.Add(userAction);
            var auditEnries = OnBeforeSaveChanges(username, userAction.Id);
            base.SaveChanges();
            OnAfterSaveChanges(auditEnries);

            return userAction;
        }

        private List<AuditEntry> OnBeforeSaveChanges(string username, long actionId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.Entity is UserAction || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.Model.ToString(),
                    UserName = username,
                    UserAction = actionId
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary) //For auto-generated properties such as id
                    {
                        //get the value after save
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            //Save audit log for all the changes
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
                this.AuditLog.Add(auditEntry.ToAudit());

            //return those for which we need to get their primary keys for
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private void OnAfterSaveChanges(List<AuditEntry> entries)
        {
            if (entries == null || entries.Count == 0) return;


            foreach (var auditEntry in entries)
            {
                //Get the auto-generated values
                foreach (var prop in auditEntry.TemporaryProperties)
                    if (prop.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    else
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;

                AuditLog.Add(auditEntry.ToAudit());
            }

            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }

        //public DbSet<RIMS.Services.Auth.UserDetialViewModel> UserDetialViewModel { get; set; }

        //public DbSet<RIMS.Services.Auth.RegisterViewModel> RegisterViewModel { get; set; }

        //public DbSet<RIMS.ViewModel.TransactionRequestModal> TransactionRequestModal { get; set; }

        //public DbSet<RIMS.ViewModel.TransactionViewModel> TransactionViewModel { get; set; }

    }

    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            string replaced = Regex.Replace(input, "(?<=.)([A-Z])", "_$0",
                      RegexOptions.Compiled);
            var startUnderscores = Regex.Match(replaced, @"^_+");
            return startUnderscores + Regex.Replace(replaced, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
