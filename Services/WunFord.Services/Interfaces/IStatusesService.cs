using System;
using System.Collections.Generic;
using System.Text;
using WunFord.Data.Models;
using WunFord.Data.ViewModels.Status;

namespace WunFord.Services.Interfaces
{
    public interface IStatusesService
    {
        IEnumerable<StatusViewModel> GetAllStatuses();

        Status AddStatus(AddStatusViewModel model);

        StatusViewModel GetStatusById(int id);

        StatusViewModel UpdateStatus(StatusViewModel model);

        void DeleteStatus(StatusViewModel model);
    }
}
