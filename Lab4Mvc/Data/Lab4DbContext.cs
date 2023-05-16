using Lab4Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4Mvc.Data
{
    public class Lab4DbContext : DbContext
    {
        public Lab4DbContext(DbContextOptions<Lab4DbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CustomerBookList> CuBoLists { get; set; }
    }
}
