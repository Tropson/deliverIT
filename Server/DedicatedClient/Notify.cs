using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSub.Extension;
using DedicatedClient.ServiceReference2;

namespace DedicatedClient
{
    class Notify : INotificationServiceCallback
    {
        public void SendNotification()
        {
            this.Publish<NotificationModel>(new NotificationModel { A=1 });
        }
    }
}
