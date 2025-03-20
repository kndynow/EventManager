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
        config.NewConfig<CreateEventDto, Event>();
        config.NewConfig<UpdateEventDto, Event>();
        config.NewConfig<PatchEventDto, Event>().IgnoreNullValues(true);
        config.NewConfig<Event, EventResponseDto>();
    }
}
