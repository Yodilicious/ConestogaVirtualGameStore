namespace ConestogaVirtualGameStore.Web.Models
{
    using System;

    public class EventRegistration : BaseModel
    {
        public string UserId { get; set; }
        public long EventId { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
