using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverGardenNew.Models
{
    public class CategoryEdible
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Plant> Plants { get; set; }
    }
}
