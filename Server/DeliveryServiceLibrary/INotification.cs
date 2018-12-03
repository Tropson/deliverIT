using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DeliveryServiceLibrary
{
    [ServiceContract(CallbackContract =typeof(INotificationCallback))]
    public interface INotification
    {
    }
    [ServiceContract]
    public interface INotificationCallback
    {
        [OperationContract(IsOneWay = true)]
        {}
    }
}
