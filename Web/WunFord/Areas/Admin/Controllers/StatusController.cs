namespace WunFord.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WunFord.Common;
    using WunFord.Data.ViewModels.Status;
    using WunFord.Services.Interfaces;

    [Area(AreaConstants.Admin)]
    public class StatusController : Controller
    {
        private readonly IStatusesService statusService;

        public StatusController(IStatusesService statusService)
        {
            this.statusService = statusService;
        }

        public IActionResult Index()
        {
            var statuses = this.statusService.GetAllStatuses();

            return this.View(statuses);
        }

        [Authorize]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusService.GetAllStatuses();
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddStatusViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Statuses] = this.statusService.GetAllStatuses();
                return this.View(model);
            }

            this.statusService.AddStatus(model);

            return this.RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusService.GetAllStatuses();
            var status = this.statusService.GetStatusById(id);
            if (status == null)
            {
                return BadRequest();
            }

            return this.View(status);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(StatusViewModel model)
        {
            this.ViewData[GlobalConstants.Statuses] = this.statusService.GetAllStatuses();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var status = this.statusService.UpdateStatus(model);
            if (status == null)
            {
                return this.View(model);
            }

            return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var status = this.statusService.GetStatusById(id);
            if (status == null)
            {
                return BadRequest();
            }

            return this.View(status);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var status = this.statusService.GetStatusById(id);
            if (status == null)
            {
                return BadRequest();
            }

            return this.View(status);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(StatusViewModel model)
        {
            this.statusService.DeleteStatus(model);
            return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
        }
    }
}