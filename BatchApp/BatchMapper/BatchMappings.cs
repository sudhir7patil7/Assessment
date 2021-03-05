using AutoMapper;
using BatchAPI.Models;
using BatchAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchAPI.BatchMapper
{
    public class BatchMappings: Profile
    {
        public BatchMappings()
        {
           
            CreateMap<Batch, BatchDtos>().ReverseMap();
        }
    }
}
