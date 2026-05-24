using AutoMapper;
using api.Models;
using api.Dtos.Stock;
using api.Dtos.Comment;

namespace api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CreateCommentDto>();
        }
    }
}