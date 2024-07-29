using Microsoft.Office.Interop.Excel;
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
    public partial class KeyKartSatisListesi : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        KeykartUrun keykarturun = new KeykartUrun();
        SqlConnection baglanti, SDbaglanti;
        public decimal toplamKeyGelir = 0;
        public int toplamKeyKartAdet = 0;
        public int toplamUcretsizAdet=0;
        string connetionString;
        public int keykartad = 1;
        string birkeykart;
        int adett = 0;
        decimal aratoplam, keyKartUcreti;
        public KeyKartSatisListesi()
        {
            InitializeComponent();
        }

        private void KeyKartSatisListesi_Load(object sender, EventArgs e)
        {
            DateTime simdikiTarih = DateTime.Now;
            DateTime ilkGun = new DateTime(simdikiTarih.Year, simdikiTarih.Month, 1);
            dateTimePicker2.Value=DateTime.Now;
            dateTimePicker1.Value = ilkGun;
            SD_Connect();
            DB_Connect();
            keykartUcretiniOgren();

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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;
                //excelWorkbook = excel.Workbooks.Open(Application.StartupPath + "\\Rapor\\AboneDetayRaporu.xlsx");
                this.Cursor = Cursors.WaitCursor;
                excel.Visible = false;
                excel.DisplayAlerts = false;
                excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\KEYCARDTakipFormu.xlsx");
                //excelWorkbook = excel.Workbooks.Open(@"data\BjvOtoparkGunlukSatisRaporu.xlsx");

                //@"data\SC_DB.dat
                excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["Keycard"];
                int satirArttirimi = 10;
                
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                                     
                    excelWorksheet.Cells[i + satirArttirimi, 2] = dataGridView1.Rows[i].Cells["KartBiletNo"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 3] = dataGridView1.Rows[i].Cells["Adet"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 4] = dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 5] = dataGridView1.Rows[i].Cells["Tanim"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 6] = dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 7] = dataGridView1.Rows[i].Cells["BitisTarihi"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 8] = dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 9] = dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 10] = dataGridView1.Rows[i].Cells["Otopark"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 11] = dataGridView1.Rows[i].Cells["Personel"].Value.ToString();
                    if (dataGridView1.Rows[i].Cells["Notlar"].Value==null)
                    {
                        excelWorksheet.Cells[i + satirArttirimi, 12] = "_";
                    }
                    else excelWorksheet.Cells[i + satirArttirimi, 12] = dataGridView1.Rows[i].Cells["Notlar"].Value.ToString();


                }
                excelWorksheet.Cells[5, 12] = txtGelir.Text;
                excelWorksheet.Cells[4, 4] = txtAdet.Text;
                excelWorksheet.Cells[5, 4] = txtUcretsiz.Text;
                excelWorksheet.Cells[6, 4] = txtKalan.Text;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "KeyKartSatisRaporu" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");

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
                MessageBox.Show("Excel'e Gönderilecek Veri Bulunamadı.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        
        }

        private void btnbulgetir_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2;
            string icHatOtoPark = "IC HAT 1 OTOPARK";
            string disHatOtoPark = "DIS HAT OTOPARK";
            string rentACarOtopark = "RENT A CAR OTOPARK";
            dataGridView1.Rows.Clear();
            int satirsayisi;
            int i = 0; int z = 0;
            String StringDt1, StringDt2;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt2 = Convert.ToDateTime(StringDt2);
            string invoices = "FATURA";
            // string ozsatis = "ÖZEL SATIŞ";
            string keykart = "Key Kart";
            DateTime ilkgun;
            ilkgun = new DateTime(Dt1.Year, Dt1.Month, 1);
            dateTimePicker1.Value = ilkgun;
            //int birkeykart = 0;
            adett = 0;

            if (radioButtonIc.Checked == true)
            {
                var bireysel = (from px in db.Gelirler
                                join fx in db.GercekMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Otopark == icHatOtoPark & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.AdSoyad,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar=px.Notlar,

                                }).ToList();
                i = 0;
                z = 1;



                bireysel.ForEach(x =>
                {

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;


                    z = z + 1;
                    i = i + 1;
                });

                var firmalar = (from px in db.Gelirler
                                join fx in db.TuzelMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Otopark == icHatOtoPark & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.Unvan,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar = px.Notlar,

                                }).ToList();


                firmalar.ForEach(x =>
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;
                    z = z + 1;
                    i = i + 1;
                });

                // icKeyKartAdeti
                // icKeyKartGeliri
                // icKeyKartUcretsizAdeti
                // kalanKeykartAdeti

                var icKeyKartAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == icHatOtoPark).Sum(x => x.Adet);
                txtAdet.Text = icKeyKartAdeti.ToString();
                var icKeyKartGeliri = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == icHatOtoPark).Sum(x => x.KeyKartGeliri);
                txtGelir.Text = icKeyKartGeliri.ToString();
                var icKeyKartUcretsizAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == icHatOtoPark & x.KeyKartGeliri == 0).Count();
                txtUcretsiz.Text = icKeyKartUcretsizAdeti.ToString();
                var ix = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
                if (ix.Count() > 0)
                {
                    var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                    keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                    keykartstok.urunAdi = st.UrunAdi;
                    keykartstok.ID = st.Id;
                }
                txtKalan.Text = keykartstok.StokMiktar.ToString();

            }



            if (radioButtonDh.Checked == true)
            {
                var bireysel = (from px in db.Gelirler
                                join fx in db.GercekMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Otopark == disHatOtoPark & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.AdSoyad,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar = px.Notlar,

                                }).ToList();
                i = 0;
                z = 1;



                bireysel.ForEach(x =>
                {

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;



                    z = z + 1;
                    i = i + 1;
                });

                var firmalar = (from px in db.Gelirler
                                join fx in db.TuzelMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.Otopark == disHatOtoPark & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.Unvan,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar = px.Notlar,

                                }).ToList();


                firmalar.ForEach(x =>
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;
                    z = z + 1;
                    i = i + 1;
                });

                // disKeyKartAdeti
                // disKeyKartGeliri
                // disKeyKartUcretsizAdeti
                // kalanKeykartAdeti

                var disKeyKartAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == disHatOtoPark).Sum(x => x.Adet);
                txtAdet.Text = disKeyKartAdeti.ToString();
                var disKeyKartGeliri = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == disHatOtoPark).Sum(x => x.KeyKartGeliri);
                txtGelir.Text = disKeyKartGeliri.ToString();
                var disKeyKartUcretsizAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.Otopark == disHatOtoPark & x.KeyKartGeliri == 0).Count();
                txtUcretsiz.Text = disKeyKartUcretsizAdeti.ToString();
                var ix = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
                if (ix.Count() > 0)
                {
                    var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                    keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                    keykartstok.urunAdi = st.UrunAdi;
                    keykartstok.ID = st.Id;
                }
                txtKalan.Text = keykartstok.StokMiktar.ToString();




            }
            
            if (radioButtonTumu.Checked == true)
            {
                var bireysel = (from px in db.Gelirler
                                join fx in db.GercekMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.AdSoyad,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar = px.Notlar,

                                }).ToList();
                i = 0;
                z = 1;



                bireysel.ForEach(x =>
                {

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;


                    z = z + 1;
                    i = i + 1;
                });

                var firmalar = (from px in db.Gelirler
                                join fx in db.TuzelMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.VeriTasiyici == keykart
                                select new
                                {
                                    _kartBiletNo = px.KartBiletNo,
                                    _sureAdet = px.Sure,
                                    _AdSoyadUnvan = fx.Unvan,
                                    _tanim = px.Tanim,
                                    bastar = px.BaslangicTarihi,
                                    bttar = px.BitisTarihi,
                                    _keyKart = px.KeyKartGeliri,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _islemiYapan = px.Personel,
                                    _satisTipi = px.Status,
                                    kartAdeti = px.Adet,
                                    notlar = px.Notlar,

                                }).ToList();


                firmalar.ForEach(x =>
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Adet"].Value = x.kartAdeti;
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["BitisTarihi"].Value = x.bttar;
                    dataGridView1.Rows[i].Cells["KeyKartGeliri"].Value = x._keyKart;
                    dataGridView1.Rows[i].Cells["OdemeYontemiNet"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["Notlar"].Value = x.notlar;
                    z = z + 1;
                    i = i + 1;
                });

                // icKeyKartAdeti
                // icKeyKartGeliri
                // icKeyKartUcretsizAdeti
                // kalanKeykartAdeti

                var icKeyKartAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart").Sum(x => x.Adet);
                txtAdet.Text = icKeyKartAdeti.ToString();
                var icKeyKartGeliri = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart").Sum(x => x.KeyKartGeliri);
                txtGelir.Text = icKeyKartGeliri.ToString();
                var icKeyKartUcretsizAdeti = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == "Key Kart" & x.KeyKartGeliri == 0).Count();
                txtUcretsiz.Text = icKeyKartUcretsizAdeti.ToString();
                var ix = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
                if (ix.Count() > 0)
                {
                    var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                    keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                    keykartstok.urunAdi = st.UrunAdi;
                    keykartstok.ID = st.Id;
                }
                txtKalan.Text = keykartstok.StokMiktar.ToString();


            }
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
    }
}
