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
        public IEnumerable<FAPurchase> SearchFAPurchases(FAPurchaseFilter filter)
        {
            FAPurchase[] faPurchase = new FAPurchase[] { };
            using (FlowFBEntities context = new FlowFBEntities())
            {
                if (filter == null)
                {
                    faPurchase =  context.FFBA_Purchase.Where(g=> g.Status != 0).AsQueryable().ToFAPurchases();  
                }                    
                else
                {
                    var result = context.FFBA_Purchase.Where(g => g.Status != 0 && (g.ProjectID == filter.ProjectID ||
                                                                  g.PurchaseInvoiceNumber == filter.PurchaseInvoiceNumber
                                                                  ));
                    if(filter.Status.HasValue)
                    {
                        faPurchase = result.Where(t => t.Status == filter.Status.Value).AsEnumerable().ToFAPurchases();
                    }
                    else
                    {
                        faPurchase = result.AsEnumerable().ToFAPurchases();
                    }
                }
            }

            return faPurchase;
        }


        public FAPurchase SearchFAPurchase(int purchaseId)
        {
            FAPurchase faPurchase = new FAPurchase();
            using (FlowFBEntities context = new FlowFBEntities())
            {

                    var result = context.FFBA_Purchase.Where(g => g.Status != 0 && g.PurchaseID == purchaseId)
                                                      .FirstOrDefault();
                    if(result != null)
                        faPurchase = result.ToFAPurchase();

            }

            return faPurchase;
        }


        public void SaveFAPurchaseComment(int PurchaseId, string Comment)
        {
            using (FlowFBEntities context = new FlowFBEntities())
            {

                var result = context.FFBA_Purchase.Where(g => g.PurchaseID == PurchaseId)
                                                  .FirstOrDefault();
                if (result != null)
                {
                    result.Comment = Comment;
                    context.SaveChanges();
                }                   

            }
        }
    }
}
