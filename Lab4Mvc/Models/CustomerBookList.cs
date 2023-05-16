using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab4Mvc.Models
{
    public class CustomerBookList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CuBoListId { get; set; } = 0;


        [ForeignKey("Customers")]
        [DisplayName("Customer")]
        public int FK_CustomerId { get; set; }
        public virtual Customer? Customers { get; set; }



        [ForeignKey("Books")]
        [DisplayName("Book")]
        public int FK_BookId { get; set; }
        public virtual Book? Books { get; set; }


        [Required]
        [DisplayName("Which date the booking starts")]
        public DateTime BookStart { get; set; }


        [Required]
        [DisplayName("Which date the booking ends")]
        public DateTime BookEnd { get; set; }


        public bool Retrieved { get; set; }
        public bool Returned { get; set; }
    }
}
