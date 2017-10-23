using System;
using System.Collections.Generic;
using System.Text;

namespace Enumaretion
{

    public enum AdminSession
    {
        CurrentUserId = 1,
        CurrentUserName = 2
    }

    
    public enum Month
    {
        Select = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }


    public enum SanctionUserType
    {
        BAMELCO = 1,
        DeputyCAMELCO = 2,
        CAMELCO = 3,
        Standard = 4
    }

    public enum ReferStatus
    {
        Request = 1,
        Recommend = 2,
        Approve = 3,
        Refuse = 4
    }
    
   
}
