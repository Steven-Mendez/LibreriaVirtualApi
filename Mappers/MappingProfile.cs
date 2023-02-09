using AutoMapper;
using LibreriaVirtualApi.Dtos;
using LibreriaVirtualApi.Models;

namespace LibreriaVirtualApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookRequestDto, Book>().ReverseMap();
            CreateMap<Book, BookResponseDto>().ReverseMap();
        }
    }
}
