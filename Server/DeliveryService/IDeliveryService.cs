using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DeliveryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISenderService
    {
        [OperationContract]
        int AddSender(string cpr, string firstName, string lastName, string phoneNumber,string email, string address, string zipCode, string city);
        [OperationContract]
        void ClearDB();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
