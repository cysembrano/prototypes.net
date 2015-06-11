using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventAggregator
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(Object subscriber);
    }


}
