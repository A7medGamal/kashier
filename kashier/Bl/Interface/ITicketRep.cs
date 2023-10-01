using kashier.DAL.Entities;
using kashier.Models;

namespace kashier.BL.Interface
{
    public interface ITicketRep
    {
         
         int Add(TicketsVm tickets);
         TicketsVm GetByID(int id);
        void Edit(TicketsVm ticketsVm);
    }
}
