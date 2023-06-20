using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Domain.Entities
{
    public class Product
    {
        public required string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public required string ManufacturePhone { get; set; }
        public required string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}
