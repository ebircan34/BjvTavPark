using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TavParkBJV
{
    public partial class CokluFirmaAbonelik : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        SqlConnection baglanti, SDbaglanti;
        string connetionString;
        tempDbx tempdbx = new tempDbx();
        Gelirler gelirler = new Gelirler();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        public decimal keyKartUcreti = 0; public decimal genelToplam = 0;
        int az5 = 0;
        bool shiftLock=false;
        decimal araToplam = 0;
        int _period = 0;

        private void CokluFirmaAbonelik_Load(object sender, EventArgs e)
        {
            SD_Connect();
            DB_Connect();
            articleLoad();
            cmbAbonelikAdeti.SelectedIndex = 0;
            cmbAbonelikSuresi.SelectedIndex = 0;
            txtMidAra.Focus();  
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            btnKaydet.Enabled = false;
            btnsil.Enabled = false;
            MidRead();
            OdemeKasasiYukle();
            keykartUcretiniOgren();
            OtoparkYukle();
            cmbKeyKartAdeti.SelectedIndex = 0;
            cmbAbonelikDurumu.SelectedIndex = 0;    


        }

        private void MidReadOnly()
        {
            txtFirmaUnvani.ReadOnly = false;
            txtPlakaNo.ReadOnly = false;
            txtTelefonNo.ReadOnly = false;
            txtTcKimlikNo.ReadOnly = false; 
            txtemail.ReadOnly = false;  
            txtYetkili.ReadOnly = false;
            richTextBoxAdres.ReadOnly = false;
            txtilce.ReadOnly = false;
            txtsehir.ReadOnly = false;
        }

        private void MidRead()
        {
            txtFirmaUnvani.ReadOnly = true;
            txtPlakaNo.ReadOnly = true;
            txtTelefonNo.ReadOnly = true;
            txtTcKimlikNo.ReadOnly = true;
            txtemail.ReadOnly = true;
            txtYetkili.ReadOnly = true;
            richTextBoxAdres.ReadOnly = true;
            txtilce.ReadOnly = true;
            txtsehir.ReadOnly = true;
            richTextBoxAdres.ReadOnly=true; 
        }

        private void Panel3Clear()
        {
            txtPersonel.Text=string.Empty;
            cmbVardiya.SelectedIndex = -1;
            cmbOtopark.SelectedIndex = -1;
            cmbOdemeKasasi.SelectedIndex = -1;
            cmbArticle.SelectedIndex = -1;
            cmbAbonelikSuresi.SelectedIndex = -1;
            cmbAbonelikAdeti.SelectedIndex = 0; 
            radioButtonAbnYenile.Checked = false;
            radiobuttonKeyKart.Checked = false;
            richtextOzNot.Text = string.Empty;
            cmbOdeYontemi.SelectedIndex = -1;
            txtTanimUcreti.Text = "0";
            txtAboneSurei.Text = "2";
            txtAraToplam.Text = "0";
            txtKeyKart.Text = "0";
            txtOdemeTutari.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            txtMidAra.Text = "";



        }

        private void Panel2Clear()
        {
            txtFirmaUnvani.Text = string.Empty;
            txtilce.Text = string.Empty;
            txtsehir.Text = string.Empty;
            txtPlakaNo.Text = string.Empty;
            txtTelefonNo.Text = string.Empty;
            txtTcKimlikNo.Text = string.Empty;
            txtemail.Text = string.Empty;   
            txtYetkili.Text = string.Empty;
            richTextBoxAdres.Text = string.Empty;

        }

        private void selectIndex()
        {
            cmbAbonelikAdeti.SelectedIndex = 0;
            cmbAbonelikSuresi.SelectedIndex = 0;
            cmbKeyKartAdeti.SelectedIndex = 0;
            cmbAbonelikDurumu.SelectedIndex = 0;
        }

        private void btnMidAra_Click(object sender, EventArgs e)
        {
            if (txtMidAra.Text != string.Empty)
            {
                int BmusteriNo = Convert.ToInt16(txtMidAra.Text);
                var stexist = from s in db.TuzelMusteriler where s.MusteriId == BmusteriNo select s.MusteriId;
                if (stexist.Count() > 0)
                {
                    MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var st = (from s in db.TuzelMusteriler where s.MusteriId == BmusteriNo select s).First();
                    txtFirmaUnvani.Text = st.Unvan;
                    txtPlakaNo.Text = st.PlakaNo;
                    txtTcKimlikNo.Text = st.VergiNo;
                    txtTelefonNo.Text = st.TelefonNo;
                    richTextBoxAdres.Text = st.AdresText;
                    txtilce.Text = st.ilce;
                    txtsehir.Text = st.Sehir;
                    txtemail.Text = st.email;
                    txtID.Text = txtMidAra.Text;
                    txtYetkili.Text = st.Yetkili;

                }
                else
                {
                    MessageBox.Show("Müşteri Bulunamadı. Lütfen Kayıt Yapınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez. Lütfen Dikkat!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void SD_Connect()
        {
            StreamReader oku = new StreamReader(@"data\SC_DB.dat");
            connetionString = oku.ReadLine();
            SDbaglanti = new SqlConnection(connetionString);
            SDbaglanti.Open();
            //MessageBox.Show("SKIDATA Bağlantısı Yapıldı.  !");
            SDbaglanti.Close();
        }
        private void DB_Connect()
        {
            StreamReader oku = new StreamReader(@"data\Connection_DB.dat");
            connetionString = oku.ReadLine();
            baglanti = new SqlConnection(connetionString);
            baglanti.Open();
            //MessageBox.Show("Connection Open  !");
            baglanti.Close();
        }
        private void articleLoad()
        {

            cmbArticle.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from Tanimlar", baglanti);
            SqlDataReader dr_validasyon = cmd.ExecuteReader();

            while (dr_validasyon.Read())
            {
                cmbArticle.Items.Add(dr_validasyon["Tanim"]);

            }
            baglanti.Close();
            dr_validasyon.Close();
        }
        private void OtoparkYukle()
        {
            cmbOtopark.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\Carpark.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                cmbOtopark.Items.Add(tokens[0]);
            }
        }

        private void OdemeKasasiYukle()
        {
            cmbOdemeKasasi.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\OdemeKasasi.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                cmbOdemeKasasi.Items.Add(tokens[0]);
            }

        }

        private void keykartUcretiniOgren()
        {
            string[] lineOfContents = File.ReadAllLines(@"data\KeyKart.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //comboBox1.Items.Add(tokens[1]);
                keyKartUcreti = Convert.ToDecimal(tokens[0]);

            }
        }

        private void txtMidAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPersonel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);

            if ((int)e.KeyChar >= 33 && (int)e.KeyChar <= 47)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 58 && (int)e.KeyChar <= 64)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 91 && (int)e.KeyChar <= 96)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 123 && (int)e.KeyChar <= 127)
            {
                e.Handled = true;
            }
        }

        private void txtPersonel_TextChanged(object sender, EventArgs e)
        {
            txtPersonel.Text = txtPersonel.Text.ToUpper();
            txtPersonel.SelectionStart = txtPersonel.Text.Length;
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            Panel2Clear();
            Panel3Clear();
            dateTimePicker1.Value = DateTime.Now;   
            dateTimePicker2.Value= DateTime.Now;
            btnKaydet.Enabled = false;
            btnsil.Enabled = false;
            btnYeni.Enabled = true;

            az5 = 0;// en son satır.
        }

        private void GenelToplamHesapla(int abnsuresi,int abnadet,int kkadeti)
        {
         
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
           
            
            
            
           // araToplam = decimal.Parse(cmbAbonelikSuresi.Text) * decimal.Parse(txtTanimUcreti.Text);
           // txtAraToplam.Text = araToplam.ToString("N");
            az5 = 1;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            az5 = 0;// en son satır.
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled= true;
            btnsil.Enabled= false;
        }

        private void abonelikHesapla()
        {
            araToplam = 0;
            txtTanimUcreti.Text = "0";

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from Tanimlar", baglanti);
            SqlDataReader drarticle = cmd.ExecuteReader();

            while (drarticle.Read())
            {
                if (cmbArticle.Text == drarticle[1].ToString())
                {
                    dateTimePicker1.Value = DateTime.Today;
                    txtTanimUcreti.Text = drarticle[2].ToString();
                    //timerr_Count = Convert.ToInt32(drarticle[4]);
                    _period = Convert.ToInt32(drarticle[3]);
                    DateTime date = DateTime.Now;
                    _period = int.Parse(cmbAbonelikSuresi.Text) * _period;
                    date = date.AddDays(_period); // Adds days to the date
                    dateTimePicker2.Value = Convert.ToDateTime(date);

                }
            }
            //textBoxOdemeTutari.Text = Convert.ToString(textBoxRevenue.Text);

            //textBoxAraToplam.Text = Convert.ToString(0);
            baglanti.Close();
            //araToplam = decimal.Parse(cmbAbonelikSuresi.Text) * decimal.Parse(txtTanimUcreti.Text);
            //txtAraToplam.Text = araToplam.ToString("N");


        }

        private void cmbArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            abonelikHesapla();
            az5 = 0;
        }

        private void cmbAbonelikSuresi_SelectedIndexChanged(object sender, EventArgs e)
        {
            az5 = 0;
        }

        private void cmbAbonelikDurumu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radiobuttonKeyKart_CheckedChanged(object sender, EventArgs e)
        {
            az5 = 0;
        }

        private void radioButtonAbnYenile_CheckedChanged(object sender, EventArgs e)
        {
            az5 = 0;
        }

        public CokluFirmaAbonelik()
        {
            InitializeComponent();
        }
    }
}
