using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Enums
{
    public enum FriendRequestStatus : byte
    {
        RequestPending = 1,
        RequestAccepted = 2,
        RequestRejected = 3
    }
}
