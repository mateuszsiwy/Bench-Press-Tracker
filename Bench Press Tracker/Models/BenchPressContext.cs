using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bench_Press_Tracker.Models
{
    public class BenchPressContext : DbContext
    {
        public DbSet<BenchPressItem> BenchPressItems { get; set; }

        public BenchPressContext(DbContextOptions<BenchPressContext> options): base(options) { }
    } 
}
