using AutoMapper;
using WebAPI.Dto;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<Comment, CommentDto>();


            CreateMap<StockDto, Stock>();
            CreateMap<CommentDto, Comment>();

        }
    }
}
