using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel()
            {
                AccountTypes = new SelectList(new SelectListItem[2]),
                SelectedAccountType = "",
                UserName = "blah",
                StudentToken = "blah",
                FirstName = "blah",LastName = "blah",EmailAddress = "blah"
            };
            Assert.IsNotNull(model.UserName);
            Assert.IsNotNull(model.AccountTypes);
            Assert.IsNotNull(model.SelectedAccountType);
            Assert.IsNotNull(model.StudentToken);
            Assert.IsNotNull(model.FirstName);
            Assert.IsNotNull(model.LastName);
            Assert.IsNotNull(model.EmailAddress);
        }

        [TestMethod]
        public void ManageUserViewModelTest()
        {
            ManageUserViewModel model = new ManageUserViewModel(){StudentToken = "blah"};
            model.ConfirmPassword = "blah";
            model.NewPassword = "blah1";
            model.OldPassword = "blah1";
            Assert.IsNotNull(model.ConfirmPassword);
            Assert.IsNotNull(model.NewPassword);
            Assert.IsNotNull(model.OldPassword);
            Assert.IsNotNull(model.StudentToken);
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
                AccountTypes = new SelectList(new SelectListItem[2]),
                SelectedAccountType = "",
                StudentToken = "blah",
                FirstName = "blah",
                LastName = "blah",
                EmailAddress = "blah"

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
        }
    }
}
