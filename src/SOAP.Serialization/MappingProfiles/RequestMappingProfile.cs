using AutoMapper;
using SOAP.Models;

namespace SOAP.Serialization.MappingProfiles
{
    public class RequestMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Envelope, Request>();
        }
    }
}
