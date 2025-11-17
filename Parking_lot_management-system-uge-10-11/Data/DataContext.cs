using Microsoft.EntityFrameworkCore;
using Parking_lot_management_system_uge_10_11.Models;

namespace Parking_lot_management_system_uge_10_11.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {

        }

        public DbSet<Lot> lots { get; set; }
        public DbSet<Lot_History> lot_Histories { get; set; }
        public DbSet<Lot_types> lot_Types{ get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<Parking_Lot_Structur> parking_Lot_Structurs { get; set; }
        public DbSet<User_Types> user_Types { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
