using DevExpress.Utils.Layout;
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
    public partial class SatisRaporu : Form
    {

        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        SqlConnection baglanti, SDbaglanti;
        bool oemLock=false;
        string connetionString;
        public int _ID, _PerID;
        public static string _vardiya,_personel;
        DateTime _OpenTime;
        public SatisRaporu()
        {
            InitializeComponent();
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

        

        private void txtPerNo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPerNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '£' || e.KeyChar == '½' ||
           e.KeyChar == '€' || e.KeyChar == '?' ||
           e.KeyChar == '¨' || e.KeyChar == 'æ' ||
           e.KeyChar == 'ß' || e.KeyChar == '´')
            {
                e.Handled = true;
            }
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
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtVarNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '£' || e.KeyChar == '½' ||
               e.KeyChar == '€' || e.KeyChar == '?' ||
               e.KeyChar == '¨' || e.KeyChar == 'æ' ||
               e.KeyChar == 'ß' || e.KeyChar == '´')
            {
                e.Handled = true;
            }
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
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnKontrol_Click(object sender, EventArgs e)
        {

            //if (txtPerNo.Text != string.Empty || txtVarNo.Text!= string.Empty)
           // {
                //try
                //{
                    
                //    DataTable dt1 = new DataTable();
                //    baglanti.Open();
                //    SqlDataAdapter ad = new SqlDataAdapter("select ID,PerID,AdSoyad,Vardiya,OpenTime,DeviceDesing,VStatus from Vardiya where PerID='" + txtPerNo.Text + "' and ID='"+txtVarNo.Text+"' and OpenTime='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"'", baglanti);
                //    ad.Fill(dt1);
                //    dataGridView2.DataSource = dt1;
                //    baglanti.Close();
                //    oemLock=true;

                //}

                //catch (Exception ex)
                //{
                //    MessageBox.Show("Veri Tabanı Baglanti Hatasi 008", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
           
            
            //}
           // else if (txtPerNo.Text == string.Empty || txtVarNo.Text==string.Empty)
          //  {
                //MessageBox.Show("Zorunlu Alanlar da Eksik Giriş Bilgisi","UYARI",MessageBoxButtons.OK, MessageBoxIcon.Error);
           // }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            SD_Connect();
            DB_Connect();
            DataTable dt1 = new DataTable();
            baglanti.Open();
            SqlDataAdapter ad = new SqlDataAdapter("select ID,PerID,AdSoyad,Vardiya,OpenTime,DeviceDesing,VStatus from Vardiya where OpenTime>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'", baglanti);
            ad.Fill(dt1);
            dataGridView2.DataSource = dt1;
            baglanti.Close();
        }

        private void btnCrXML_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _ID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            _PerID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value.ToString());
            _vardiya = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            _personel = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            _OpenTime= Convert.ToDateTime(dataGridView2.CurrentRow.Cells[4].Value.ToString());
            _OpenTime = Convert.ToDateTime(_OpenTime.ToString("yyyy.MM.dd"));
            labelVardiyaSaati.Text = _vardiya;
            labelVardiyaNo.Text=_ID.ToString();
            labelPersonel.Text = _personel;
            labelBasTar.Text = _OpenTime.ToString("yyyy.MM.dd");

            //baglanti.Open();
            //DataTable dt1 = new DataTable();
            //SqlCommand cmd = new SqlCommand("select count(GenelToplam),Sum(GenelToplam),count(KeyKartGeliri),sum(KeyKartGeliri) from Gelirler where PerID='"+ _PerID + "' and Vardiya='"+ _vardiya + "' and VardiyaID='"+_ID+"'",baglanti);

            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    if (dr[0].ToString() == string.Empty)
            //    {
            //        TLpara = 0;
            //        textBoxisladet.Text = Convert.ToString(TLpara);
            //        //textBoxcongreess.Text = string.Format("{0:c}", decimal.Parse(textBoxcongreess.Text));
            //    }
            //    else
            //    {

            //        textBoxisladet.Text = dr6[0].ToString();
            //        //TLpara = Convert.ToDouble(textBoxisladet.Text);
            //        //textBoxcongreess.Text = string.Format("{0:c}", decimal.Parse(textBoxcongreess.Text));
            //    }



            //}
            //dr.Close();
            //baglanti.Close();

            if (oemLock==false)
            { 

            int IDX = Convert.ToInt16(labelVardiyaNo.Text);
           
            
            
            var toplam = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX).Sum(p => p.GenelToplam);
            if (toplam != null)
            {
                labelToplamCiro.Text = string.Format("{0:C}", toplam);
                toplam = 0;
            }
            else if (toplam == null)
            {
                labelToplamCiro.Text = "0";
                toplam = 0;
            }

            var krediKarti = db.Gelirler.Where(z => z.Vardiya ==labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi=="KREDI KARTI").Sum(p => p.GenelToplam);
            if (krediKarti != null)
            {
               labelKrediKarti.Text = string.Format("{0:C}", krediKarti);
                krediKarti = 0;
            }
            else if(krediKarti == null)
            {
                labelKrediKarti.Text= "0";
                string _kk = labelKrediKarti.Text;
                labelKrediKarti.Text= string.Format("{0:C}", _kk);
            }





            var nakit = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "Nakit").Sum(p => p.GenelToplam);
            if (nakit != null)
            {
                labelNakit.Text = string.Format("{0:C}", nakit);
                nakit = 0;
            }
            else if (nakit == null)
            {
                labelNakit.Text = "0";
                string nk = labelNakit.Text;
                labelNakit.Text = string.Format("{0:C}", nk);
            }



            var havaleEft = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "HAVALE-EFT").Sum(p => p.GenelToplam);
            if (havaleEft != null)
            {
                labelHavaleEft.Text = string.Format("{0:C}", havaleEft);
                havaleEft = 0;
            }
            else if (havaleEft == null)
            {
                labelHavaleEft.Text = "0";
                labelHavaleEft.Text = string.Format("{0:C}", labelHavaleEft.Text);
            }

            var cari= db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "CARI").Sum(p => p.GenelToplam);
            if (cari != null)
            {
                labelCari.Text = string.Format("{0:C}", cari);
                cari = 0;
            }
            else if (cari == null)
            {
                labelCari.Text = "0";
                labelCari.Text = string.Format("{0:C}", labelCari.Text);
            }

            var fat=db.Gelirler.Where(z=> z.Vardiya==labelVardiyaSaati.Text & z.PerID == _PerID &z.VardiyaID == IDX & z.InvoiceStatus=="FATURA").Sum(p => p.GenelToplam);
            if (fat != null)
            {
                labelFatura.Text=string.Format("{0:C}",fat);
                fat = 0;
            }
            else if(fat == null)
            {
                labelFatura.Text= "0";
                fat= 0;
            }

            baglanti.Open();
            DataTable dt1 = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter("select Tanim as SatışTanımı,SatisGeliri,Sum(sure) as Adet,KeyKartGeliri,GenelToplam,OdemeYontemi from Gelirler where PerID='"+_PerID+"' and VardiyaID='"+_ID+"' group by Tanim,SatisGeliri,KeyKartGeliri,GenelToplam,OdemeYontemi", baglanti);
            ad.Fill(dt1);
            dataGridView1.DataSource = dt1;
            baglanti.Close();

         }
         else
            {
                int IDX = Convert.ToInt16(labelVardiyaNo.Text);



                var toplam = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX).Sum(p => p.GenelToplam);
                if (toplam != null)
                {
                    labelToplamCiro.Text = string.Format("{0:C}", toplam);
                    toplam = 0;
                }
                else if (toplam == null)
                {
                    labelToplamCiro.Text = "0";
                    toplam = 0;
                }

                var krediKarti = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "KREDI KARTI").Sum(p => p.GenelToplam);
                if (krediKarti != null)
                {
                    labelKrediKarti.Text = string.Format("{0:C}", krediKarti);
                    krediKarti = 0;
                }
                else if (krediKarti == null)
                {
                    labelKrediKarti.Text = "0";
                    string _kk = labelKrediKarti.Text;
                    labelKrediKarti.Text = string.Format("{0:C}", _kk);
                }





                var nakit = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "Nakit").Sum(p => p.GenelToplam);
                if (nakit != null)
                {
                    labelNakit.Text = string.Format("{0:C}", nakit);
                    nakit = 0;
                }
                else if (nakit == null)
                {
                    labelNakit.Text = "0";
                    string nk = labelNakit.Text;
                    labelNakit.Text = string.Format("{0:C}", nk);
                }



                var havaleEft = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "HAVALE-EFT").Sum(p => p.GenelToplam);
                if (havaleEft != null)
                {
                    labelHavaleEft.Text = string.Format("{0:C}", havaleEft);
                    havaleEft = 0;
                }
                else if (havaleEft == null)
                {
                    labelHavaleEft.Text = "0";
                    labelHavaleEft.Text = string.Format("{0:C}", labelHavaleEft.Text);
                }

                var cari = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.OdemeYontemi == "CARI").Sum(p => p.GenelToplam);
                if (cari != null)
                {
                    labelCari.Text = string.Format("{0:C}", cari);
                    cari = 0;
                }
                else if (cari == null)
                {
                    labelCari.Text = "0";
                    labelCari.Text = string.Format("{0:C}", labelCari.Text);
                }

                var fat = db.Gelirler.Where(z => z.Vardiya == labelVardiyaSaati.Text & z.PerID == _PerID & z.VardiyaID == IDX & z.InvoiceStatus == "FATURA").Sum(p => p.GenelToplam);
                if (fat != null)
                {
                    labelFatura.Text = string.Format("{0:C}", fat);
                    fat = 0;
                }
                else if (fat == null)
                {
                    labelFatura.Text = "0";
                    fat = 0;
                }

                baglanti.Open();
                DataTable dt1 = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter("select Tanim as SatışTanımı,SatisGeliri,Sum(sure) as Adet,KeyKartGeliri,GenelToplam,OdemeYontemi from Gelirler where PerID='" + _PerID + "' and VardiyaID='" + _ID + "' group by Tanim,SatisGeliri,KeyKartGeliri,GenelToplam,OdemeYontemi", baglanti);
                ad.Fill(dt1);
                dataGridView1.DataSource = dt1;
                baglanti.Close();

            }








        }

        private void SatisRaporu_Load(object sender, EventArgs e)
        {
            try
            {
                SD_Connect();
                DB_Connect();
                DataTable dt1 = new DataTable();
                baglanti.Open();
                SqlDataAdapter ad = new SqlDataAdapter("select ID,PerID,AdSoyad,Vardiya,OpenTime,DeviceDesing,VStatus from Vardiya where VStatus='Open'", baglanti);
                ad.Fill(dt1);
                baglanti.Close();
                dataGridView2.DataSource= dt1;  

            }

            catch (Exception ex)
            {
             MessageBox.Show("Veri Tabanı Baglanti Hatasi 008","UYARI",MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
