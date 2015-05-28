using System;
namespace IOCforMVC.Web.Models
{
    public interface IProteinRepository
    {
        ProteinData GetData(DateTime date);
        void SetGoal(DateTime date, int val);
        void SetTotal(DateTime date, int val);
    }
}
