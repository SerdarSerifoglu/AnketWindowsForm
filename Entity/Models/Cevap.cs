using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("tblCevaplar")]
    public class Cevap
    {
        [Key]
        public int CevapID { get; set; }
        [ForeignKey("CevabiVerenKisi")]
        public int KisiID { get; set; }
        [ForeignKey("Sorusu")]
        public int SoruID { get; set; }
        [Required]
        public Yanit Yanit { get; set; }
        public virtual Kisi CevabiVerenKisi { get; set; } 
        public virtual Soru Sorusu { get; set; } 
    }
    public enum Yanit
    {
       Hayir,
       Evet
    }
}
