using Microsoft.EntityFrameworkCore;
using ZinfoFramework.Repository.EF.Contex;
using ZinfoFramework.Repository.EF.Repositories;

namespace ZinfoFramework.Repository.EF.MysqlTests
{
    public class MainRepository : RepositoryBase<TestModel>
    {
        public MainRepository(DbContext context) : base(context)
        {
        }
    }
}