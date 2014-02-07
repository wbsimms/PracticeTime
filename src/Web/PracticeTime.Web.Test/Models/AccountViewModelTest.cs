using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class AccountViewModelTest
    {
        [TestMethod]
        public void ExternalLoginConfirmationViewModelTest()
        {
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel();
            model.UserName = "blah";
            Assert.IsNotNull(model.UserName);
        }

        [TestMethod]
        public void ManageUserViewModelTest()
        {
            ManageUserViewModel model = new ManageUserViewModel();
            model.ConfirmPassword = "blah";
            model.NewPassword = "blah1";
            model.OldPassword = "blah1";
            Assert.IsNotNull(model.ConfirmPassword);
            Assert.IsNotNull(model.NewPassword);
            Assert.IsNotNull(model.OldPassword);
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
            RegisterViewModel model = new RegisterViewModel(){ConfirmPassword = "blah",Password = "blah",UserName = "sfdgh"};
            Assert.IsNotNull(model.ConfirmPassword);
            Assert.IsNotNull(model.Password);
            Assert.IsNotNull(model.UserName);
        }
    }
}
