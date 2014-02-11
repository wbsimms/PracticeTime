using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.DataAccess.Test.Repositories
{
    [TestClass]
    public class AccountTypeRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            AccountTypeRepository repo = new AccountTypeRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void GetAllTest()
        {
            AccountTypeRepository repo = new AccountTypeRepository();
            List<C_AccountType> badges = repo.GetAll();
            Assert.IsTrue(badges.Count > 0);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            AccountTypeRepository repo = new AccountTypeRepository();
            C_AccountType item = repo.GetById(1);
            Assert.IsNotNull(item);
            Assert.AreEqual("Student", item.Name);
            Assert.IsNotNull(item.Name);
            Assert.IsNotNull(item.Description);
            Assert.IsNotNull(item.C_AccountTypeId);
        }

    }
}
