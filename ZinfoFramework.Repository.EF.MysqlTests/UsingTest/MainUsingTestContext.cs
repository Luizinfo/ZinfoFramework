using Microsoft.EntityFrameworkCore;
using ZinfoFramework.Repository.EF.Contex;

namespace ZinfoFramework.Repository.EF.MysqlTests
{
    public class MainUsingTestContext : BaseContext
    {
        public MainUsingTestContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public virtual DbSet<TestModel> TestModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestModelMap());
        }

        public static MainUsingTestContext GetInstance(DbContextOptions<BaseContext> options)
        {
            return new MainUsingTestContext(options);
        }
    }
}