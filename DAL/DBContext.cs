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
            //Database initialization as strategies in EF
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DB_Context>());

            /*If we want to turn off the database initializer for the app.*/
            //Database.SetInitializer<DB_Context>(null);
        }

        //pointing to the data base
        public DbSet<DBCurrency> currencies { get; set; }
        public DbSet<History> historicalCurrencies { get; set; }
    }
}
