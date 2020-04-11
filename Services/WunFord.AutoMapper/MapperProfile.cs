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
                .ForMember(dvm => dvm.Status, d => d.MapFrom(x => x.Status.Name)).ReverseMap();

            this.CreateMap<Ticket, AddTicketViewModel>().ReverseMap();
        }
    }
}
