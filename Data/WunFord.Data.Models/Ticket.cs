namespace WunFord.Data.Models
{
    using System;

    public class Ticket : IEquatable<Ticket>
    {
        public int Id { get; set; }

        public string TicketKey { get; set; }

        public string TicketLabel { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime DispatchDate { get; set; }

        public int? Volume { get; set; }

        public string Description { get; set; }

        public string FirstCheck { get; set; }

        public string SecondCheck { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }


        public string UserId { get; set; }
        public virtual User User { get; set; }

        public bool Equals(Ticket other)
        {
            return this.Id == other.Id && this.TicketKey == other.TicketKey && this.TicketLabel == other.TicketLabel &&
                this.CreatedDate == other.CreatedDate && this.DispatchDate == other.DispatchDate && this.Volume == other.Volume &&
                this.Description == other.Description && this.StatusId == other.StatusId &&
                this.FirstCheck == other.FirstCheck && this.SecondCheck == other.SecondCheck;
        }
    }
}
