using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSub.Extension;
using DedicatedClient.ServiceReference1;
using System.ServiceModel;

namespace DedicatedClient
{
    class Callback : IAdminServiceCallback
    {
        public void Notify()
        {
            this.Publish<NotificationModel>(new NotificationModel { A=1});
        }
    }
}
