using DAL;
using Entity;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnketV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        AnketContext db = new AnketContext();
        Soru soru = new Soru();
        Cevap cevap = new Cevap();
        private void button2_Click(object sender, EventArgs e)
        {

            soru.SoruCumlesi = textBox2.Text;
            db.Sorular.Add(soru);
            db.SaveChanges();
            SorulariYenile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SorulariYenile();
            CevaplariYenile();               
        }
        public void CevaplariYenile()
        {
            dataGridView2.DataSource = null;
            //dataGridView2.DataSource = db.Cevaplar.ToList();
            dataGridView2.DataSource = db.Cevaplar.Select(x => new CevapViewModel()
            {
                AdSoyad = x.CevabiVerenKisi.AdSoyad,
                Soru = x.Sorusu.SoruCumlesi,
                Cevap = x.Yanit.ToString(),
                CevapID = x.CevapID
            }).ToList();


        }
        public void SorulariYenile()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.Sorular.ToList();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Clear();

            foreach (Soru soru in db.Sorular)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = soru.SoruCumlesi;
                flowLayoutPanel1.Controls.Add(lbl);
                //flowLayoutPanel1.SetFlowBreak(lbl, true);



                RadioButton r1 = new RadioButton();
                r1.Name = "Soru_" + soru.SoruID; //radiobuttonlara isim verdik.
                r1.Text = "Evet";
                RadioButton r2 = new RadioButton();
                r2.Name = "Soru_" + soru.SoruID; //radiobuttonlara isim verdik.
                r2.Text = "Hayir";
                //flowLayoutPanel1.SetFlowBreak(r2,true);

                FlowLayoutPanel p = new FlowLayoutPanel();
                p.Width = 400;
                p.Height = 40;
                p.Controls.Add(r1);
                p.Controls.Add(r2);
                flowLayoutPanel1.Controls.Add(p);


                //flowLayoutPanel1.SetFlowBreak(p, true);
                /*Radio button yerşne combobox kullanmak istersek
                 ComboBox c1 = new ComboBox();
                c1.Items.Add("Evet");
                c1.Items.Add("Hayır");
                flowLayoutPanel1.Controls.Add(c1);
                flowLayoutPanel1.SetFlowBreak(c1,true);*/
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//AnketKayıtButonu
            foreach (Control pnl in flowLayoutPanel1.Controls)//form'a eklediğimiz flowlayoutpanelin içindeki kontrolleri Control tipinde bir pnl nesnesine aktarıyor.
            {
                if (pnl is FlowLayoutPanel)//pnl nesnesi FlowLayoutPanel mi? diye soruyoruz.
                {
                    foreach (RadioButton item in ((FlowLayoutPanel)pnl).Controls)
                    {

                        RadioButton r = (RadioButton)item;
                        if (r.Checked)
                        {
                            string soruID = item.Name.Replace("Soru_", "");
                            int SID = Convert.ToInt32(soruID);
                            Cevap cevap = new Cevap();
                            cevap.SoruID = SID;
                            cevap.Yanit = r.Text == "Evet" ? Yanit.Evet : Yanit.Hayir;

                            Kisi kisi = db.Kisiler.Where(x => x.AdSoyad == textBox1.Text).FirstOrDefault();//FirstorDefault() bulabilirse ilk kişiyi getirir bulamazsa null getirir.
                            if (kisi != null)
                                cevap.KisiID = kisi.KisiID;
                            else
                            {
                                kisi = new Kisi();
                                kisi.AdSoyad = textBox1.Text;
                                db.Kisiler.Add(kisi);
                                db.SaveChanges();
                                cevap.KisiID = kisi.KisiID;

                            }
                            db.Cevaplar.Add(cevap);
                            db.SaveChanges();
                        }
                    }
                }
            }
            CevaplariYenile();
            MessageBox.Show("Kaydedildi");


        }

        private void button3_Click(object sender, EventArgs e)
        {//sorular sil
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Soru seçiniz");
            else
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    int soruID=(int)item.Cells[0].Value;
                    Soru silinecek = db.Sorular.Find(soruID);
                    db.Sorular.Remove(silinecek);
                }
                db.SaveChanges();
                SorulariYenile();
            }
            //var a = dataGridView1.SelectedRows[0].Cells[0].Value;

        }

        private void button4_Click(object sender, EventArgs e)
        {//Düzenle butonu
            //Benim yaptığım yol
           if(dataGridView1.SelectedRows.Count!=0)
            {
                
                Soru a = new Soru();
                a.SoruCumlesi= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                a.SoruID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                SoruDuzenleForm s = new SoruDuzenleForm();
                s.GelenSoru = a;
                s.Show();
            }
           else
            {
                MessageBox.Show("Soru seçmedinki düzenleyesin?");
            }

           // Hocanın Yolu
            /*if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Soru seçiniz");
            else
            {
                SoruDuzenleForm form = new SoruDuzenleForm();
                int secilenID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Soru duzenlenecek = db.Sorular.Find(secilenID);
                form.GelenSoru = duzenlenecek;
                form.Show();
            }
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {//Cevaplar Sil
            if (dataGridView2.SelectedRows.Count == 0)
                MessageBox.Show("Silinecek bir cevap seçiniz");
            else
            {
                foreach (DataGridViewRow item in dataGridView2.SelectedRows)
                {
                    int cevapID = (int)item.Cells[0].Value;
                    Cevap silinecek = db.Cevaplar.Find(cevapID);
                    db.Cevaplar.Remove(silinecek);
                }
                db.SaveChanges();
                CevaplariYenile();
            }
            //CevapID kullanmadan yapma Hocanın yaptığı
            //if (dataGridView2.SelectedRows.Count == 0)
            //    MessageBox.Show("Cevap seçiniz");
            //else
            //{
            //    List<Cevap> silinecekler = new List<Cevap>();
            //    foreach (DataGridViewRow item in dataGridView2.SelectedRows)
            //    {
            //        var silinecek = db.Cevaplar.ToList()[item.Index];
            //        silinecekler.Add(silinecek);
            //        //int CevapID = (int)item.Cells[0].Value;
            //        //Cevap silinecek = db.Cevaplar.Find(CevapID);
            //        //db.Cevaplar.Remove(silinecek);
            //    }
            //    db.Cevaplar.RemoveRange(silinecekler);
            //    db.SaveChanges();
            //    //db.SaveChanges();
            //    //Yenile();
            //    CevaplariYenile();
            }
        }
    }


