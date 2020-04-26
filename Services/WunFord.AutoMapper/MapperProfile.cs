namespace WunFord.AutoMapper
{
    using global::AutoMapper;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Status;
    using WunFord.Data.ViewModels.Ticket;
    using WunFord.Data.ViewModels.User;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Tickets
            this.CreateMap<Ticket, TicketViewModel>()
                .ForMember(dvm => dvm.Status, d => d.MapFrom(x => x.Status.Name))
                .ForMember(dvm => dvm.FirstName, d => d.MapFrom(x => x.User.FirstName))
                //.ForMember(dvm => dvm.FirstCheck, d => d.MapFrom(x => x.User.FirstName))
                .ForMember(dvm => dvm.LastName, d => d.MapFrom(x => x.User.LastName)).ReverseMap();

            this.CreateMap<Ticket, AddTicketViewModel>().ReverseMap();

            this.CreateMap<Status, AddStatusViewModel>().ReverseMap();

            this.CreateMap<User, UserViewModel>().ReverseMap();
            this.CreateMap<User, ChangeUserRoleViewModel>().ReverseMap();
            this.CreateMap<User, RoleViewModel>().ReverseMap();
            this.CreateMap<User, ChangePasswordViewModel>().ReverseMap();
            this.CreateMap<User, DeleteUserViewModel>().ReverseMap();
            this.CreateMap<User, EditUserViewModel>().ReverseMap();
            this.CreateMap<User, UserRoleViewModel>().ReverseMap();

        }
    }
}
