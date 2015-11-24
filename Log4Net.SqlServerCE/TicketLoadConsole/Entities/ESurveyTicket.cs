using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLoadConsole.Entities
{
    public class ESurveyTicket
    {
        public decimal? Doc_Id { get; set; }
        public decimal? Contract_Id { get; set; }
        public string Ticket_Id { get; set; }
        public string Customer_Id { get; set; }
        public string Customer_FName { get; set; }
        public string Customer_LName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Product_Name { get; set; }
        public string Product_Code { get; set; }
        public string Product_Cat { get; set; }
        public string Agent_Id { get; set; }
        public string Agent_FName { get; set; }
        public string Agent_LName { get; set; }
        public DateTime? Date_Svc_Start { get; set; }
        public DateTime? Date_Svc_Resolve { get; set; }
        public string Svc_Short_Desc { get; set; }
        public string Svc_Cat { get; set; }
        public string Svc_Sub_Cat { get; set; }
        public string Txt_Fld1 { get; set; }
        public string Txt_Fld2 { get; set; }
        public string Ots_Media_Origin { get; set; }
        public string Ots_System_Origin { get; set; }
        public string Ucase_Email { get; set; }
        public string Agent_Site { get; set; }
        public string Cust_Language { get; set; }
        public decimal? Log_Id { get; set; }


    }
}
