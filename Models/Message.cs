using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace formatif.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Message_Id { get; set; }
        public String Remite_Id { get; set; }
        public String Destino_Id { get; set; }

        public String Msg { get; set; }
        public DateTime Date { get; set; }

        public int MsgPere{ get; set; }
        
    }
}