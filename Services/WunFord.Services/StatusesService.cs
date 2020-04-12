namespace WunFord.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using WunFord.Data;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Status;
    using WunFord.Services.Interfaces;

    public class StatusesService : BaseService, IStatusesService
    {
        public StatusesService(WunFordDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
           : base(context, mapper, userManager, signInManager)
        {
        }

        public Status AddStatus(AddStatusViewModel model)
        {
            var status = this.Mapper.Map<Status>(model);

            this.Context.Statuses.Add(status);
            this.Context.SaveChanges();

            return status;
        }

        public void DeleteStatus(StatusViewModel model)
        {
            var status = this.Context
                .Statuses
                .FirstOrDefault(d => d.Id == model.Id);

            if (status != null)
            {
                this.Context.Statuses.Remove(status);
                this.Context.SaveChanges();
            }
        }

        public IEnumerable<StatusViewModel> GetAllStatuses()
        {
            var status = this.Context
                           .Statuses
                           .OrderBy(s => s.Name)
                           .AsQueryable();

            var statusViewModel = this.Mapper.Map<IQueryable<Status>, IEnumerable<StatusViewModel>>(status);

            return statusViewModel;
        }
       
        public StatusViewModel GetStatusById(int id)
        {
            var status = this.Context
                .Statuses
                .FirstOrDefault(d => d.Id == id);

            var statusViewModel = this.Mapper.Map<StatusViewModel>(status);

            return statusViewModel;
        }       

        public StatusViewModel UpdateStatus(StatusViewModel model)
        {
            var status = this.Context
                            .Statuses
                            .FirstOrDefault(s => s.Id == model.Id);

            if (status == null)
            {
                return null;
            }

            status.Name = model.Name;
            this.Context.SaveChanges();

            var statusViewModel = this.Mapper.Map<StatusViewModel>(status);

            return statusViewModel;
        }
    }
}
