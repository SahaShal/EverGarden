using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EverGarden.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string CategoryWhere { get; set; }
        public string CategoryEdible { get; set; }
        public string Info { get; set; }
        public string Accecories { get; set; }
    }
}
