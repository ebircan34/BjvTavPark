using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace TavParkBJV
{
    public partial class OzelSatisRaporMenu : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        OzetGelir ozetgelir = new OzetGelir();
        public static string[] ozelsatisRap = new string[4];
        public static decimal[] gTop = new decimal[18];
        public static int[] sayac = new int[18];
        int i = 0;
        int j = 0;
        int sc = 0;
        int row;
        int c = 0;
        public string ozSatFormElemanlari, connetionString,sql;
        SqlConnection baglanti, SDbaglanti;

        public OzelSatisRaporMenu()
        {
            InitializeComponent();
        }

        private void btnOzelsatisForm_Click(object sender, EventArgs e)
        {

            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            //Dt2 = Convert.ToDateTime(StringDt2);
                
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\KayipBiletFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["formkayipbilet"];

           
            string sql;
            string KayipBilet = "KAYIP BİLET";
            baglanti.Open();
            sql = "Select Saat,Ext5,FisNo,KartBiletNo,GenelToplam,Notlar from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='" + KayipBilet + "'";
            SqlCommand cmd = new SqlCommand(sql, baglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            baglanti.Close();
            //dataGridViewOzelSatis.DataSource=dt;
            //MessageBox.Show(dt.Rows.Count.ToString());
            //MessageBox.Show(dt.Columns.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            excelWorksheet.Cells[i + 13, j + 1] = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns.Count == null) MessageBox.Show("Excell'e Gönderilecek Veri Bulunamadı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(dt.Rows[i][j].ToString());
                }
                excelWorksheet.Cells[11,6]=dateTimePicker1.Value.ToString("yyyy-MM-dd");


                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "KayipBiletRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_";

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
                MessageBox.Show("Kayıp Bilet İşlemi Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;


            }



            //-----









        }

        private void ozelsatisTipleriniGetir()
        {
            row = 0;
        
            string[] lineOfContents = File.ReadAllLines(@"data\OzelSatisRaporlama.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //cmbOtopark.Items.Add(tokens[0]);
                ozelsatisRap[row] = tokens[0];
                row++;

            }
        }

        private void btnZorunluBilet_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            //Dt2 = Convert.ToDateTime(StringDt2);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\ZorunluBiletFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["formZorunlubilet"];


            string sql;
            string zorunluBilet = "ZORUNLU BİLET";
            baglanti.Open();
            sql = "Select Saat,Ext5,FisNo,KartBiletNo,GenelToplam,Notlar from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='" + zorunluBilet + "'";
            SqlCommand cmd = new SqlCommand(sql, baglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            baglanti.Close();
            //dataGridViewOzelSatis.DataSource=dt;
            //MessageBox.Show(dt.Rows.Count.ToString());
            //MessageBox.Show(dt.Columns.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            excelWorksheet.Cells[i + 13, j + 1] = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns.Count == null) MessageBox.Show("Excell'e Gönderilecek Veri Bulunamadı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(dt.Rows[i][j].ToString());
                }
                excelWorksheet.Cells[11, 6] = dateTimePicker1.Value.ToString("yyyy-MM-dd");


                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "ZorunluBiletRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_";

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
                MessageBox.Show("Zorunlu Bilet İşlemi Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;


            }
        }

        private void btnExUcret_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            //Dt2 = Convert.ToDateTime(StringDt2);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\EkstraUcretOdemeFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["ekstraucret"];


            string sql;
            string EXTRAucret = "EXTRA ÜCRET";
            baglanti.Open();
            sql = "Select Saat,Ext5,FisNo,KartBiletNo,GenelToplam,Notlar from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='" + EXTRAucret + "'";
            SqlCommand cmd = new SqlCommand(sql, baglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            baglanti.Close();
            //dataGridViewOzelSatis.DataSource=dt;
            //MessageBox.Show(dt.Rows.Count.ToString());
            //MessageBox.Show(dt.Columns.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            excelWorksheet.Cells[i + 13, j + 1] = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns.Count == null) MessageBox.Show("Excell'e Gönderilecek Veri Bulunamadı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(dt.Rows[i][j].ToString());
                }
                excelWorksheet.Cells[11, 6] = dateTimePicker1.Value.ToString("yyyy-MM-dd");


                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "EkstraUcretFormRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_";

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
                MessageBox.Show("Ekstra Ücret  İşlemi Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;


            }
        }

        private void btnArBilForm_Click(object sender, EventArgs e)
        {

            DateTime Dt1, Dt2, Tarih;
            String StringDt1, StringDt2, carpark, paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            //Dt2 = Convert.ToDateTime(StringDt2);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\EkstraUcretOdemeFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["ekstraucret"];


            string sql;
            string arizalibilet = "ARIZALI BİLET";
            baglanti.Open();
            sql = "Select Saat,Ext5,FisNo,KartBiletNo,GenelToplam,Notlar from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='" + arizalibilet + "'";
            SqlCommand cmd = new SqlCommand(sql, baglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            baglanti.Close();
            //dataGridViewOzelSatis.DataSource=dt;
            //MessageBox.Show(dt.Rows.Count.ToString());
            //MessageBox.Show(dt.Columns.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            excelWorksheet.Cells[i + 13, j + 1] = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns.Count == null) MessageBox.Show("Excell'e Gönderilecek Veri Bulunamadı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(dt.Rows[i][j].ToString());
                }
                excelWorksheet.Cells[11, 6] = dateTimePicker1.Value.ToString("yyyy-MM-dd");


                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "ArizaliBiletRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_";

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
                MessageBox.Show("Arızalı Bilet İşlemi Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;


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

        private void OzelSatisRaporMenu_Load(object sender, EventArgs e)
        {
            SD_Connect();
            DB_Connect();
            ozelsatisTipleriniGetir();
        }
    }
}
