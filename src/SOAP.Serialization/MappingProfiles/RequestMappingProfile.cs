using AutoMapper;
using SOAP.Models;

namespace SOAP.Serialization.MappingProfiles
{
    public class RequestMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Envelope, Request>()
                .ForMember(req => req.MethodName, opt => opt.MapFrom(env => env.Body.MethodName))
                .ForMember(req => req.Parameters, opt => opt.MapFrom(env => env.Body.Parameters));

            CreateMap<Response, Envelope>()
                .ForMember(env => env.Body, opt => opt.MapFrom(res => res));

            //CreateMap<Response, Body>()
            //    .ForMember(b => b.MethodName, opt => opt.MapFrom(res => res.Name))
            //    .ForMember(b => b.Parameters, opt => opt.MapFrom(res => res.Content));
        }
    }
}
