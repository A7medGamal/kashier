
namespace kashier.Models
{
    public class TicketsVm
    {
     
        public int Id { get; set; }
      
        public string FistName { get; set; }
      
        public string LastName { get; set; }
     
        public string Email {get;set;}
     
        public int Phone { get; set; }
        public string Company { get; set; }
        public int NumOfTickets { get; set; }
        public decimal TotalAmount { get; set; }
         public bool IsPaid { get; set; }
        public string TransactionId { get; set; }

    }
}