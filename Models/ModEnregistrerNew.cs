using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class ModEnregistrerNew
    {
        public int Jour { set; get; }
        public int Hour { set; get; }
        public List<Eleve> ListELeves { set; get; }
    }
}