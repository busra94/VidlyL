using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            //Mapper.CreateMap<MembershipTypeDto, MembershipType>();

            /*
             * The exception is thrown when AutoMapper attempts to set the Id of movie:
             * customer.Id	=	customerDto.Id;	
            
             * Id is the key property for the Movie class, and a key property should not be changed.
             * That’s why we get this exception. To resolve this, you need to tell AutoMapper to ignore
             * Id during mapping of a MovieDto to Movie. 
             */
        }

    }
}