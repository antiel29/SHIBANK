using Microsoft.EntityFrameworkCore;

namespace SHIBANK.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }
        

    }
}
