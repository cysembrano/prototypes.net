using System;
using System.Collections.Generic;
using Convergys.Assist.BusinessLogic.SQLRepository;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.Repository.SearchFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convergys.Assist.Test
{
    [TestClass]
    public class OfflineSQLTest
    {
        private IOfflineRepository _offrepo;
        private IPublicRepository _pubrepo;
        private EmployeeView empView;

        public OfflineSQLTest()
        {
            _offrepo = new OfflineRepository();
            _pubrepo = new PublicRepository();
        }
        [TestInitialize]
        public void Initialize()
        {
            this.empView = _pubrepo.LoginEmployee("Sylvain_orfani@stream.com", "yarhkFZs");
        }

        [TestMethod]
        public void GetCallbacks_FilterbyKeyword()
        {

            OfflineSearchFilter filter = new OfflineSearchFilter();
            filter.Keyword = "153634407";

            var callbacksFound = _offrepo.SearchOfflineActivities(filter);
            int length = 0;
            foreach(var call in callbacksFound)
                length++;

            Assert.IsTrue(length > 0, "SearchOfflineActivities returned Ok ");
        }

    }
}
