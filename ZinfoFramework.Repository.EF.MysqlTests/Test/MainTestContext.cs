using Microsoft.EntityFrameworkCore;

namespace ZinfoFramework.Repository.EF.MysqlTests
{
    public class MainTestContext : DbContext
    {
        public MainTestContext(DbContextOptions<MainTestContext> options) : base(options)
        {
        }

        public virtual DbSet<TestModel> TestModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestModelMap());
        }
    }
}