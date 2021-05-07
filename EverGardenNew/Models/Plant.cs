using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverGardenNew.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public int CategoryEdibleID { get; set; }
        public CategoryEdible CategoryEdible { get; set; }
        public int CategoryPlaceID { get; set; }
        public CategoryPlace CategoryPlace { get; set; }
        public string Climate { get; set; }
        public string Watering { get; set; }
        public string ShortDescription { get; set; }
        public string BioDescription { get; set; }
        public string SpreadingArea { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
        public string Tools { get; set; }
        public ICollection<PlantActivity> PlantActivities { get; set; }
    }
}
