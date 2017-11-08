using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("tblSorular")]
    public class Soru
    {
        [Key]
        public int SoruID { get; set; }
        [Required] //SoruCumlesi doldurulmak zorunda olunan bir alan olması için yazarız.
        public string SoruCumlesi { get; set; }
    }
}
