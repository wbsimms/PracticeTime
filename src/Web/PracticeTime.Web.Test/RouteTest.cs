using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PracticeTime.Web.Test
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void DefaultTest()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteEvaluator routeEvaluator = new RouteEvaluator(routes);
            IList<RouteData> routeData = routeEvaluator.GetMatches("~/");
            Assert.IsTrue(routeData.Count > 0);
            Assert.AreEqual("Home",routeData[0].Values["controller"]);
        }

        [TestMethod]
        public void SessionsTest()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteEvaluator routeEvaluator = new RouteEvaluator(routes);
            IList<RouteData> routeData = routeEvaluator.GetMatches("~/Sessions");
            Assert.IsTrue(routeData.Count > 0);
            Assert.AreEqual("Sessions", routeData[0].Values["controller"]);

            routeData = routeEvaluator.GetMatches("~/Add");
            Assert.IsTrue(routeData.Count > 0);
            Assert.AreEqual("Add", routeData[0].Values["controller"]);

        }

    }

    public class RouteEvaluator
    {
        private RouteCollection routes;

        public RouteEvaluator(RouteCollection routes)
        {
            this.routes = routes;
        }

        public IList<RouteData> GetMatches(string virtualPath)
        {
            return GetMatches(virtualPath, "GET");
        }

        public IList<RouteData> GetMatches(string virtualPath, string httpMethod)
        {
            List<RouteData> matchingRouteData = new List<RouteData>();

            foreach (var route in this.routes)
            {
                var context = new Mock<HttpContextBase>();
                var request = new Mock<HttpRequestBase>();

                context.Setup(ctx => ctx.Request).Returns(request.Object);
                request.Setup(req => req.PathInfo).Returns(string.Empty);
                request.Setup(req =>
                    req.AppRelativeCurrentExecutionFilePath).Returns(virtualPath);
                if (!string.IsNullOrEmpty(httpMethod))
                {
                    request.Setup(req => req.HttpMethod).Returns(httpMethod);
                }

                RouteData routeData = this.routes.GetRouteData(context.Object);
                if (routeData != null)
                {
                    matchingRouteData.Add(routeData);
                }
            }
            return matchingRouteData;
        }
    }
}
