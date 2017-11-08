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
        [ForeignKey("CevabiVerenKisi")]// Bu Fk'nın dolduracağı virtual Navigation Property CevabiVerenKisi'dir.
        public int KisiID { get; set; }
        [ForeignKey("Sorusu")]
        public int SoruID { get; set; }
        [Required]
        public Yanit Yanit { get; set; }
        public virtual Kisi CevabiVerenKisi { get; set; } //Yanıtı kimin verdiğini görebilmek için
        public virtual Soru Sorusu { get; set; } //Hangi sorunun yanıtı olabildiğini görebilmek için



    }

    public enum Yanit
    {
       Hayir,
       Evet
    }
}
