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
        }

        public DbSet<Currency> currencies { get; set; }
        public DbSet<History> historicalCurrencies { get; set; }
    }
}
