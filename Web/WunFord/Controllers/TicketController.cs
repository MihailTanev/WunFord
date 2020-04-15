namespace WunFord.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using WunFord.Common;
    using WunFord.Data;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Ticket;
    using WunFord.Services.Interfaces;

    public class TicketController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IStatusesService statusesService;
        private readonly IUsersService usersService;
        private readonly UserManager<User> userManager;
        private readonly WunFordDbContext  context;

        public TicketController(WunFordDbContext context, ITicketsService ticketsService, IStatusesService statusesService, UserManager<User> userManager, IUsersService usersService)
        {
            this.ticketsService = ticketsService;
            this.statusesService = statusesService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.context = context;
        }

        public IActionResult Index(string searchString)
        {
            var tickets = this.ticketsService.GetAllTickets();

            if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(s => s.TicketKey.ToLower().Contains(searchString.ToLower()));
            }

            return this.View(tickets);
        }

        [Authorize]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();
            //this.ViewData[GlobalConstants.Users] = this.usersService.GetAllUsers();
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTicketViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(User);

            this.ticketsService.AddTicket(model.TicketKey, model.Description, model.TicketLabel, userId, model.Volume ?? 0, model.DispatchDate, model.StatusId, model.FirstCheck, model.SecondCheck);         

            return this.RedirectToAction(nameof(Index));
        }

        [Route("Ticket/{ticketId}/{**title}")]
        public IActionResult Details(int ticketId)
        {
            var ticket = this.ticketsService.GetTicketById(ticketId);
            if (ticket == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(ticket);
        }

        [Authorize]
        public IActionResult FirstCheck(int Id)
        {
            var ticket = this.ticketsService.GetTicketById(Id);
            if (ticket == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(ticket);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FirstCheck(TicketViewModel model)
        {          

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var ticket = this.ticketsService.UpdateTicketFirstCheck(model);

            if (ticket == null)
            {
                return this.View(model);
            }
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult SecondCheck(int Id)
        {
            var ticket = this.ticketsService.GetTicketById(Id);
            if (ticket == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(ticket);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SecondCheck(TicketViewModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var ticket = this.ticketsService.UpdateTicketSecondCheck(model);

            if (ticket == null)
            {
                return this.View(model);
            }
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();

            var ticket = this.ticketsService.GetTicketById(Id);

            if (ticket == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(ticket);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(TicketViewModel model)
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var ticket = this.ticketsService.UpdateTicket(model);

            if (ticket == null)
            {
                return this.View(model);
            }
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            var venue = this.ticketsService.GetTicketById(Id);
            if (venue == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(venue);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(TicketViewModel model)
        {
            this.ticketsService.DeleteTicket(model);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
