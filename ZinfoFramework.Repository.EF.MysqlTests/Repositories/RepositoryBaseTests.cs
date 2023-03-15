using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using ZinfoFramework.Repository.EF.MysqlTests;

namespace ZinfoFramework.Repository.EF.Repositories.Tests
{
    [TestFixture()]
    public class RepositoryBaseTests
    {
        [Test()]
        public void AddTest()
        {
            var repository = GetRepository();
            repository.Add(GetTestModel());
            Assert.IsNotNull(repository.Get(1));
        }

        [Test()]
        public void AddAllTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
        }

        [Test()]
        public void DeleteTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            var model = repository.Get(2);
            repository.Delete(model);

            Assert.IsTrue(repository.Get().Count == 1);
        }

        [Test()]
        public void DeleteAllTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            repository.DeleteAll(x => x.Id > 0);

            Assert.IsTrue(repository.Get().Count == 0);
        }

        [Test()]
        public void DisposeTest()
        {
            var repository = GetRepository();
            repository.Dispose();
        }

        [Test()]
        public void EditTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            var model = repository.Get(2);
            model.Nome = "Novo nome";

            repository.Edit(model);
            Assert.AreEqual("Novo nome", repository.Get(model.Id).Nome);
        }

        [Test()]
        public void GetTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Get().Count == 2);
        }

        [Test()]
        public void GetTest1()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Get(2).Id == 2);
        }

        [Test()]
        public void ExisteTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Existe(x => x.Id == 2));
        }

        [Test()]
        public void WhereTest()
        {
            var repository = GetRepository();
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Where(x => x.Nome.Contains("José")).Count == 1);
        }

        private TestModel GetTestModel()
        {
            return new TestModel() { Id = 1, Nome = "José do Pé" };
        }

        private List<TestModel> GetTestModelList()
        {
            var list = new List<TestModel>();
            list.Add(new TestModel() { Id = 2, Nome = "João Simão" });
            list.Add(new TestModel() { Id = 3, Nome = "Maria José" });

            return list;
        }

        private MainRepository GetRepository()
        {
            var builder = new DbContextOptionsBuilder<MainTestContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;
            return new MainRepository(new MainTestContext(options));
        }
    }
}