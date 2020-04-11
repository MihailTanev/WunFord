namespace WunFord.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using WunFord.Common;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Ticket;
    using WunFord.Services.Interfaces;

    public class TicketController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IStatusesService statusesService;
        private readonly UserManager<User> userManager;

        public TicketController(ITicketsService ticketsService, IStatusesService statusesService, UserManager<User> userManager)
        {
            this.ticketsService = ticketsService;
            this.statusesService = statusesService;
            this.userManager = userManager;
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
        public IActionResult Add(AddTicketViewModel model, string userId)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Statuses] = this.statusesService.GetAllStatuses();
                return this.View(model);
            }

            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);


            this.ticketsService.AddTicket(model, user);

            return this.RedirectToAction("~/");
        }


    }
}
