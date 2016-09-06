using System;

namespace BusinessEntities.Entities
{
    public partial class FriendRequest
    {
        public int IdRequester { get; set; }
        public int IdReceiver { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte State { get; set; }

        public virtual User Requester { get; set; }
        public virtual User Receiver { get; set; }
    }
}
