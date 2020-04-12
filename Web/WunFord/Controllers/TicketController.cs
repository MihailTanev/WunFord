namespace WunFord.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> userManager;
        private readonly WunFordDbContext context;

        public TicketController(ITicketsService ticketsService, IStatusesService statusesService, UserManager<User> userManager, WunFordDbContext context)
        {
            this.ticketsService = ticketsService;
            this.statusesService = statusesService;
            this.userManager = userManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            var sports = this.ticketsService.GetAllTickets();

            return this.View(sports);
        }

        [Authorize]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();
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

            this.ticketsService.AddTicket(model.TicketKey, model.Description, model.TicketLabel, userId, model.Volume ?? 0, model.DispatchDate, model.StatusId);         

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

        public IActionResult Edit(int ticketId)
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();
            var ticket = this.ticketsService.GetTicketById(ticketId);
            if (ticket == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(ticket);
        }

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

            return this.View();
        }

    }
}
