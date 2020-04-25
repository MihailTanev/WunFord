namespace WunFord.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public void AddTicket(string ticketKey, string description, string ticketLabel, string userId, int volume, DateTime dispatchDate, int statusId, string firstcheck,string secondcheck)
        {
            Ticket post = new Ticket
            {
                TicketKey = ticketKey,
                TicketLabel = ticketLabel,
                Description = description,
                Volume = volume,
                UserId = userId,
                DispatchDate = dispatchDate,
                StatusId = statusId,
                CreatedDate = DateTime.Now,
                FirstCheck = firstcheck,
                SecondCheck = secondcheck
            };

            this.Context.Tickets.Add(post);

            this.Context.SaveChanges();

        }

        public void DeleteTicket(TicketViewModel model)
        {
            var ticket = this.Context
               .Tickets
               .FirstOrDefault(d => d.Id == model.Id);

            if (ticket != null)
            {
                this.Context.Tickets.Remove(ticket);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<TicketViewModel> GetAllTickets()
        {
            var ticket = this.Context
                            .Tickets
                            .OrderBy(v => v.DispatchDate)
                            .AsQueryable();

            var ticketViewModel = this.Mapper.Map<IQueryable<Ticket>, IEnumerable<TicketViewModel>>(ticket);
            return ticketViewModel;
        }       

        public TicketViewModel GetTicketById(int ticketId)
        {
            var ticket = this.Context
                           .Tickets
                           .FirstOrDefault(d => d.Id == ticketId);

            var ticketViewModel = this.Mapper.Map<TicketViewModel>(ticket);

            return ticketViewModel;
        }

        public TicketViewModel UpdateTicket(TicketViewModel model)
        {
            var ticket = this.Context
               .Tickets
               .FirstOrDefault(s => s.Id == model.Id);

            if (ticket == null)
            {
                return null;
            }

            ticket.TicketKey = model.TicketKey;
            ticket.TicketLabel = model.TicketLabel;
            ticket.Description = model.Description;
            ticket.StatusId = model.StatusId;
            ticket.Volume = model.Volume;
            ticket.DispatchDate = model.DispatchDate;
            ticket.FirstCheck = model.FirstCheck;

            this.Context.SaveChanges();

            var ticketViewModel = this.Mapper.Map<TicketViewModel>(ticket);

            return ticketViewModel;
        }

        public TicketViewModel UpdateTicketFirstCheck(TicketViewModel model)
        {
            var ticket = this.Context
               .Tickets
               .FirstOrDefault(s => s.Id == model.Id);

            if (ticket == null)
            {
                return null;
            }

            ticket.FirstCheck = model.FirstCheck;

            this.Context.SaveChanges();

            var ticketViewModel = this.Mapper.Map<TicketViewModel>(ticket);

            return ticketViewModel;
        }

        public TicketViewModel UpdateTicketSecondCheck(TicketViewModel model)
        {
            var ticket = this.Context
               .Tickets
               .FirstOrDefault(s => s.Id == model.Id);

            if (ticket == null)
            {
                return null;
            }

            ticket.SecondCheck = model.SecondCheck;

            this.Context.SaveChanges();

            var ticketViewModel = this.Mapper.Map<TicketViewModel>(ticket);

            return ticketViewModel;
        }
    }
}
