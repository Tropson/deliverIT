using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DeliveryServiceLibrary
{
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        UserModel[] GetAllUsers();
        [OperationContract]
        ApplicationModel[] GetAllApplications();
        [OperationContract]
        int DeleteApplication(ApplicationModel application, bool deletePerson);
        [OperationContract]
        int AddCourier(UserModel courier);

    }
}
