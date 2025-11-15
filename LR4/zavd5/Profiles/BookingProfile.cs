using AutoMapper;
using MyWebApi.Models;

namespace MyWebApi.Profiles
{
    public class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.Details, o => o.MapFrom(s => s.ServiceDetails));

            CreateMap<BookingDto, Booking>()
                .ForMember(d => d.ServiceDetails, o => o.MapFrom(s => s.Details));

        }
    }
}
