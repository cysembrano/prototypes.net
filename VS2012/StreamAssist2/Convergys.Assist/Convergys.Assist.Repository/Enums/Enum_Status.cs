using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.Enums
{
    public enum Enum_CallbackStatus
    {
        Closed = 0,
        Open = 1,
    }

    public enum Enum_OfflineStatus
    {
        Closed = 0,
        Open = 1,
    }

    public enum Enum_OfflineEventStatus
    {
        none = 0,
        success = 1,
        warning = 2,
        danger = 3,
    }
}
