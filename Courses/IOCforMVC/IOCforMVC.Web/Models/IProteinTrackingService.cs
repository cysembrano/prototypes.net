using System;
namespace IOCforMVC.Web.Models
{
    public interface IProteinTrackingService
    {
        void AddProtein(int amount);
        int Goal { get; set; }
        int Total { get; set; }
    }
}
