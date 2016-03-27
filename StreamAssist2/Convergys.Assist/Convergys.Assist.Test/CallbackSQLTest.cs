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
    public class CallbackSQLTest
    {
        private ICallbackRepository _callrepo;
        private IPublicRepository _pubrepo;
        private EmployeeView empView;
        
        public CallbackSQLTest()
        {
            _callrepo = new CallbackRepository();
            _pubrepo = new PublicRepository();
        }
        [TestInitialize]
        public void Initialize()
        {
            this.empView = _pubrepo.LoginEmployee("Rhonda.Koop@Stream.com", "5mQmSnQf");
        }

        [TestMethod]
        public void GetCallbacks_FilterbyScheduled()
        {

            CallbackSearchFilter filter = new CallbackSearchFilter();
            filter.From = new DateTime(2008, 03, 30);
            filter.To = new DateTime(2008, 5, 26);

            var callbacksFound = _callrepo.SearchCallbacks(filter);
            int length = 0;
            foreach (var call in callbacksFound)
                length++;

            Assert.IsTrue(length > 0,"GetCallbacks returned 0 ");
        }


        [TestMethod]
        public void GetCallbacks_FilterbyKeyword()
        {

            CallbackSearchFilter filter = new CallbackSearchFilter();
            filter.Keyword = "Steppo";

            var callbacksFound = _callrepo.SearchCallbacks(filter);
            int length = 0;
            foreach(var call in callbacksFound)
                length++;

            Assert.IsTrue(length > 0, "GetCallbacks returned 0 ");
        }


        [TestMethod]
        public void AddCallback_ReturnNewScopeIdentity()
        {
            Callback cb = new Callback();
            cb.AssignedToTeamId = 2047;
            cb.AssignedToTeamName = "GLB Contact Center App - PH Corp Global";
            cb.AssignedToEmpId = 95744;
            cb.AssignedToEmpName = "Cyrus Sembrano";
            cb.CustomerName = "Henry Ford";
            cb.CallRefNumber = "228-446";
            cb.Contact1Phone = "1234567890";
            cb.Contact2Phone = "09876544321";
            cb.Comments = "Call him up as soon as possible.  The assembly line is broken!";
            cb.CallbackStatusId = Enum_CallbackStatus.Open.GetHashCode();

            cb.AgentCallbackTimeStart = DateTime.Now;
            cb.AgentCallbackTimeEnd = DateTime.Now.AddMinutes(30);

            cb.CustomerCallbackTimeStart = DateTime.Now;
            cb.CustomerCallbackTimeEnd = DateTime.Now.AddMinutes(30);

            var newCallbackId = _callrepo.AddCallback(this.empView, cb);
            Assert.IsTrue(newCallbackId > 1);

        }

        [TestMethod]
        public void ViewCallback_TestId()
        {
            var callback = _callrepo.ViewCallback(this.empView, 132);

            Assert.IsTrue(callback.Id == 132);
        }
    }
}
