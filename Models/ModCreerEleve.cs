using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class ModCreerEleve
    {
        public String nom_log { set; get; }
        public Eleve DatEleve { get; set; }
        public List<Eleve> ListEleves { get; set; }
    }
}