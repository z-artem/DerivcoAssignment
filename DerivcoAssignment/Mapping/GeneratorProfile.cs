using AutoMapper;
using DerivcoAssignment.Core.Dtos;
using DerivcoAssignment.Web.Helpers;
using DerivcoAssignment.Web.ViewModels;
using Newtonsoft.Json;

namespace DerivcoAssignment.Web.Mapping
{
    public class GeneratorProfile : Profile
    {
        public GeneratorProfile()
        {
            CreateMap<FibonacciResultDto, FibonacciResponseViewModel>()
                .ForMember(dest => dest.FibonacciNumbers, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.FibonacciNumbers, new FibonacciResponseConverter())));
        }
    }
}
