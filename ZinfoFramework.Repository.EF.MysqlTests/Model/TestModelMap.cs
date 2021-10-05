using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZinfoFramework.Repository.EF.MysqlTests
{
    internal class TestModelMap : IEntityTypeConfiguration<TestModel>
    {
        public void Configure(EntityTypeBuilder<TestModel> builder)
        {
            builder.Property(e => e.Id).IsRequired();

            builder.Property(e => e.Nome).HasMaxLength(200);
        }
    }
}