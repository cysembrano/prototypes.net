using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.WebUI.Models
{
    public class CallbackSettingsModel : BaseModel
    {
        public Team[] TeamList { get; set; }
        public int? TeamSelectedId { get; set; }
    }
}