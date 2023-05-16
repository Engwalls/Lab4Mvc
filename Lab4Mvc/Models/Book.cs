using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4Mvc.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; } = 0;

        [Required]
        [MinLength(1), MaxLength(50)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(250)]
        public string Description { get; set; } = default!;

        [Required]
        [MinLength(1), MaxLength(75)]
        public string Author { get; set; } = default!;

        [Required]
        public DateTime Published { get; set; } = default!;

        public virtual ICollection<CustomerBookList>? CuBoLists { get; set; }
    }
}
