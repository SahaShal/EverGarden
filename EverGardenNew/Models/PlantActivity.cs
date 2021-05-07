using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverGardenNew.Models
{
    public class PlantActivity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string NeededTools { get; set; }
        public int PlantID { get; set; }
        public Plant Plant { get; set; }
}
}
