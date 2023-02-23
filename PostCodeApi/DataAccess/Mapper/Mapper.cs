using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataAccess.Model;

namespace DataAccess.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PostCode, Result>().ReverseMap();
        }
    }
}
