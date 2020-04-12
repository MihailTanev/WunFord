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

        void AddTicket(string ticketKey, string description, string ticketLabel, string userId, int volume, DateTime dispatchDate, int statusId);

        TicketViewModel GetTicketById(int ticketId);

        TicketViewModel UpdateTicket(TicketViewModel model);

        void DeleteVenue(TicketViewModel model);
    }
}
