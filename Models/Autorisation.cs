using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class Autorisation
    {
        [Key]
        [Required]
        public int Auto_Id { get; set; }
        public int Eleve_Id { get; set; }
        public int Status { get; set; }
       
    }
}