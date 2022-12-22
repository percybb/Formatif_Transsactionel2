using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class ModMessage
    {
        public String nom_log { set; get; }
        public List<Eleve> LstELeves { get; set; }
        public List<Message> LstMessage { get; set; }
    }



}