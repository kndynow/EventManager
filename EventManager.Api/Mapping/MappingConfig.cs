using System.Diagnostics;
using EventManager.Api.Models;
using EventManager.Core.Models;
using Mapster;

namespace EventManager.Api.Mapping;

//Configure Mapster to map between Event Core model and API's EventDto
public class MappingConfig
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        //Event models to DTO mapping
        config.NewConfig<CreateEventDto, Event>();
        config.NewConfig<UpdateEventDto, Event>();
        config.NewConfig<PatchEventDto, Event>().IgnoreNullValues(true);
        config.NewConfig<Event, EventResponseDto>();
        //Booking models to DTO mapping
        config.NewConfig<CreateBookingDto, Booking>();
        config.NewConfig<UpdateBookingDto, Booking>();
        config.NewConfig<PatchBookingDto, Booking>().IgnoreNullValues(true);
        config.NewConfig<Booking, BookingResponseDto>();
        //User models to DTO mapping
        config.NewConfig<UpdateUserDto, User>();
        config.NewConfig<PatchUserDto, User>().IgnoreNullValues(true);
        config.NewConfig<User, UserResponseDto>();
    }
}
