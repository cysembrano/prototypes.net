using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository
{
    public interface IFAPurchaseRepository
    {
        IEnumerable<FAPurchase> SearchFAPurchases(FAPurchaseFilter filter);

        FAPurchase SearchFAPurchase(int purchaseId);

        void SaveFAPurchaseComment(int PurchaseId, string Comment);

        IEnumerable<FAContact> GetAllFAContacts(int projectId);
    }
}
