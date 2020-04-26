namespace WunFord.Data.ViewModels.User
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class ChangeUserRoleViewModel
    {
        public UserViewModel User { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
