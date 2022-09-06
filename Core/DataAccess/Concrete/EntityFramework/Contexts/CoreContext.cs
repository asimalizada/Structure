using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.Concrete.EntityFramework.Contexts
{
    public class CoreContext : DbContext
    {
        #region DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        #endregion

        public CoreContext()
        {

        }

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = (localdb)\MSSQLLocalDB; Database = GuideDb; Trusted_connection = true");
        }
    }
}
