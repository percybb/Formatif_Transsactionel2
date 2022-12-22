using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public int Reserva_Id { get; set; }
        public String Eleve_Id { get; set; }    
        
        public int Jour { get; set; }
        public int Hour { get; set; }

        public String Status { get; set; }
        public String Supervision { get; set; }
        public String Anotation { get; set; }
    }
}