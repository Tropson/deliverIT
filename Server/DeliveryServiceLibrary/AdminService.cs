using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net;

namespace DeliveryServiceLibrary
{
    [ServiceBehavior(
        Name = "AdminService",
        InstanceContextMode = InstanceContextMode.Single)]
    class AdminService : IAdminService
    {
        public int AddCourier(UserModel courier)
        {
            return new SenderService().AddCourier(courier);
        }

        public int DeleteApplication(ApplicationModel application, bool deletePerson)
        {
            return new SenderService().DeleteApplication(application, deletePerson);
        }

        public ApplicationModel[] GetAllApplications()
        {
            return new SenderService().GetAllApplications();
        }

        public UserModel[] GetAllUsers()
        {
            return new SenderService().GetAllUsers();
        }
    }
}
