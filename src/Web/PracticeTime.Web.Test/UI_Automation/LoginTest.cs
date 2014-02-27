using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace PracticeTime.Web.Test.UI_Automation
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod, Ignore]
        public void LoginStudentTest()
        {
            using (var browser = new IE("http://localhost:29116/"))
            {
                if (browser.ContainsText("Hello"))
                {
                    // logout
                    Link logoff = browser.Link(Find.ByText("Log off"));
                    if (logoff.Exists)
                    {
                        logoff.Click();
                        browser.WaitUntilContainsText("Log in");
                    }
                }
                browser.WaitUntilContainsText("Log in");

                var links = browser.Links;
                Assert.IsTrue(links.Count > 0);
                Link login = browser.Link(Find.ByText("Log in"));
                Assert.IsTrue(login.Exists);
                login.Click();
                browser.WaitUntilContainsText("Log in.");
                browser.TextField(Find.ById("UserName")).TypeText("student");
                browser.TextField(Find.ById("Password")).TypeText("student");

                var buttons = browser.Buttons;
                Button loginButton = browser.Button(Find.ByValue("Log in"));
                Assert.IsTrue(loginButton.Exists);
                loginButton.Click();
                browser.WaitUntilContainsText("Hello student!");

                Assert.IsTrue(browser.ContainsText("Sessions"));
            }
        }
    }
}
