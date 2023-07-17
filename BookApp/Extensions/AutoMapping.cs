using AutoMapper;
using BookApp.Models;
using BookApp.DTos;

namespace BookApp.Extensions
{
    public class AutoMapping:Profile
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>

            {
                cfg.CreateMap<BookDto, Book>();

                
                cfg.CreateMap<ProductDtos, Product>();
            });

            return config;
        }
    }
}
