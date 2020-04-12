namespace WunFord.AutoMapper
{
    using global::AutoMapper;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.Ticket;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Tickets
            this.CreateMap<Ticket, TicketViewModel>()
                .ForMember(dvm => dvm.Status, d => d.MapFrom(x => x.Status.Name))
                .ForMember(dvm => dvm.FirstName, d => d.MapFrom(x => x.User.FirstName))
                .ForMember(dvm => dvm.LastName, d => d.MapFrom(x => x.User.LastName)).ReverseMap();

            this.CreateMap<Ticket, AddTicketViewModel>().ReverseMap();
        }
    }
}
