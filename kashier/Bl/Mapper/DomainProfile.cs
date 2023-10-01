using AutoMapper;
using kashier.DAL.Entities;
using kashier.Models;

namespace kashier.BL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            CreateMap<Tickets,TicketsVm >();
            CreateMap<TicketsVm, Tickets>();
        }
    }
}
