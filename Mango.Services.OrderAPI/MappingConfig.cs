using AutoMapper;

namespace Mango.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
                //config.CreateMap<OrderHeaderDto, OrderHeader>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
