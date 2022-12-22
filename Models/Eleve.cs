using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class Eleve
    {
        [Key]
        [Required]
        [MaxLength(50)]
        public String Eleve_Id { get; set; }
        [Required]
        [MaxLength(50)]
        public String Nom { get; set; }
        [Required]
        [MaxLength(50)]
        public String Prenom { get; set; }
        [Required]
        [MaxLength(50)]
        public String Email { get; set; }
        [Required]
        [MaxLength(50)]
        public String Psw { get; set; }
        [DataType(DataType.Date)]
        public String Photo { get; set; }
        [Required]
        [MaxLength(50)]
        public String Status { get; set; }
        public String Supervision { get; set; }
    }
}