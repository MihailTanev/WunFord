namespace WunFord.Data.Models
{
    using System;

    public class Ticket
    {
        public int Id { get; set; }

        public string TicketKey { get; set; }

        public string TicketLabel { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DispatchDate { get; set; }

        public int? Volume { get; set; }

        public string Description { get; set; }

        public string CheckInOne { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
    }
}
