/**
 * CHAIN OF RESPONSIBILITY
 * From Wikipedia
 * In object-oriented design, the chain-of-responsibility pattern is a design pattern consisting of a source of command objects and 
 * a series of processing objects.[1] Each processing object contains logic that defines the types of command objects that it can handle; 
 * the rest are passed to the next processing object in the chain. A mechanism also exists for adding new processing objects to the end of this chain.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    class Client
    {
        static void Main()
        {
            ExpenseHandler Staff = new ExpenseHandler(new Employee("Stephanie Staff", Decimal.Zero));
            ExpenseHandler Manager = new ExpenseHandler(new Employee("Mary Manager", new Decimal(1000)));
            ExpenseHandler VicePresident = new ExpenseHandler(new Employee("Victor Vicepres", new Decimal(5000)));
            ExpenseHandler President = new ExpenseHandler(new Employee("Paula President", new Decimal(20000)));

            Staff.RegisterNext(Manager);
            Manager.RegisterNext(VicePresident);
            VicePresident.RegisterNext(President);

            Decimal expenseReportAmount = new Decimal(30000);
            IExpenseReport expense = new ExpenseReport(expenseReportAmount);
            ApprovalReponse response = Staff.Approve(expense);
            Console.WriteLine("The request was {0}", response);
        }
    }

    #region Common
    public interface IExpenseReport
    {
        Decimal Total { get; }
    }
    public interface IExpenseApprover
    {
        ApprovalReponse ApproveExpense(IExpenseReport expenseReport);
    }
    public enum ApprovalReponse
    {
        Denied,
        Approved,
        BeyondApprovalLimit
    }
    #endregion

    #region Concrete
    class ExpenseReport : IExpenseReport
    {
        public ExpenseReport(Decimal total)
        {
            this.Total = total;
        }
        public decimal Total
        {
            get;
            private set;
        }
    }

    class Employee : IExpenseApprover
    {
        public string Name { get; private set; }
        private readonly Decimal _approvalLimit;
        public Employee(string name, Decimal approvalLimit)
        {
            Name = name;
            _approvalLimit = approvalLimit;

        }
        public ApprovalReponse ApproveExpense(IExpenseReport expenseReport)
        {
            return expenseReport.Total > _approvalLimit ? ApprovalReponse.BeyondApprovalLimit : ApprovalReponse.Approved;
        }
    }


    #endregion

    #region Handlers
    interface IExpenseHandler
    {
        ApprovalReponse Approve(IExpenseReport expenseReport);
        void RegisterNext(IExpenseHandler next);
    }

    class ExpenseHandler : IExpenseHandler
    {
        private readonly IExpenseApprover _approver;
        private IExpenseHandler _next;
        public ExpenseHandler(IExpenseApprover expenseApprove)
        {
            _approver = expenseApprove;
            _next = EndOfChainExpenseHandler.Intance;
        }

        public ApprovalReponse Approve(IExpenseReport expenseReport)
        {
            ApprovalReponse response = _approver.ApproveExpense(expenseReport);
            if (response == ApprovalReponse.BeyondApprovalLimit)
            {
                return _next.Approve(expenseReport);
            }
            return response;
        }

        public void RegisterNext(IExpenseHandler next)
        {
            _next = next;
        }
    }
    #endregion

    #region NullObjectPattern
    class EndOfChainExpenseHandler : IExpenseHandler
    {
        private EndOfChainExpenseHandler() { }
        private static readonly EndOfChainExpenseHandler _instance = new EndOfChainExpenseHandler();
        public static EndOfChainExpenseHandler Intance
        {
            get { return _instance; }
        }

        public ApprovalReponse Approve(IExpenseReport expenseReport)
        {
            return ApprovalReponse.Denied;
        }

        public void RegisterNext(IExpenseHandler next)
        {
            throw new InvalidOperationException("End of chain handler must be the end of the chain!");
        }
    }

    #endregion
}
