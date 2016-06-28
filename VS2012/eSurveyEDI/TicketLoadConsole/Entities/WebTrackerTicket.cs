using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLoadConsole.Entities
{
    public class WebTrackerTicket
    {
        public decimal? Contract_id { get; set; }
        public decimal? Log_id { get; set; }
        public decimal? UserId { get; set; }
        public string Cust_FName { get; set; }
        public string Cust_LName { get; set; }
        public string Cust_Email { get; set; }
        public string Cust_Company { get; set; }
        public string Cust_Phone { get; set; }
        public string Cust_Addr1 { get; set; }
        public string Cust_Addr2 { get; set; }
        public string Cust_City { get; set; }
        public string Cust_State { get; set; }
        public string Cust_ZipCode { get; set; }
        public string Cust_Country { get; set; }
        public string OTS_Product { get; set; }
        public string OTS_Platform { get; set; }
        public string OTS_Version { get; set; }
        public decimal? Agnt_Id { get; set; }
        public string Agnt_FName { get; set; }
        public string Agnt_LName { get; set; }
        public string Creation_TS { get; set; }
        public DateTime? Creation_TS_DateTime
        {
            get
            {
                DateTime tmp;
                if (!DateTime.TryParseExact(Creation_TS,"yyyyMMdd",CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                    return null;
                else
                    return tmp;
            }
        }
        public string Resolution_TS { get; set; }

        public DateTime? Resolution_TS_DateTime
        {
            get
            {
                DateTime tmp;
                if (!DateTime.TryParseExact(Resolution_TS, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tmp))
                    return null;
                else
                    return tmp;
            }
        }
        public string Short_Desc { get; set; }
        public string OTS_Category { get; set; }
        public string OTS_SubCategory { get; set; }
        public string FLD_Val1 { get; set; }
        public string FLD_Val2 { get; set; }
        public string OTS_Media_Origin { get; set; }
        public string OTS_System_Origin { get; set; }
        public string UCase_Email { get; set; }
        public string Agent_Site { get; set; }
        public string Cust_Lang { get; set; }


        public ESurveyTicket ToESurveyTicket()
        {
            return new ESurveyTicket()
            {
                Contract_Id = this.Contract_id,
                Ticket_Id = this.Log_id.ToString(),
                Customer_Id = this.UserId.ToString(),
                Customer_FName = this.Cust_FName,
                Customer_LName = this.Cust_LName,
                Email = this.Cust_Email,
                Company = this.Cust_Company,
                Phone = this.Cust_Phone,
                Address1 = this.Cust_Addr1,
                Address2 = this.Cust_Addr2,
                City = this.Cust_City,
                State = this.Cust_State,
                Zip = this.Cust_ZipCode,
                Country = this.Cust_Country,
                Product_Name = this.OTS_Product,
                Product_Code = this.OTS_Platform,
                Product_Cat = this.OTS_Version,
                Agent_Id = this.Agnt_Id.ToString(),
                Agent_FName = this.Agnt_FName,
                Agent_LName = this.Agnt_LName,
                Date_Svc_Start = this.Creation_TS_DateTime,
                Date_Svc_Resolve = this.Resolution_TS_DateTime,
                Svc_Short_Desc = this.Short_Desc,
                Svc_Cat = this.OTS_Category,
                Svc_Sub_Cat = this.OTS_SubCategory,
                Txt_Fld1 = this.FLD_Val1,
                Txt_Fld2 = this.FLD_Val2,
                Ots_Media_Origin = this.OTS_Media_Origin,
                Ots_System_Origin = this.OTS_System_Origin,
                Ucase_Email = this.UCase_Email,
                Agent_Site = this.Agent_Site,
                Cust_Language = this.Cust_Lang,
                Log_Id = this.Log_id                
            };
        }
    }
}
