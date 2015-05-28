using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCforMVC.Web.Models
{
    public class ProteinRepository : IOCforMVC.Web.Models.IProteinRepository
    {

        private static ProteinData _proteinData = new ProteinData();
        public ProteinData GetData(DateTime date)
        {
            _proteinData.EffectiveDate = date;
            return _proteinData;
        }

        public void SetGoal(DateTime date, int val)
        {
            _proteinData.EffectiveDate = date;
            _proteinData.Goal = val;
        }

        public void SetTotal(DateTime date, int val)
        {
            _proteinData.EffectiveDate = date;
            _proteinData.Total = val;
        }

    }
}