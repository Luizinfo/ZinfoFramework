using Microsoft.EntityFrameworkCore;
using ZinfoFramework.Repository.EF.Contex;
using ZinfoFramework.Repository.EF.Repositories;

namespace ZinfoFramework.Repository.EF.MysqlTests
{
    public class MainRepositoryUsing : RepositoryBaseUsing<TestModel>
    {
        public MainRepositoryUsing(string connectionString) : base(connectionString)
        {
        }

        public override BaseContext GetContext()
        {
            //For mysql
            //var optionsBuilder = new DbContextOptionsBuilder<BaseContext>();
            //optionsBuilder.UseMySQL(connectionString, op => op.CommandTimeout(180));
            //return MainContext.GetInstance(optionsBuilder.Options);

            var builder = new DbContextOptionsBuilder<BaseContext>();
            builder.UseInMemoryDatabase(connectionString);
            var options = builder.Options;
            return MainUsingTestContext.GetInstance(options);
        }
    }
}