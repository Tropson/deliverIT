using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NotificationService.ServiceReference1;

namespace NotificationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Single,InstanceContextMode =InstanceContextMode.Single)]
    public class NotificationService : INotificationService
    {
        INotificationServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<INotificationServiceCallback>();
            }
        }
        AdminServiceClient proxy = new AdminServiceClient();
        public void CallCallback()
        {
            var a = proxy.GetAllApplications();
            while (true)
            {
                System.Threading.Tasks.Task.Delay(5000);
                var b = proxy.GetAllApplications();
                if (a.Length == b.Length)
                {
                    continue;
                }
                else
                {
                    a = b;
                    Callback.SendNotification();
                }
            }
        }
        public static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(NotificationService));
            host.Open();
        }
    }
}
