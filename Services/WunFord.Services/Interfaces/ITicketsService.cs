namespace WunFord.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using WunFord.Data.ViewModels.Ticket;

    public interface ITicketsService
    {
        IEnumerable<TicketViewModel> GetAllTickets();

        void AddTicket(string ticketKey, string description, string ticketLabel, string userId, int volume, DateTime dispatchDate, int statusId, string firstcheck, string secondcheck);

        TicketViewModel GetTicketById(int ticketId);

        TicketViewModel UpdateTicket(TicketViewModel model);

        void DeleteTicket(TicketViewModel model);

        TicketViewModel UpdateTicketFirstCheck(TicketViewModel model);

        TicketViewModel UpdateTicketSecondCheck(TicketViewModel model);
    }
}
