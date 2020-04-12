namespace WunFord.Data.ViewModels.Ticket
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WunFord.Common;
    using WunFord.Data.Models;

    public class AddTicketViewModel
    {
        [Display(Name = TicketConstants.TicketKey)]
        public string TicketKey { get; set; }

        [Display(Name = TicketConstants.TicketLabel)]
        public string TicketLabel { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = TicketConstants.DispatchDate)]
        public DateTime DispatchDate { get; set; } = DateTime.UtcNow.Date.Add(new TimeSpan(23, 59, 0));

        public int? Volume { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public string CheckInOne { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
