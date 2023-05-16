using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab4Mvc.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; } = 0;


        [Required]
        [MinLength(1), MaxLength(25)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;



        [Required]
        [MinLength(1), MaxLength(45)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;


        [Required]
        [DisplayName("Date when registered")]
        public DateTime RegisteredDate { get; set; } = default!;


        [DisplayName("ID and full name")]
        public string FullName { get { return $"ID #{CustomerId} | {FirstName} {LastName}"; } }
        public virtual ICollection<CustomerBookList>? CuBoLists { get; set; }
    }
}
