using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class CallbackSearchView
    {
        public int CallbackLogId { get; set; }
        public string TeamName { get; set; }
        public string AssignedToName { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CustomerName { get; set; }
        public string Contact1Phone { get; set; }
        public string CallReferenceNumber { get; set; }
        public int? CallbackStatusId { get; set; }
        public Enum_CallbackStatus CallbackStatus
        {
            get { return (Enum_CallbackStatus)Enum.ToObject(typeof(Enum_CallbackStatus), this.CallbackStatusId);  }
        }

        public DateTimeOffset? AgentCallbackTimeStart { get; set; }
        public DateTimeOffset? AgentCallbackTimeEnd { get; set; }
        public decimal CreatorId { get; set; }
        public string CallbackReasonDescription { get; set; }

        public bool? TeamActive { get; set; }
        public bool? AssignedToActive { get; set; }
        public bool? CreatorActive { get; set; }
        public bool? ReasonTypeActive { get; set; }
        public int? AssignedToTeamId { get; set; }
        public decimal? AssignedToEmpId { get; set; }
        public decimal? CreatedByEmpId { get; set; }
    }
}
