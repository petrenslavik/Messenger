using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace MessengerServices.Infrastructure
{
    public class ServiceBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ServiceBehavior();
        }

        public override Type BehaviorType => typeof(ServiceBehavior);
    }
}