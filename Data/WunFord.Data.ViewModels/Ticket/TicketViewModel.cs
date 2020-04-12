namespace WunFord.Data.ViewModels.Ticket
{
    using System;
    using WunFord.Data.Models;

    public class TicketViewModel : IEquatable<TicketViewModel>
    {
        public int Id { get; set; }

        public string TicketKey { get; set; }

        public string TicketLabel { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime DispatchDate { get; set; }

        public int? Volume { get; set; }

        public string Description { get; set; }

        public string CheckInOne { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }


        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string SearchString { get; set; }

        public bool Equals(TicketViewModel other)
        {
            return this.Id == other.Id && this.TicketKey == other.TicketKey
                && this.TicketLabel == other.TicketLabel && this.DispatchDate == other.DispatchDate
                && this.CreatedDate == other.CreatedDate && this.Volume == other.Volume
                && this.StatusId == other.StatusId && this.UserId == other.UserId;
        }
    }
}
