using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messageboards.Web.Controllers;
using Messageboards.Tests.Fakes;
using System.Linq;
using Messageboards.Web.Data;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Newtonsoft.Json;

namespace Messageboards.Tests
{
    [TestClass]
    public class TopicsControllerTests
    {
        private TopicsController _ctrl;
        [TestInitialize]
        public void Init()
        {
            _ctrl = new TopicsController(new FakeMessageBoardRepository());
        }

        [TestMethod]
        public void TopicsController_Get()
        {
            

            var results = _ctrl.Get(true);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
            Assert.IsNotNull(results.First());
            Assert.IsNotNull(results.First().Title);

        }

        [TestMethod]
        public void TopicsController_Post()
        {
            // Testing POST is harder than it should be but we need to do some work:

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/v1/topics");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "topics" } });

            _ctrl.ControllerContext = new HttpControllerContext(config, routeData, request);
            _ctrl.Request = request;
            _ctrl.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var newTopic = new Topic()
            {
                Title = "Test Topic",
                Body = "This is a test of a topic...."
            };

            var result = _ctrl.Post(newTopic);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            var json = result.Content.ReadAsStringAsync().Result;
            var topic = JsonConvert.DeserializeObject<Topic>(json);

            Assert.IsNotNull(topic);
            Assert.IsTrue(topic.Id > 0);
            Assert.IsTrue(topic.Created > DateTime.MinValue);

        }
    }
}
