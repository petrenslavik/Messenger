using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace MessengerWindowsClient.Managers.ServiceInfrastructure
{
    public class TokenBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new TokenEndpointBehavior();
        }

        public override Type BehaviorType => typeof(TokenEndpointBehavior);
    }
}
