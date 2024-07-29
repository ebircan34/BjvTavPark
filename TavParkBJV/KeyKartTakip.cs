using DevExpress.XtraExport;
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
    public partial class KeyKartTakip : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        KeykartUrun keykarturun=  new KeykartUrun();
        public static int aratoplam;
        string connetionString, _shiftBlock, _Personel;
        public bool updateLock = false;
        public bool oemLock = false;
        public bool customerLock = false;
        public bool NewRecord = false;
        public string _register = "NULL";
        public string _status = "NULL";
        SqlConnection baglanti, SDbaglanti;
        public KeyKartTakip()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            txtMiktar.Text = "0";
            txtPersonel.Text = "";
            btnYeni.Enabled = false;
            btnKAYDET.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void txtMiktar_Enter(object sender, EventArgs e)
        {
        }

        private void txtMiktar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtPersonel.Focus();
            }
        }

        private void KeyKartTakip_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value= DateTime.Now;  
            //dateTimePicker3.Value= DateTime.Now;
            btnYeni.Enabled= true;
            btnKAYDET.Enabled= false;
            groupBox1.Enabled= false;
            btnSil.Enabled= false;
            //DateTime simdikiTarih = DateTime.Now;
            //DateTime ilkGun = new DateTime(simdikiTarih.Year, simdikiTarih.Year, 1);
            //dateTimePicker2.Value = ilkGun;
            SD_Connect();
            DB_Connect();

        }

        private void GetALL()
        {
            var Liste = db.KeykartUrun.ToList();
            dataGridView1.DataSource = Liste;
            dataGridView1.Columns[3].Visible=false; 
        }

        private void btnKAYDET_Click(object sender, EventArgs e)
        {
            int miktar =Convert.ToInt32(txtMiktar.Text);
            if (miktar == 0)
            {
                MessageBox.Show("Miktar 0 Olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtMiktar.Text == "" || txtPersonel.Text == "")
                {
                    MessageBox.Show("Boş Geçilemez Alanlar Mevcut");
                }
                else
                {

                    keykarturun.UrunAdi = "Key Kart";
                    keykarturun.EklenenMiktar = Convert.ToInt32(txtMiktar.Text);
                    keykarturun.UrunId = 1;
                    keykarturun.Personel = txtPersonel.Text;
                    keykarturun.Tarih = dateTimePicker1.Value;
                    db.KeykartUrun.Add(keykarturun);
                    db.SaveChanges();
                    
                    
                    GetALL();
                    var stexist = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
                    if (stexist.Count() > 0)
                    {
                        var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                        keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                        keykartstok.urunAdi = st.UrunAdi;
                        keykartstok.ID = st.Id;
                    }
                    int stok = 0;
                    int aratoplam = Convert.ToInt32(txtMiktar.Text);
                    stok = keykartstok.StokMiktar + aratoplam;
                    var x = db.KeyKartStok.Find(keykartstok.ID);
                    x.StokMiktar = stok;
                    db.SaveChanges();
                    MessageBox.Show("Kayıt Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMiktar.Text = "0";
                    txtPersonel.Text = "";
                    btnKAYDET.Enabled = false;
                    btnYeni.Enabled = true;
                    groupBox1.Enabled=false;
                }
            }

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            GetALL();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Kayıt Silmek için eminmisin?", "SİL", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {

                if (txtID.Text != "")
                {
                    int id = Convert.ToInt32(txtID.Text);
                    var x = db.KeykartUrun.Find(id);
                    db.KeykartUrun.Remove(x);
                    db.SaveChanges();
                    var stexist = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
                    if (stexist.Count() > 0)
                    {
                        var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                        keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                        keykartstok.urunAdi = st.UrunAdi;
                        keykartstok.ID = st.Id;
                    }
                    
                    int stok = 0;
                    aratoplam = Convert.ToInt32(txtMiktar.Text);
                    stok = keykartstok.StokMiktar - aratoplam;
                    if (stok<0)
                    {
                     MessageBox.Show("Stok Miktarı 0'dan Düşük olamaz. İşlem Başarısız","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    
                    }
                    else if (stok>0)
                    {
                        var z = db.KeyKartStok.Find(keykartstok.ID);
                        z.StokMiktar = stok;
                        db.SaveChanges();

                        MessageBox.Show("Kayıt Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetALL();
                        btnSil.Enabled = false;
                        btnYeni.Enabled = true;
                        btnKAYDET.Enabled = false;
                        txtMiktar.Text = "0";
                        if (stok==0) { MessageBox.Show("KeyKart Stok Miktarınız 0","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error); }    
                    }
                   

                }
                else
                {
                    MessageBox.Show("Lütfen Listeden Seçim Yapınız!");
                }
            }
            if (result1 == DialogResult.No)
            {
            //MessageBox.Show("")
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

        private void txtMiktar_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DB_Connect()
        {
            StreamReader oku = new StreamReader(@"data\Connection_DB.dat");
            connetionString = oku.ReadLine();
            baglanti = new SqlConnection(connetionString);
            baglanti.Open();
            //MessageBox.Show("Connection Open  !");
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            btnSil.Enabled = Enabled; btnKAYDET.Enabled = false; btnYeni.Enabled = false;
            txtMiktar.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int i, j;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\KEYCARDTakipFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["Keycard"];
            string sql;
            string KayipBilet = "KAYIP BİLET";
            baglanti.Open();
            sql = " from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='" + KayipBilet + "'";
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
                            excelWorksheet.Cells[i + 11, j + 1] = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns.Count == null) MessageBox.Show("Excell'e Gönderilecek Veri Bulunamadı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //MessageBox.Show(dt.Rows[i][j].ToString());
                }







                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "BjvOtoparkKayipBiletRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_";

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
            }
        }
    }
}
