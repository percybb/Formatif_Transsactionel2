using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class Enseignant
    {
        
        [Key]
        [Required]
        [MaxLength(50)]
        public String Ens_Id { get; set; }
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
    
       

    }
}