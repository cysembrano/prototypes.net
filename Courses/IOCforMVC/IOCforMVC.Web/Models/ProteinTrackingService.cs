using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCforMVC.Web.Models
{
    public class ProteinTrackingService : IProteinTrackingService
    {
        private IProteinRepository _repository;

        public ProteinTrackingService(IProteinRepository repository)
        {
            _repository = repository;
        }

        public int Total
        {
            get { return _repository.GetData(new DateTime().Date).Total; }
            set { _repository.SetTotal(new DateTime().Date, value); }
        }

        public int Goal
        {
            get { return _repository.GetData(new DateTime().Date).Goal; }
            set { _repository.SetGoal(new DateTime().Date, value); }
        }
        public void AddProtein(int amount)
        {
            Total += amount;
        }
    }
}