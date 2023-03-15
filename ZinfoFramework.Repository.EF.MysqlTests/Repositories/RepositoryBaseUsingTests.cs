using NUnit.Framework;
using System;
using System.Collections.Generic;
using ZinfoFramework.Repository.EF.MysqlTests;

namespace ZinfoFramework.Repository.EF.Mysql.Repositories.Tests
{
    [TestFixture()]
    public class RepositoryBaseUsingTests
    {
        [Test()]
        public void AddTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.Add(GetTestModel());
            Assert.IsNotNull(repository.Get(1));
        }

        [Test()]
        public void AddAllTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
        }

        [Test()]
        public void DeleteTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            repository.Delete(new TestModel() { Id=2});

            Assert.IsTrue(repository.Get().Count == 1);
        }

        [Test()]
        public void DeleteAllTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            repository.DeleteAll(x=>x.Id > 0);

            Assert.IsTrue(repository.Get().Count == 0);
        }

        [Test()]
        public void DisposeTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.Dispose();
        }

        [Test()]
        public void EditTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            var model = new TestModel() { Id = 2, Nome = "Novo nome" };
            
            repository.Edit(model);
            Assert.AreEqual("Novo nome", repository.Get(model.Id).Nome);
        }

        [Test()]
        public void GetTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Get().Count == 2);
        }

        [Test()]
        public void GetTest1()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Get(2).Id == 2);
        }

        [Test()]
        public void ExisteTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
            repository.AddAll(GetTestModelList());
            Assert.IsTrue(repository.Existe(x => x.Id == 2));
        }

        [Test()]
        public void WhereTest()
        {
            var repository = new MainRepositoryUsing(Guid.NewGuid().ToString());
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
    }
}