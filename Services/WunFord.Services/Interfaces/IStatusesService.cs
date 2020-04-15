namespace WunFord.Services.Interfaces
{
    using System.Collections.Generic;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Status;

    public interface IStatusesService
    {
        IEnumerable<StatusViewModel> GetAllStatuses();

        Status AddStatus(AddStatusViewModel model);

        StatusViewModel GetStatusById(int id);

        StatusViewModel UpdateStatus(StatusViewModel model);

        void DeleteStatus(StatusViewModel model);

    }
}
