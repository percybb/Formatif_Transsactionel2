using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class ModMessage2
    {
       
            public String nom_log { set; get; }
            public List<Enseignant> LstEnseignant { get; set; }
            public List<Message> LstMessage { get; set; }
      
    }
}