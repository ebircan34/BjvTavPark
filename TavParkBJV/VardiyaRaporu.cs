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
using Excel = Microsoft.Office.Interop.Excel;

namespace TavParkBJV
{
    public partial class VardiyaRaporu : Form
    {
        string connetionString, _shiftBlock, _Personel;
        public bool updateLock = false;
        public bool oemLock = false;
        public bool customerLock = false;
        public bool NewRecord = false;
        public string _register = "NULL";
        public string _status = "NULL";
        SqlConnection baglanti, SDbaglanti;
        public int _period;
        public int abnAdet;
        public decimal araToplam = 0;
        public decimal keyKartUcreti = 0; public decimal genelToplam = 0;
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        public VardiyaRaporu()
        {
            InitializeComponent();
        }

        private void VardiyaRaporu_Load(object sender, EventArgs e)
        {
           
            VardiyaYukle();
            SD_Connect();
            DB_Connect();
            comboBoxVardiya.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void txtPerid_KeyPress(object sender, KeyPressEventArgs e)
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
           

        }

        private void btnbulGetir_Click(object sender, EventArgs e)
        {
            DateTime Dt1,Dt2;
            String StringDt1,StringDt2;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            Dt2 = Convert.ToDateTime(StringDt2);   
            

            if (comboBoxVardiya.Text ==string.Empty)
            {
                MessageBox.Show("Vardiya Saat Aralığı Boş Geçilemez!");

            }
            else
            { 

            //int IDPX = Convert.ToInt16(txtPerid.Text);
           
            var toplam = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text &  z.BaslangicTarihi>=Dt1 & z.BaslangicTarihi<=Dt2 ).Sum(p => p.GenelToplam);
            if (toplam != null)
            {
                labelToplam.Text = string.Format("{0:C}", toplam);
                toplam = 0;
                labelToplamAdet.Text= db.Gelirler.Count(x=> x.Vardiya== comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2).ToString();
            }
            else if (toplam == null)
            {
                labelToplam.Text = "0";
                toplam = 0;
                labelToplamAdet.Text = "0";
            }

            var krediKarti = db.Gelirler.Where(z=> z.Vardiya==comboBoxVardiya.Text & z.BaslangicTarihi>=Dt1 & z.BaslangicTarihi<=Dt2 & z.OdemeYontemi=="KREDI KARTI").Sum(p=> p.GenelToplam);
            if (krediKarti != null)
            {
                labelKrediKarti.Text = string.Format("{0:C}", krediKarti);
                krediKarti = 0;
                labelKrediKartiAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text &  x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemi == "KREDI KARTI").ToString();
             }                     
            else
            {
                labelKrediKarti.Text = "0";
                krediKarti = 0;
                labelKrediKartiAdet.Text = "0";
            }
            //-------------------------------------
            var nakit = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.OdemeYontemi == "Nakit").Sum(p => p.GenelToplam);
            if (nakit != null)
            {
                labelNakit.Text = string.Format("{0:C}", nakit);
                nakit = 0;
                labelNakitAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemi == "Nakit").ToString();
            }
            else
            {
                labelNakit.Text = "0";
                nakit = 0;
                labelNakitAdet.Text = "0";
            }
            //------------------------------------
            //-------------------------------------
            var eft = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.OdemeYontemi == "HAVALE-EFT").Sum(p => p.GenelToplam);
            if (eft != null)
            {
                labelhavaleEft.Text = string.Format("{0:C}", eft);
                eft = 0;
                labelEftAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemi == "HAVALE-EFT").ToString();
            }
            else
            {
                labelhavaleEft.Text = "0";
                eft = 0;
                labelEftAdet.Text = "0";
            }
            //------------------------------------
            //-------------------------------------
            var fat = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.InvoiceStatus == "FATURA").Sum(p => p.GenelToplam);
            if (fat != null)
            {
                labelFatura.Text = string.Format("{0:C}", fat);
                fat = 0;
                labelFaturaAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.InvoiceStatus == "FATURA").ToString();
            }
            else
            {
                labelFatura.Text = "0";
                fat = 0;
                labelFaturaAdet.Text = "0";
            }
            //------------------------------------

            //-------------------------------------
            var fis = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.InvoiceStatus == "FİŞ").Sum(p => p.GenelToplam);
            if (fis != null)
            {
                labelFis.Text = string.Format("{0:C}", fis);
                fis = 0;
                labelFisAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.InvoiceStatus == "FİŞ").ToString();
            }
            else
            {
                labelFis.Text = "0";
                fis = 0;
                labelFisAdet.Text = "0";
            }
            //------------------------------------
            var abn = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.Status == "ABONE").Sum(p => p.GenelToplam);
            if (abn != null)
            {
                labelAbone.Text = string.Format("{0:C}", abn);
                abn = 0;
                labelAboneAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Status == "ABONE").ToString();
            }
            else
            {
                labelAbone.Text = "0";
                fis = 0;
                labelAboneAdet.Text = "0";
            }
            //------------------------------------
            var cong = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.Status == "CONGRESS").Sum(p => p.GenelToplam);
            if (cong != null)
            {
                labelCongress.Text = string.Format("{0:C}", cong);
                cong = 0;
                labelCongessAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Status == "CONGRESS").ToString();
            }
            else
            {
                labelCongress.Text = "0";
                cong = 0;
               labelCongessAdet.Text = "0";
            }
            //------------------------------------
            //------------------------------------
            var ozsat = db.Gelirler.Where(z => z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2 & z.Status == "ÖZEL SATIŞ").Sum(p => p.GenelToplam);
            if (ozsat != null)
            {
               labelOzelSatis.Text = string.Format("{0:C}", ozsat);
               ozsat = 0;
               labelOzelSatisAdet.Text = db.Gelirler.Count(x => x.Vardiya == comboBoxVardiya.Text & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Status == "ÖZEL SATIŞ").ToString();
            }
            else
            {
                labelOzelSatis.Text = "0";
                ozsat = 0;
                labelOzelSatisAdet.Text = "0";
            }
            //------------------------------------
            //DataTable dt1 = new DataTable();
            //baglanti.Open();
            //SqlDataAdapter ad = new SqlDataAdapter("select Tanim as SatışTanımı,SatisGeliri,Sum(sure) as Adet,KeyKartGeliri,GenelToplam,OdemeYontemi from Gelirler where BaslangicTarihi>='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' and BaslangicTarihi<='"+dateTimePicker2.Value.ToString("yyyy-MM-dd")+ "' and Vardiya='"+comboBoxVardiya.Text+"' group by Tanim,SatisGeliri,KeyKartGeliri,GenelToplam,OdemeYontemi", baglanti);
            //ad.Fill(dt1);
            //dataGridView1.DataSource = dt1;
            //baglanti.Close();

            var st = (from s in db.Vardiya where s.VStatus == "Open" select s).First();
            txtPersonel.Text = st.AdSoyad;

            }
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

        private void btnExceLGonder_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                //excelWorkbook = excel.Workbooks.Open(Application.StartupPath + "\\Rapor\\AboneDetayRaporu.xlsx");
                this.Cursor = Cursors.WaitCursor;
                excel.Visible = false;
                excel.DisplayAlerts = false;
                excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\VardiyaRaporu.xlsx");

                excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["VardiyaRaporu"];
                int satirArttirimi = 21;

                for (int i = 0; i < dataGridView2.RowCount-1; i++)
                {
                    excelWorksheet.Cells[i + satirArttirimi, 3] = dataGridView2.Rows[i].Cells["SiraNo"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 4] = dataGridView2.Rows[i].Cells["BaslangicTarihi"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 5] = dataGridView2.Rows[i].Cells["Plaka"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 6] = dataGridView2.Rows[i].Cells["AdSoyadUnvan"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 7] = dataGridView2.Rows[i].Cells["Telefon"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 8] = dataGridView2.Rows[i].Cells["email"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 9] = dataGridView2.Rows[i].Cells["Tanim"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 10] = dataGridView2.Rows[i].Cells["SatisGeliri"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 11] = dataGridView2.Rows[i].Cells["Sure"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 12] = dataGridView2.Rows[i].Cells["AraTplm"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 13] = dataGridView2.Rows[i].Cells["KeyKart"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 14] = dataGridView2.Rows[i].Cells["GnlToplam"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 15] = dataGridView2.Rows[i].Cells["OdemeYonDetayi"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 16] = dataGridView2.Rows[i].Cells["Otopark"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 17] = dataGridView2.Rows[i].Cells["OdemeKasasi"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 18] = dataGridView2.Rows[i].Cells["Personel"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 19] = dataGridView2.Rows[i].Cells["KartBiletNo"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 20] = dataGridView2.Rows[i].Cells["Status"].Value.ToString();

                }

                string vz="";

                if (comboBoxVardiya.Text=="08:00-20:00") vz = "0800_2000";
                if (comboBoxVardiya.Text=="20:00-08:00") vz = "2000_0800";

                excelWorksheet.Cells[5, 2] = txtPersonel.Text;
                excelWorksheet.Cells[3, 2] = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                excelWorksheet.Cells[3, 4] = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                excelWorksheet.Cells[3, 6] = comboBoxVardiya.Text;
                excelWorksheet.Cells[8, 2] = labelToplam.Text;
                excelWorksheet.Cells[8, 3] = labelToplamAdet.Text;
                excelWorksheet.Cells[9, 2] = labelKrediKarti.Text;
                excelWorksheet.Cells[9, 3] = labelKrediKartiAdet.Text;
                excelWorksheet.Cells[10, 2] = labelNakit.Text;
                excelWorksheet.Cells[10, 3] = labelNakitAdet.Text;
                excelWorksheet.Cells[11, 2] = labelhavaleEft.Text;
                excelWorksheet.Cells[11, 3] = labelEftAdet.Text;
                excelWorksheet.Cells[12, 2] = labelCari.Text;
                excelWorksheet.Cells[12, 3] = labelCariAdet.Text;
                excelWorksheet.Cells[14, 2] = labelFatura.Text;
                excelWorksheet.Cells[14, 3] = labelFaturaAdet.Text;
                excelWorksheet.Cells[15, 2] = labelFis.Text;
                excelWorksheet.Cells[15, 3] = labelFisAdet.Text;
                excelWorksheet.Cells[16, 2] = labelAbone.Text;
                excelWorksheet.Cells[16, 3] = labelAboneAdet.Text;
                excelWorksheet.Cells[17, 2] = labelCongress.Text;
                excelWorksheet.Cells[17, 3] = labelCongessAdet.Text;
                excelWorksheet.Cells[18, 2] = labelOzelSatis.Text;
                excelWorksheet.Cells[18, 3] = labelOzelSatisAdet.Text;
                   
                excelWorksheet.Columns.AutoFit();
                dataGridView2.DataSource = null;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                
                if (comboBoxVardiya.Text==string.Empty)
                {
                    saveDialog.FileName = "SatışRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
                }
                else
                {
                    saveDialog.FileName = "VardiyaRaporu_" + vz + "_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
                }
                

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    excelWorksheet.SaveAs(saveDialog.FileName);

                    MessageBox.Show("Rapor Excel Formatında Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Gönderilecek Veri Bulunamadı","UYARI",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void BtnListeOlustur_Click(object sender, EventArgs e)
        {
            if (comboBoxVardiya.Text == string.Empty)
            {
                MessageBox.Show("Vardiya Saat Aralığı Seçimi Yapınız!");
            }
            else
            {


                DateTime Dt1, Dt2;
                string icHatOtoPark = "IC HAT 1 OTOPARK";
                string disHatOtoPark = "DIS HAT OTOPARK";
                string rentACarOtopark = "RENT A CAR OTOPARK";
                dataGridView2.Rows.Clear();
                int satirsayisi;
                int i = 0; int z = 0;
                String StringDt1, StringDt2;
                StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                Dt1 = Convert.ToDateTime(StringDt1);
                StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                Dt2 = Convert.ToDateTime(StringDt2);
                string invoices = "FATURA";
                string vardiya;
                vardiya = comboBoxVardiya.Text;
                // string ozsatis = "ÖZEL SATIŞ";

                var bireysel = (from px in db.Gelirler
                                join fx in db.GercekMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Vardiya==vardiya 
                                select new
                                {

                                    bastar = px.BaslangicTarihi,
                                    _PlakaNo = fx.PlakaNo,
                                    _AdSoyadUnvan = fx.AdSoyad,
                                    _tekNo = fx.TelefonNo,
                                    _email = fx.email,
                                    _tanim = px.Tanim,
                                    _birimFiyat = px.SatisGeliri,
                                    _sureAdet = px.Sure,
                                    _AraToplam = px.AraToplam,
                                    _keyKart = px.KeyKartGeliri,
                                    _GenelToplam = px.GenelToplam,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _odemeKasai = px.OdemeKasasi,
                                    _islemiYapan = px.Personel,
                                    _kartBiletNo = px.KartBiletNo,
                                    _ilce = fx.ilce,
                                    _sehir = fx.Sehir,
                                    _adresText = fx.AdresText,
                                    _satisTipi = px.Status,
                                    _vergiDairesi = fx.VergiDairesi,
                                }).ToList();


                satirsayisi = bireysel.Count;



                i = 0;
                z = 1;


                bireysel.ForEach(x =>
                {


                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells["SiraNo"].Value = z.ToString();
                    dataGridView2.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView2.Rows[i].Cells["Plaka"].Value = x._PlakaNo.ToString();
                    dataGridView2.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView2.Rows[i].Cells["Telefon"].Value = x._tekNo;
                    dataGridView2.Rows[i].Cells["email"].Value = x._email;
                    dataGridView2.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView2.Rows[i].Cells["SatisGeliri"].Value = x._birimFiyat;
                    dataGridView2.Rows[i].Cells["Sure"].Value = x._sureAdet;
                    dataGridView2.Rows[i].Cells["AraTplm"].Value = x._AraToplam;
                    dataGridView2.Rows[i].Cells["KeyKart"].Value = x._keyKart.ToString();
                    dataGridView2.Rows[i].Cells["GnlToplam"].Value = x._GenelToplam;
                    dataGridView2.Rows[i].Cells["OdemeYonDetayi"].Value = x._odemeYontemiNet;
                    dataGridView2.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView2.Rows[i].Cells["OdemeKasasi"].Value = x._odemeKasai;
                    dataGridView2.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView2.Rows[i].Cells["Status"].Value = x._satisTipi;
                    dataGridView2.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView2.Rows[i].Cells["VergiDairesi"].Value = x._vergiDairesi;
                    dataGridView2.Rows[i].Cells["Ilce"].Value = x._ilce;
                    dataGridView2.Rows[i].Cells["Sehir"].Value = x._sehir;
                    dataGridView2.Rows[i].Cells["AdresText"].Value = x._adresText;




                    z = z + 1;
                    i = i + 1;
                });
                var firmalar = (from px in db.Gelirler
                                join fx in db.TuzelMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Vardiya == vardiya
                                select new
                                {
                                    bastar = px.BaslangicTarihi,
                                    _PlakaNo = fx.PlakaNo,
                                    _AdSoyadUnvan = fx.Unvan,
                                    _tekNo = fx.TelefonNo,
                                    _email = fx.email,
                                    _tanim = px.Tanim,
                                    _birimFiyat = px.SatisGeliri,
                                    _sureAdet = px.Sure,
                                    _AraToplam = px.AraToplam,
                                    _keyKart = px.KeyKartGeliri,
                                    _GenelToplam = px.GenelToplam,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _odemeKasai = px.OdemeKasasi,
                                    _islemiYapan = px.Personel,
                                    _kartBiletNo = px.KartBiletNo,
                                    _ilce = fx.ilce,
                                    _sehir = fx.Sehir,
                                    _adresText = fx.AdresText,
                                    _satisTipi = px.Status,
                                    _vergiDairesi = fx.VergiDairesi,

                                }).ToList();


                firmalar.ForEach(x =>
                {


                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells["SiraNo"].Value = z.ToString();
                    dataGridView2.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView2.Rows[i].Cells["Plaka"].Value = x._PlakaNo.ToString();
                    dataGridView2.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView2.Rows[i].Cells["Telefon"].Value = x._tekNo;
                    dataGridView2.Rows[i].Cells["email"].Value = x._email;
                    dataGridView2.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView2.Rows[i].Cells["SatisGeliri"].Value = x._birimFiyat;
                    dataGridView2.Rows[i].Cells["Sure"].Value = x._sureAdet;
                    dataGridView2.Rows[i].Cells["AraTplm"].Value = x._AraToplam;
                    dataGridView2.Rows[i].Cells["KeyKart"].Value = x._keyKart.ToString();
                    dataGridView2.Rows[i].Cells["GnlToplam"].Value = x._GenelToplam;
                    dataGridView2.Rows[i].Cells["OdemeYonDetayi"].Value = x._odemeYontemiNet;
                    dataGridView2.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView2.Rows[i].Cells["OdemeKasasi"].Value = x._odemeKasai;
                    dataGridView2.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView2.Rows[i].Cells["Status"].Value = x._satisTipi;
                    dataGridView2.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView2.Rows[i].Cells["VergiDairesi"].Value = x._vergiDairesi;
                    dataGridView2.Rows[i].Cells["Ilce"].Value = x._ilce;
                    dataGridView2.Rows[i].Cells["Sehir"].Value = x._sehir;
                    dataGridView2.Rows[i].Cells["AdresText"].Value = x._adresText;



                    z = z + 1;
                    i = i + 1;
                });






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

        private void VardiyaYukle()
        {
            comboBoxVardiya.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\VardiyaSaati.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                comboBoxVardiya.Items.Add(tokens[0]);
            }

        }



    }
}
