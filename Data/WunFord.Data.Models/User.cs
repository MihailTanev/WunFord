namespace WunFord.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Status> Statuses { get; set; }
    }
}
