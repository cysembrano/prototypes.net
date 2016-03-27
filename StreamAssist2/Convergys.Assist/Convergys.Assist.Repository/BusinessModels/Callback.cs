using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class Callback
    {
        public int Id { get; set; }
        public decimal AssignedToTeamId { get; set; }
        public decimal AssignedToEmpId { get; set; }
        public decimal CreatedByEmpId { get; set; }
        public string AssignedToTeamName { get; set; }
        public string AssignedToEmpName { get; set; }
        public string CreatedByEmpName { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string Contact1Phone { get; set; }
        public string Contact2Phone { get; set; }
        public string CallRefNumber { get; set; }
        public string Comments { get; set; }
        public int? CallbackStatusId { get; set; }
        public decimal? StatusId 
        {
            get
            {
                if (!CallbackStatusId.HasValue)
                    return null;

                return Convert.ToDecimal(CallbackStatusId.Value);
            }
        }
        /// <summary>
        /// Gets CallbackStatus.  e.g. Open or Close
        /// </summary>
        public Enum_CallbackStatus CallbackStatus
        {
            get
            {
                if (!CallbackStatusId.HasValue)
                    return Enum_CallbackStatus.Open;

                return (Enum_CallbackStatus)Enum.ToObject(typeof(Enum_CallbackStatus), this.CallbackStatusId);
            }
        }
        public DateTimeOffset? CustomerCallbackTimeStart { get; set; }
        private DateTimeOffset? customerCallbackTimeEnd;
        public DateTimeOffset? CustomerCallbackTimeEnd 
        {
            get
            {
                if (customerCallbackTimeEnd.HasValue)
                    return customerCallbackTimeEnd;
                else if (this.CustomerCallbackTimeStart.HasValue)
                    return this.CustomerCallbackTimeStart.Value.AddMinutes(15.00);
                else
                    return null;
            }
            set
            {
                customerCallbackTimeEnd = value;
            }
        }
        public DateTimeOffset? AgentCallbackTimeStart { get; set; }
        private DateTimeOffset? agentCallbackTimeEnd;
        public DateTimeOffset? AgentCallbackTimeEnd
        {
            get
            {
                if (agentCallbackTimeEnd.HasValue)
                    return agentCallbackTimeEnd;
                else if (this.AgentCallbackTimeStart.HasValue)
                    return this.AgentCallbackTimeStart.Value.AddMinutes(15.00);
                else
                    return null;
            }
            set
            {
                agentCallbackTimeEnd = value;
            }
        }
        public int? CallbackReasonId { get; set; }
        public string CallbackReasonText { get; set; }
        
    }
}
