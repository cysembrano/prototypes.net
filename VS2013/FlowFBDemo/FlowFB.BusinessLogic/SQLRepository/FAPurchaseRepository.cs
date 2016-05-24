using FlowFB.Data;
using FlowFB.Repository;
using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowFB.BusinessLogic.Extensions;

namespace FlowFB.BusinessLogic.SQLRepository
{
    public class FAPurchaseRepository : IFAPurchaseRepository
    {
        public IEnumerable<FAPurchase> SearchFAPurchase(FAPurchaseFilter filter)
        {
            FAPurchase[] faPurchase = new FAPurchase[] { };
            using (FlowFBEntities context = new FlowFBEntities())
            {
                if (filter == null)
                {
                    faPurchase =  context.FFBA_Purchase.Where(g=> g.Status != 0).AsQueryable().ToFAPurchase();  
                }                    
                else
                {
                    faPurchase = context.FFBA_Purchase.Where(g => g.Status != 0 && (g.ProjectID == filter.ProjectID ||
                                                                  g.PurchaseInvoiceNumber == filter.PurchaseInvoiceNumber))
                                                                  .AsQueryable()
                                                                  .ToFAPurchase();
                }
            }

            return faPurchase;
        }
    }
}
