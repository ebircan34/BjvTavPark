using DevExpress.ClipboardSource.SpreadsheetML;
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
    public partial class MuhasebeRapor : Form
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
        tempDbx tbltempDbx = new tempDbx();

        private void personel_yukle()
        {
            comboBoxPersonel.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from BjvPersonel", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBoxPersonel.Items.Add(dr["PerAdSoyad"]);

            }
            baglanti.Close();
            dr.Close();



        }

        private void MuhasebeRapor_Load(object sender, EventArgs e)
        {
            SD_Connect();
            DB_Connect();
            personel_yukle();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }

        private void btnBulveGetir_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            Dt2 = Convert.ToDateTime(StringDt2);
            double gtop = 0;
            //dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[0].Visible=false;
            //dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataTable dt = new DataTable();
            baglanti.Open();
            SqlDataAdapter ad = new SqlDataAdapter("select BaslangicTarihi,Otopark,Sum(GenelToplam) as Gelir,Count(GenelToplam) as Adet,OdemeYontemiNet as OdemeYontemi from Gelirler where BaslangicTarihi>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and BaslangicTarihi <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' Group by BaslangicTarihi,Otopark,OdemeYontemiNet", baglanti);
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            baglanti.Close();



        }

        private void btnExcelGonder_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod,nakitfis,nakitfat,kredikartifis,kredikartifatura;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            Dt2 = Convert.ToDateTime(StringDt2);
            nakitfat = "Nakit_FATURA";
            nakitfis = "Nakit_FİŞ";
            kredikartifis = "KREDI KARTI_FİŞ";
            kredikartifatura = "KREDI KARTI_FATURA";
            string cariFat = "CARI_FATURA";
            string eftFatura = "HAVALE-EFT_FATURA";
            string icHatOtoPark = "IC HAT 1 OTOPARK";
            string disHatOtoPark = "DIS HAT OTOPARK";
            string rentACarOtopark = "RENT A CAR OTOPARK";
            int sifir = 0;
            string keykartt = "Key Kart";
            string ozsatis = "ÖZEL SATIŞ";
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
                excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\BjvOtoparkGunlukSatisRaporu.xlsx");
                //excelWorkbook = excel.Workbooks.Open(@"data\BjvOtoparkGunlukSatisRaporu.xlsx");

                //@"data\SC_DB.dat
                excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["CarparkRapor"];
                int satirArttirimi = 8;
                DateTime bt;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    bt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value);
                    excelWorksheet.Cells[i + satirArttirimi, 1] = bt.ToString("yyyy-MM-dd");
                    excelWorksheet.Cells[i + satirArttirimi, 2] = dataGridView1.Rows[i].Cells["Otopark"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 3] = dataGridView1.Rows[i].Cells["Gelir"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 4] = dataGridView1.Rows[i].Cells["Adet"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 5] = dataGridView1.Rows[i].Cells["OdemeYontemi"].Value.ToString();


                }
                excelWorksheet.Cells[1, 2] = dateTimePicker1.Value.ToString("yyyy-MM-dd") + "_" + dateTimePicker1.Value.ToString("yyyy-MM-dd");
                excelWorksheet.Cells[2, 2]=DateTime.Now.ToShortTimeString();

                //---İçhat otopark nakit fiş satış toplam geliri---
                var st = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == nakitfis & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (st != string.Empty) excelWorksheet.Cells[8, 8] = st;
                else if (st == string.Empty) excelWorksheet.Cells[8, 8] = 0;
                
                //---İçhat otopark nakit fatura satış toplam geliri---
                var nfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == nakitfat & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (nfat != string.Empty) excelWorksheet.Cells[9, 8] = nfat;
                else if (nfat == string.Empty) excelWorksheet.Cells[9, 8] = sifir.ToString();

                //---İçhat otopark kredi kartı fiş satış toplam geliri---
                var kkfis = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == kredikartifis & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (kkfis !=string.Empty) excelWorksheet.Cells[10, 8] = kkfis;
                else if (kkfis==string.Empty)excelWorksheet.Cells[10, 8] = sifir.ToString();
                
                //---İçhat otopark kredi kartı fatura  satış toplam geliri---
                var kkfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == kredikartifatura & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (kkfat != string.Empty) excelWorksheet.Cells[11, 8] = kkfat;
                else if (kkfat == string.Empty) excelWorksheet.Cells[11, 8] = 0;

                //---İçhat Otopark Havale-EFT FATURA satış toplam geliri---
                var eft = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == eftFatura & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (eft != string.Empty) excelWorksheet.Cells[12, 8] = eft;
                else if (eft == string.Empty) excelWorksheet.Cells[12, 8] = 0;

                //---İçhat Otopark Cari FATURA satış toplam geliri---
                var cari = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <=Dt2 & x.OdemeYontemiNet == cariFat & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (cari != string.Empty) excelWorksheet.Cells[13, 8] = cari;
                else if (cari == string.Empty) excelWorksheet.Cells[13, 8] = 0;
                //--- iç hat toplam tüm gelir---
                var gt = db.Gelirler.Where(x=> x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi<=Dt2 & x.Otopark==icHatOtoPark).Sum(x=> x.GenelToplam).ToString();
                if (gt != string.Empty) excelWorksheet.Cells[14,8]= gt;
                else if (gt == string.Empty) excelWorksheet.Cells[15,8] = 0;
                //---Dış Hat Otopark Nakit Fiş satış toplam geliri---
                //var st = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == nakitfis & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                //if (st != string.Empty) excelWorksheet.Cells[8, 8] = st;
                //else if (st == string.Empty) excelWorksheet.Cells[8, 8] = 0;
                //var dhnf=db.Gelirler.Where(x=>x.BaslangicTarihi>=Dt1 & x.BaslangicTarihi<=Dt2 & x.Otopark==disHatOtoPark & x.OdemeYontemiNet=nakitfis).Sum(x=> x.Ge
                var dhnf = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == nakitfis & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dhnf != string.Empty) excelWorksheet.Cells[17, 8] = dhnf;   
                else if (dhnf == string.Empty) excelWorksheet.Cells [17, 8] = 0;
                //-- DışHat Nakit Fatura---
                var dhnfat = db.Gelirler.Where(x => x.BaslangicTarihi > Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == nakitfat & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dhnfat != string.Empty) excelWorksheet.Cells[18, 8] = dhnfat;
                else if (dhnfat == string.Empty) excelWorksheet.Cells[18, 8] =0;
                //----Dışhat kredi kartı fiş gelir toplamı
                var dhkkfis = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == kredikartifis & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();   
                if ( dhkkfis != string.Empty) excelWorksheet.Cells[19,8]= dhkkfis;
                else if (dhkkfis == string.Empty) excelWorksheet.Cells[19,8] = 0;
                //---DışHat kredi kartı fatura 
                var dhkkfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == kredikartifatura & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dhkkfat != string.Empty) excelWorksheet.Cells[20, 8] = dhkkfat;
                else if (dhkkfat == string.Empty) excelWorksheet.Cells[20, 8] = 0;
                // Dış hat Cari Fatura Geliri
                var dhcrfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == cariFat & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dhcrfat != string.Empty) excelWorksheet.Cells[21, 8] = dhcrfat;
                else if (dhcrfat == string.Empty) excelWorksheet.Cells[21, 8] = 0;
                // Dış Hat Havale - EFT Fatura--
                var dheftfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.OdemeYontemiNet == eftFatura & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dheftfat != string.Empty) excelWorksheet.Cells[22, 8] = dheftfat;
                else if (dheftfat == string.Empty) excelWorksheet.Cells[22, 8] = 0;
                //--- Duş hat toplam tüm gelir---
                var dgt = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (dgt != string.Empty) excelWorksheet.Cells[23, 8] = dgt;
                else if (dgt == string.Empty) excelWorksheet.Cells[23, 8] = 0;
                //----Rent A Car Otopark Nakit Fiş Geliri
                var rnf = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == nakitfis).Sum(x => x.GenelToplam).ToString();
                if (rnf != string.Empty) excelWorksheet.Cells[26, 8] = rnf;
                else if (rnf == string.Empty) excelWorksheet.Cells[26, 8] = 0;
                //----Rent A Car Otopark  Nakit Fatura Geliri
                var rnfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == nakitfat).Sum(x => x.GenelToplam).ToString();
                if (rnfat != string.Empty) excelWorksheet.Cells[27, 8] = rnfat;
                else if (rnfat == string.Empty) excelWorksheet.Cells[27, 8] = 0;
                //----Rent A Car Otopark  Kredi Kartı Fiş Geliri
                var rkkn = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == kredikartifis).Sum(x => x.GenelToplam).ToString();
                if (rkkn != string.Empty) excelWorksheet.Cells[28, 8] = rkkn;
                else if (rkkn == string.Empty) excelWorksheet.Cells[28, 8] = 0;
                //---- Rent A Car Kredi Kartı Fatura
                var rkkfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == kredikartifatura).Sum(x => x.GenelToplam).ToString();
                if (rkkfat != string.Empty) excelWorksheet.Cells[29, 8] = rkkfat;
                else if (rkkfat == string.Empty) excelWorksheet.Cells[29, 8] = 0;
                //---- Rent A Car Cari Fatura
                var rcrfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == cariFat).Sum(x => x.GenelToplam).ToString();
                if (rcrfat != string.Empty) excelWorksheet.Cells[30, 8] = rcrfat;
                else if (rcrfat == string.Empty) excelWorksheet.Cells[30, 8] = 0;
                //---- Rent A Car Eft Fatura
                var reftfat = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.OdemeYontemiNet == eftFatura).Sum(x => x.GenelToplam).ToString();
                if (reftfat != string.Empty) excelWorksheet.Cells[30, 8] = reftfat;
                else if (reftfat == string.Empty) excelWorksheet.Cells[30, 8] = 0;
                //---- Rent A Car Toplam Gelir
                var rtg =db.Gelirler.Where(x => x.BaslangicTarihi>=Dt1 & x.BaslangicTarihi>=Dt2 & x.Otopark==rentACarOtopark).Sum(x=> x.GenelToplam).ToString() ;
                if(rtg != string.Empty) excelWorksheet.Cells[31, 8] = rtg;
                else if (rtg == string.Empty) excelWorksheet.Cells[31, 8] = 0;
                // Abonelik Harici Gelir--
                var ahg = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Status == ozsatis).Sum(x => x.GenelToplam).ToString();
                if (ahg != string.Empty) excelWorksheet.Cells[35, 8] = ahg;
                else if (rtg == string.Empty) excelWorksheet.Cells[35, 8] = 0;
                //------------------------İç Hat KeyKart Geliri
                var ickeygelir = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == keykartt & x.Otopark==icHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (ickeygelir != string.Empty) excelWorksheet.Cells[39, 8] = ickeygelir;
                else if (ickeygelir == string.Empty) excelWorksheet.Cells[39, 8] = 0;
                //------------------------
                //------------------------Dış Hat KeyKart Geliri
                var diskeygelir = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == keykartt & x.Otopark == disHatOtoPark).Sum(x => x.GenelToplam).ToString();
                if (diskeygelir != string.Empty) excelWorksheet.Cells[40, 8] = diskeygelir;
                else if (diskeygelir == string.Empty) excelWorksheet.Cells[40, 8] = 0;
                //------------------------
                //------------------------Rent A Car KeyKart Geliri
                var rentkeygelir = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.VeriTasiyici == keykartt & x.Otopark == rentACarOtopark).Sum(x => x.GenelToplam).ToString();
                if (rentkeygelir != string.Empty) excelWorksheet.Cells[41, 8] = rentkeygelir;
                else if (rentkeygelir == string.Empty) excelWorksheet.Cells[41, 8] = 0;
                //------------------------
                excelWorksheet.Cells[3, 2] = comboBoxPersonel.Text;
                




                //excelWorksheet.Columns.AutoFit();
                dataGridView1.DataSource = null;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "BjvOtoparkSatışRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
                 
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

        public MuhasebeRapor()
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
            //excelWorkbook = excel.Workbooks.Open(@"data\BjvOtoparkGunlukSatisRaporu.xlsx");
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
