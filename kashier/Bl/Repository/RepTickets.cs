using kashier.BL.Interface;
using kashier.DAL.Database;
using kashier.Models;
using System.Security.Cryptography;
using System.Text;
using kashier.DAL.Entities;
using AutoMapper;

namespace kashier.BL.Repository
{
    public class RepTickets : ITicketRep
    {
        //private DbContainer db = new DbContainer();
private readonly DBContainer db;
private readonly IMapper mapper;
  public RepTickets(DBContainer _db ,IMapper _Mapper)
        {
            this.db = _db;
            this.mapper = _Mapper;
        }

        public int Add(TicketsVm tickets)
        {
           var data =mapper.Map<Tickets>(tickets);


            db.Tickets.Add(data);
          
           // var lastticket= db.Tickets.Last();
            db.SaveChanges();
            var id = data.Id;
            return id;
        }

        public TicketsVm GetByID(int id)
        {
            var data = db.Tickets.Where(a => a.Id == id)
                                     .Select(a => new TicketsVm { Id = a.Id,FistName=a.FistName,LastName=a.LastName,Company=a.Company,Email=a.Email,Phone=a.Phone,NumOfTickets=a.NumOfTickets,IsPaid=a.IsPaid,TotalAmount=a.TotalAmount,TransactionId=a.TransactionId })
                                     .FirstOrDefault();
            return data;
        }
        public void Edit(TicketsVm ticketsVm)
        {
            //old mapping without auto maper
            //var OldData = db.Department.Find(departmentsVM.Id);
            //OldData.DepatmentName= departmentsVM.DepatmentName;
            //OldData.DepartmentCode= departmentsVM.DepartmentCode;


            //mapping with auto mapper
            var data = mapper.Map<Tickets>(ticketsVm);
            db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }
    }
}
