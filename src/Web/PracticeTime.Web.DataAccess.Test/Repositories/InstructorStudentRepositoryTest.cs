using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNet.Identity;
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
        private UserStore<ApplicationUser> store;

        [TestInitialize]
        public void Setup()
        {
            if (store == null)
            {
                store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                student = store.FindByNameAsync("student").Result;
                teacher = store.FindByNameAsync("teacher").Result;
            }
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
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                Assert.IsTrue(manager.CreateAsync(new ApplicationUser() {C_AccountTypeId = 1, UserName = "blahstudent"}, "blahstudent").Result.Succeeded);
                Assert.IsTrue(manager.CreateAsync(new ApplicationUser() { C_AccountTypeId = 1, UserName = "blahteacher" }, "blahteacher").Result.Succeeded);
                string blahstudentId = manager.FindByNameAsync("blahstudent").Result.Id;
                string blahteacherId = manager.FindByNameAsync("blahteacher").Result.Id;
                InstructorStudentRepository repo = new InstructorStudentRepository();
                InstructorStudent retval =
                    repo.Add(new InstructorStudent() { InstructorId = blahteacherId, StudentId = blahstudentId });
                Assert.IsNotNull(retval);
                Assert.IsTrue(retval.InstructorStudentId > 0);
                InstructorStudent retval2 =
                    repo.Add(new InstructorStudent() { InstructorId = blahteacherId, StudentId = blahstudentId });
                Assert.IsNull(retval2);

                InstructorStudent instructorStudent = repo.GetByStudentIdInstructorId(blahstudentId, blahteacherId);
                Assert.IsNotNull(instructorStudent);
            }
        }

        [TestMethod]
        public void GetAllForInstructorTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                InstructorStudentRepository repo = new InstructorStudentRepository();
                List<InstructorStudent> list = repo.GetAllForInstructor(teacher.Id);
                Assert.IsNotNull(list);
                Assert.IsTrue(list.Count > 1);
                Assert.IsNotNull(list.First().Student);
                Assert.AreEqual("student",list.First().Student.UserName);
            }
        }

        [TestMethod]
        public void UpdateDeleteTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                InstructorStudentRepository repo = new InstructorStudentRepository();
                InstructorStudent retval = repo.GetById(1);
                Assert.IsNotNull(retval);
                Assert.IsTrue(retval.InstructorStudentId > 0);
                int count = repo.GetAll().Count;
                repo.Delete(retval);
                Assert.AreEqual(count-1, repo.GetAll().Count);
            }
        }

        [TestMethod]
        public void DeleteWithoutPrimaryIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                Assert.IsTrue(manager.CreateAsync(new ApplicationUser() { C_AccountTypeId = 1, UserName = "blahstudent" }, "blahstudent").Result.Succeeded);
                Assert.IsTrue(manager.CreateAsync(new ApplicationUser() { C_AccountTypeId = 1, UserName = "blahteacher" }, "blahteacher").Result.Succeeded);
                string blahstudentId = manager.FindByNameAsync("blahstudent").Result.Id;
                string blahteacherId = manager.FindByNameAsync("blahteacher").Result.Id;
                InstructorStudentRepository repo = new InstructorStudentRepository();
                InstructorStudent retval =
                    repo.Add(new InstructorStudent() { InstructorId = blahteacherId, StudentId = blahstudentId });

                int count = repo.GetAll().Count;
                repo.Delete(new InstructorStudent(){StudentId = blahstudentId,InstructorId = blahteacherId});
                Assert.AreEqual(count - 1, repo.GetAll().Count);
            }
        }

    }
}
