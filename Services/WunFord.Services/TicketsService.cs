namespace WunFord.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WunFord.Data;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Ticket;
    using WunFord.Services.Interfaces;

    public class TicketsService : BaseService, ITicketsService
    {
        public TicketsService(WunFordDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
          : base(context, mapper, userManager, signInManager)
        {
        }
        public Ticket AddTicket(AddTicketViewModel model, string id)
        {
            Ticket ticket = this.Mapper.Map<Ticket>(model);
            model.UserId = id;
            this.Context.Tickets.Add(ticket);
            this.Context.SaveChanges();

            return ticket;
        }

        public void DeleteVenue(TicketViewModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketViewModel> GetAllTickets()
        {
            var ticket = this.Context
                            .Tickets
                            .OrderBy(v => v.TicketLabel)
                            .AsQueryable();

            var ticketViewModel = this.Mapper.Map<IQueryable<Ticket>, IEnumerable<TicketViewModel>>(ticket);
            return ticketViewModel;
        }

        public TicketViewModel GetVenueById(int id)
        {
            throw new NotImplementedException();
        }

        public TicketViewModel UpdateVenue(TicketViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
