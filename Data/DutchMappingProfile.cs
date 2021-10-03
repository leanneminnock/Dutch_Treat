using AutoMapper;
using Dutch_Treat.Data.Entities;
using Dutch_Treat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dutch_Treat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o =>o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap().ForMember(m => m.Product, opt => opt.Ignore());
        }
    }
}
