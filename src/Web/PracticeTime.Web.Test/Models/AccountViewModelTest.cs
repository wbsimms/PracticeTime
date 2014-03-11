using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class AccountViewModelTest
    {
        [TestMethod]
        public void ExternalLoginConfirmationViewModelTest()
        {
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel()
            {
                SelectedAccountType = "",
                UserName = "blah",
                StudentToken = "blah",
                FirstName = "blah",
                LastName = "blah",
                EmailAddress = "blah", 
                StudentPublicProfile = true,
                City = "Plymouth",
                State = States.MA
            };
            Assert.IsNotNull(model.UserName);
            Assert.IsNotNull(model.AccountTypes);
            Assert.IsNotNull(model.SelectedAccountType);
            Assert.IsNotNull(model.StudentToken);
            Assert.IsNotNull(model.FirstName);
            Assert.IsNotNull(model.LastName);
            Assert.IsNotNull(model.EmailAddress);
            Assert.IsTrue(model.StudentPublicProfile);
            Assert.IsNotNull(model.City);
            Assert.AreEqual("MA",model.State.ToString());
            Assert.IsTrue(model.StateTypes.Any());
        }

        [TestMethod]
        public void ManageUserViewModelTest()
        {
            ManageUserViewModel model = new ManageUserViewModel(){StudentToken = "blah", StudentPublicProfile = true};
            model.ConfirmPassword = "blah";
            model.NewPassword = "blah1";
            model.OldPassword = "blah1";
            Assert.IsNotNull(model.ConfirmPassword);
            Assert.IsNotNull(model.NewPassword);
            Assert.IsNotNull(model.OldPassword);
            Assert.IsNotNull(model.StudentToken);
            Assert.IsTrue(model.StudentPublicProfile);
        }

        [TestMethod]
        public void LoginViewModelTest()
        {
            LoginViewModel model = new LoginViewModel() {Password = "blah",RememberMe = false,UserName = "name"};
            Assert.IsNotNull(model.Password);
            Assert.IsNotNull(model.RememberMe);
            Assert.IsNotNull(model.UserName);
        }

        [TestMethod]
        public void RegisterViewModelTest()
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                ConfirmPassword = "blah",Password = "blah",UserName = "sfdgh",
                SelectedAccountType = "",
                StudentToken = "blah",
                FirstName = "blah",
                LastName = "blah",
                EmailAddress = "blah",
                StudentPublicProfile = true,
                City = "Plymouth",
                State = States.MA
            };
            Assert.IsNotNull(model.ConfirmPassword);
            Assert.IsNotNull(model.Password);
            Assert.IsNotNull(model.UserName);
            Assert.IsNotNull(model.AccountTypes);
            Assert.IsNotNull(model.SelectedAccountType);
            Assert.IsNotNull(model.StudentToken);
            Assert.IsNotNull(model.FirstName);
            Assert.IsNotNull(model.LastName);
            Assert.IsNotNull(model.EmailAddress);
            Assert.IsTrue(model.StudentPublicProfile);
            Assert.IsNotNull(model.City);
            Assert.AreEqual("MA", model.State.ToString());
            Assert.IsTrue(model.StateTypes.Any());
        }
    }
}
