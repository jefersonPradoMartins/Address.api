using Address.Domain.Entities;
using Address.Service.Dto.Request.V1;
using Address.Service.Dto.Request.V2;
using AutoMapper;

namespace Address.api.MappingProfiles
{
    public class ResponseToDomain : Profile
    {

        public ResponseToDomain()
        {
            try
            {
                CreateMap<Domain.Entities.Address, Service.Dto.Request.V1.CreateAddress>().ReverseMap();

                CreateMap<Domain.Entities.Address, Service.Dto.Request.V2.CreateAddress>().ReverseMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
