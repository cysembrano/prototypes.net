using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IOCforMVC.Web.Controllers;
using System.Web.Mvc;
using IOCforMVC.Web.Models;

namespace IOCforMVC.Tests.Controllers
{
    [TestClass]
    public class ProteinTrackerControllerTest
    {
        [TestMethod]
        public void WhenNothingHasHappenedTotalAndGoalAreZero()
        {
            ProteinTrackerController controller = new ProteinTrackerController(new StubTrackingService());
            var result = controller.Index() as ViewResult;

            Assert.AreEqual(0, result.ViewBag.Total);
            Assert.AreEqual(0, result.ViewBag.Goal);
        }

        public class StubTrackingService : IProteinTrackingService
        {
            public void AddProtein(int amount)
            {
                throw new NotImplementedException();
            }

            public int Goal
            {
                get
                {
                    return 0;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public int Total
            {
                get
                {
                    return 0;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }


    }
}
