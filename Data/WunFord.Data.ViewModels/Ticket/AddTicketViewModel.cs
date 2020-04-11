namespace WunFord.Data.ViewModels.Ticket
{
    using System;
    using WunFord.Data.Models;

    public class AddTicketViewModel
    {
        public string TicketKey { get; set; }

        public string TicketLabel { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DispatchDate { get; set; }

        public int? Volume { get; set; }

        public int StatusId { get; set; }

        public string CheckInOne { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
