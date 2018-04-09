using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP;

namespace DAL
{
    public class DB_Context : DbContext
    {
        public DB_Context() : base()
        {
            //WHAT IS THIS
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DB_Context>());
        }
        //pointing to the data base
        public DbSet<DBCurrency> currencies { get; set; }
        public DbSet<History> historicalCurrencies { get; set; }
    }
}
