using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClientMVC.Models
{
    [DataContract(Name = "AccountType")]
    public enum AccountTypeEnum
    {
        [EnumMember]
        SENDER =1,
        [EnumMember]
        COURIER =2,
        [EnumMember]
        ADMIN =3
    }
}