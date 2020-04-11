using System;
using System.Collections.Generic;
using System.Text;
using WunFord.Data.Models;
using WunFord.Data.ViewModels.Ticket;

namespace WunFord.Services.Interfaces
{
    public interface ITicketsService
    {
        IEnumerable<TicketViewModel> GetAllTickets();

        Ticket AddTicket(AddTicketViewModel model, string id);

        TicketViewModel GetVenueById(int id);

        TicketViewModel UpdateVenue(TicketViewModel model);

        void DeleteVenue(TicketViewModel model);
    }
}
