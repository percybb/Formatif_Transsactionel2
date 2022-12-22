using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class ModAccueil
    {

        public String nom_log { set; get; }
        public List<Reservation> Reservations { set; get; }

    }
}