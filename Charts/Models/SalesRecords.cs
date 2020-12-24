using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charts.Models
{
    public class SalesRecords
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int Electronics { get; set; }
        public int BookAndMedia { get; set; }
        public int HomeAndKitchen { get; set; }
    }
}
