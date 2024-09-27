using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench_Press_Tracker.Models
{
    public class BenchPressItem
    {
        public int Id { get; set; }
        public string SessionStart { get; set; }
        public string SessionEnd { get; set; }
        public string Duration { get; set; }
        public string SetsXReps { get; set; }
        public string Comments { get; set; }
    }
}
