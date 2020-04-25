namespace WunFord.Data.ViewModels.Ticket
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WunFord.Common;

    public class TicketViewModel : IEquatable<TicketViewModel>
    {
        public int Id { get; set; }

        [Display(Name = TicketConstants.TicketKey)]
        public string TicketKey { get; set; }

        [Display(Name = TicketConstants.TicketLabel)]
        public string TicketLabel { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = TicketConstants.DispatchDate)]
        public DateTime DispatchDate { get; set; }

        public int? Volume { get; set; }

        public string Description { get; set; }

        [Display(Name = TicketConstants.SecondCheck)]
        public string SecondCheck { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public string Status { get; set; }

        public string UserId { get; set; }

        [Display(Name = TicketConstants.FirstCheck)]
        public string FirstCheck { get; set; }

        public IEnumerable<SelectListItem> Specialists { get; set; }


        public bool Equals(TicketViewModel other)
        {
            return this.Id == other.Id && this.TicketKey == other.TicketKey
                && this.TicketLabel == other.TicketLabel && this.DispatchDate == other.DispatchDate
                && this.CreatedDate == other.CreatedDate && this.Volume == other.Volume
                && this.StatusId == other.StatusId && this.UserId == other.UserId && this.FirstCheck == other.FirstCheck;
        }
    }
}
