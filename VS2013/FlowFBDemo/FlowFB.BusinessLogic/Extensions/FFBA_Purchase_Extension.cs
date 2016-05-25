using FlowFB.Data;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.BusinessLogic.Extensions
{
    public static class FFBA_Purchase_Extension
    {
        public static FAPurchase[] ToFAPurchases(this IEnumerable<FFBA_Purchase> entities)
        {
            return entities.Select(d => d.ToFAPurchase()).ToArray();
        }

        public static FAPurchase ToFAPurchase(this FFBA_Purchase entity)
        {
            var purchase = new FAPurchase()
            {
                DateChanged = entity.DateChanged,
                DateCreated = entity.DateCreated,
                DateStarted = entity.DateStarted,
                Destruction = entity.Destruction,
                Notes = entity.Notes,
                ProjectID = entity.ProjectID,
                PurchaseID = entity.PurchaseID,
                PurchaseInvoiceDescription = entity.PurchaseInvoiceDescription,
                PurchaseInvoiceNumber = entity.PurchaseInvoiceNumber,
                PurchaseInvoiceTotal = entity.PurchaseInvoiceTotal,
                Status = entity.Status,
                Comments = entity.Comment
            };        

            return purchase;
        }
    }
}
