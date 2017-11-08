using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   [Table("tblKisiler")] 
    public class Kisi
    {
        [Key]
        public int KisiID { get; set; }
        [Required]
        public string AdSoyad { get; set; }
    }
}
