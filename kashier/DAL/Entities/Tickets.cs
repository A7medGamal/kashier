using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace kashier.DAL.Entities
{
    [Table("Tickets")]
    public class Tickets
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FistName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email {get;set;}
     
        public int Phone { get; set; }
         public string Company { get; set; }
        [Range(1,50)]
        public int NumOfTickets { get; set; }
        public decimal TotalAmount { get; set; } 
        
        public bool IsPaid { get; set; }
        [AllowNull]
        public string TransactionId { get; set; }


    }
}
