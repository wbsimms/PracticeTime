using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.DataAccess.Test.Repositories
{
    [TestClass]
    public class InstructorStudentRepositoryTest
    {
        private ApplicationUser student, teacher;


        [TestInitialize]
        public void Setup()
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
            student = store.FindByNameAsync("student").Result;
            teacher = store.FindByNameAsync("teacher").Result;
        }

        [TestMethod]
        public void ConstructorTest()
        {
            InstructorStudentRepository repo = new InstructorStudentRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void AddTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                InstructorStudentRepository repo = new InstructorStudentRepository();
                InstructorStudent retval =
                    repo.Add(new InstructorStudent() {InstructorId = teacher.Id, StudentId = student.Id});
                Assert.IsNotNull(retval);
                Assert.IsTrue(retval.InstructorStudentId > 0);
            }
        }
    }
}
