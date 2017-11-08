using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnketV2
{
    public partial class SoruDuzenleForm : Form
    {
        public Soru GelenSoru { get; set; }
        public SoruDuzenleForm()
        {
            InitializeComponent();
        }
        private void SoruDuzenleForm_Load(object sender, EventArgs e)
        {            
            textBox2.Text = GelenSoru.SoruCumlesi;
        }
        private void button2_Click(object sender, EventArgs e)
        {//SoruDuzenleForm düzenle butonu
            AnketContext db = new AnketContext(); 
            var duzenlenecek = db.Sorular.Find(GelenSoru.SoruID);
            duzenlenecek.SoruCumlesi = textBox2.Text;
            db.Entry(duzenlenecek).State = EntityState.Modified;
            db.SaveChanges();
            Form1 f = (Form1)Application.OpenForms["Form1"];
            f.SorulariYenile();
            f.CevaplariYenile();
        }
    }
}
