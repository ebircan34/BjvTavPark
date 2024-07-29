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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace TavParkBJV
{
    public partial class FManuelAcma : Form
    {

        string connetionString;
        public bool updateLock = false;
        public bool oemLock = false;
        public bool updateMode = false;
        public bool NewRecord = false;
        public string _register = "NULL";
        public string _status = "NULL";
        SqlConnection baglanti, SDbaglanti;
        public int _period;

        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        CiftGecis ciftgecis = new CiftGecis();
        ManBarAcma manuelAcma = new ManBarAcma();
        public FManuelAcma()
        {
            InitializeComponent();
        }

        private void FManuelAcma_Load(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = false;
            btnSil.Enabled = false; 
            btnExcell.Enabled = true;
            comboBoxManuel.Focus();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            panel2.Enabled = false;
            try
            {
                SD_Connect();
                DB_Connect();
                OtoparkYukle();
                BariyerYukle();
                PersonelYukle();
                ManuelYukle();
            }
            catch (Exception)
            {
                MessageBox.Show("Veri Tabanı Bağlantı Hatası","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }




        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxManuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPlaka.Focus();   
        }

        private void txtPlaka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtTelefon.Focus();
            }
        }

        private void txtTelefon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtAdSoyad.Focus();
            }
        }

        private void txtAdSoyad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtKontakt.Focus();
            }
            
        }

        private void txtKontakt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtBarkodNo.Focus();
            }
        }

        private void txtBarkodNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtBelgeNo.Focus(); 
            }
        }

        private void txtBelgeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPersonel.Focus();
            }
        }

        private void cmbPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOtopark.Focus();
        }

        private void cmbOtopark_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBariyer.Focus();
        }

        private void cmbBariyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Focus();    
        }

        private void cmbONAY_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAciklama.Focus();
        }

        private void txtPlaka_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPlaka_TextChanged(object sender, EventArgs e)
        {
            txtPlaka.Text = txtPlaka.Text.ToUpper();
            txtPlaka.SelectionStart = txtPlaka.Text.Length;
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            //txtTelefon.Text = txtTelefon.Text.ToUpper();
           // txtTelefon.SelectionStart = txtTelefon.Text.Length;
        }

        private void txtAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {
            txtAdSoyad.Text = txtAdSoyad.Text.ToUpper();
            txtAdSoyad.SelectionStart = txtAdSoyad.Text.Length;
        }

        private void txtKontakt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtKontakt_TextChanged(object sender, EventArgs e)
        {
            txtKontakt.Text = txtKontakt.Text.ToUpper();
            txtKontakt.SelectionStart = txtKontakt.Text.Length;
        }

        private void txtBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBelgeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
            
            btnSil.Enabled = false;
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = false;
            btnExcell.Enabled = false;
            comboBoxManuel.SelectedIndex = -1;
            cmbPersonel.SelectedIndex = -1;
            cmbOtopark.SelectedIndex = -1;  
            cmbBariyer.SelectedIndex = -1;  
            cmbONAY.SelectedIndex = -1;

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

        private void ManuelYukle()
        {
            comboBoxManuel.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\Manuel.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                comboBoxManuel.Items.Add(tokens[0]);
            }
        }

        private void BariyerYukle()
        {
            cmbBariyer.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\Bariyer.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                cmbBariyer.Items.Add(tokens[0]);
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

        private void PersonelYukle()
        {
            cmbPersonel.Items.Clear();
            SDbaglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from PERSONAL", SDbaglanti);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cmbPersonel.Items.Add(dr["Nachname"]);

            }
            SDbaglanti.Close();
            dr.Close();
        }

        private void btnKaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (comboBoxManuel.Text!=string.Empty && txtPlaka.Text!=string.Empty && txtTelefon.Text!=string.Empty && txtAdSoyad.Text!=string.Empty && txtKontakt.Text!=string.Empty && cmbPersonel.Text!=string.Empty && cmbOtopark.Text!=string.Empty && cmbBariyer.Text!=string.Empty)
            {
                manuelAcma.ManuelTipi = comboBoxManuel.Text;
                manuelAcma.Plaka=txtPlaka.Text;
                manuelAcma.Telefon=txtTelefon.Text;
                manuelAcma.AdSoyad=txtAdSoyad.Text;
                manuelAcma.Kontakt=txtKontakt.Text;
                manuelAcma.BarkodNo = txtBarkodNo.Text;
                manuelAcma.BelgeNo=txtBelgeNo.Text;
                manuelAcma.Personel=cmbPersonel.Text;
                manuelAcma.Otopark=cmbOtopark.Text;
                manuelAcma.Bariyer = cmbBariyer.Text;
                manuelAcma.Tarih = dateTimePicker1.Value;
                manuelAcma.Saat = dateTimePicker3.Value.ToShortTimeString();  
                manuelAcma.Aciklama=txtAciklama.Text;
                manuelAcma.Onay=cmbONAY.Text;   
                db.ManBarAcma.Add(manuelAcma);
                db.SaveChanges();
                btnKaydet.Enabled = false;
                btnYeni.Enabled = true;
                btnSil.Enabled = false; 
                btnGuncelle.Enabled = false;
                MessageBox.Show("Kayıt İşlemi Tamamlandı","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllText(this);
                panel2.Enabled = false;
                var query = from item in db.ManBarAcma.Where(z => z.Tarih >= dateTimePicker1.Value && z.Tarih <= dateTimePicker5.Value)
                            select new
                            {

                                // item.MusteriId,
                                item.ID,  //0
                                item.ManuelTipi,
                                item.Tarih, //1
                                item.Saat,  //2
                                item.Plaka,  //3
                                item.Otopark,
                                item.Bariyer,
                                item.AdSoyad,
                                item.Kontakt,
                                item.Personel,
                                item.Aciklama,
                                item.BarkodNo,



                            };
                dataGridView1.DataSource = query.ToList();


            }
            else
            {
                MessageBox.Show("Zorunlu Alanlar Boş Geçilemez","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnTariheGoreListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string StringDt1, StringDt5;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            StringDt5 = dateTimePicker5.Value.ToString("yyyy-MM-dd");
            DateTime Dt1, Dt5;
            Dt1 = Convert.ToDateTime(StringDt1);
            Dt5 = Convert.ToDateTime(StringDt5);
            var query = from item in db.ManBarAcma.Where(x => x.Tarih >= Dt1 && x.Tarih <= Dt5)
                        select new
                        {

                            item.ID,  //0
                            item.ManuelTipi,
                            item.Tarih, //1
                            item.Saat,  //2
                            item.Plaka,  //3
                            item.Otopark,
                            item.Bariyer,
                            item.AdSoyad,
                            item.Kontakt,
                            item.Personel,
                            item.Aciklama,
                            item.BarkodNo,





                        };
            dataGridView1.DataSource = query.ToList();
            btnExcell.Enabled=true;
        }

        private void btnTumunuListele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataGridView1.DataSource = db.ManBarAcma.ToList();
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                oemLock = true;
                //panel2.Enabled = true;
                btnExcell.Enabled = true;

            }
        }

        private void btnKayitBulGetir_Click(object sender, EventArgs e)
        {
            updateLock = true;
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int IdNo = Convert.ToInt16(txtID.Text);
                var stexist = from s in db.ManBarAcma where s.ID == IdNo select s.ID;
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.ManBarAcma where s.ID == IdNo select s).First();
                    comboBoxManuel.Text = st.ManuelTipi;
                    txtPlaka.Text = st.Plaka;
                    txtTelefon.Text = st.Telefon;
                    txtAdSoyad.Text = st.AdSoyad;
                    txtKontakt.Text=st.Kontakt; 
                    txtBarkodNo.Text = st.BarkodNo;
                    txtBelgeNo.Text = st.BelgeNo;
                    cmbPersonel.Text = st.Personel;
                    cmbOtopark.Text = st.Otopark;   
                    cmbBariyer.Text = st.Bariyer;
                    dateTimePicker2.Value=st.Tarih.Value;
                    dateTimePicker3.Value = Convert.ToDateTime(st.Saat);
                    cmbONAY.Text = st.Onay;
                    txtAciklama.Text = st.Aciklama;
                    btnYeni.Enabled = false;
                    btnKaydet.Enabled = false;
                    btnGuncelle.Enabled = true;
                    btnSil.Enabled = true;
                    panel2.Enabled = true;
                    
                }

            }
        }

        private void btnGuncelle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtID.Text != string.Empty && updateLock == true && oemLock == true)
            {
                int id = Convert.ToInt32(txtID.Text);
                var x = db.ManBarAcma.Find(id);
                x.ManuelTipi = comboBoxManuel.Text;
                x.Plaka = txtPlaka.Text;
                x.Telefon= txtTelefon.Text;
                x.AdSoyad= txtAdSoyad.Text;
                x.Kontakt= txtKontakt.Text;
                x.BelgeNo= txtBelgeNo.Text;
                x.BarkodNo= txtBarkodNo.Text;
                x.Personel=cmbPersonel.Text;
                x.Otopark=cmbOtopark.Text;  
                x.Bariyer=cmbBariyer.Text;
                x.Tarih = dateTimePicker2.Value;
                x.Saat= dateTimePicker3.Value.ToShortTimeString(); 
                x.Onay=cmbONAY.Text;
                x.Aciklama=txtAciklama.Text; 
                db.SaveChanges();
                MessageBox.Show("Güncelleme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Enabled = false;
                btnYeni.Enabled = true;
                ClearAllText(this);
                dataGridView1.DataSource=null;
                var query = from item in db.ManBarAcma.Where(z => z.Tarih >= dateTimePicker1.Value && z.Tarih <= dateTimePicker5.Value)
                            select new
                            {

                                item.ID,  //0
                                item.ManuelTipi,
                                item.Tarih, //1
                                item.Saat,  //2
                                item.Plaka,  //3
                                item.Otopark,
                                item.Bariyer,
                                item.AdSoyad,
                                item.Kontakt,
                                item.Personel,
                                item.Aciklama,

                            };
                dataGridView1.DataSource = query.ToList();
                updateLock = false;
                oemLock = false;
                
                btnGuncelle.Enabled = false;
                btnSil.Enabled = false;

            }

        }

        private void btnSil_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Kayıt Silmek için eminmisin?", "SİL", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                int idNo = Convert.ToInt16(txtID.Text);

                var z = db.ManBarAcma.Find(idNo);
                db.ManBarAcma.Remove(z);
                db.SaveChanges();
                MessageBox.Show("Silme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Enabled = false;
                btnYeni.Enabled = true;
                var query = from item in db.ManBarAcma.Where(f => f.Tarih >= dateTimePicker1.Value && f.Tarih <= dateTimePicker5.Value)
                            select new
                            {

                                item.ID,  //0
                                item.ManuelTipi,
                                item.Tarih, //1
                                item.Saat,  //2
                                item.Plaka,  //3
                                item.Otopark,
                                item.Bariyer,
                                item.AdSoyad,
                                item.Kontakt,
                                item.Personel,
                                item.Aciklama,




                            };
                dataGridView1.DataSource = query.ToList();
                updateLock = false;
                oemLock = false;
                ClearAllText(this);

            }
        }

        private void btnIptal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel2.Enabled = false;
            btnYeni.Enabled = true;
            dataGridView1.DataSource = null;
            updateLock = false;
            oemLock = false;
            ClearAllText(this);
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false; 
        }

        private void btnKayitAra_Click(object sender, EventArgs e)
        {
            
            String dt = dateTimePicker4.Value.ToString("yyyy-MM-dd");
            DateTime dt2 = DateTime.Now;
            dt2=Convert.ToDateTime(dt);

            var query = from item in db.ManBarAcma.Where(x => x.Tarih >= dt2)
                        select new
                        {

                            item.ID,  //0
                            item.ManuelTipi,
                            item.Tarih, //1
                            item.Saat,  //2
                            item.Plaka,  //3
                            item.Otopark,
                            item.Bariyer,
                            item.AdSoyad,
                            item.Kontakt,
                            item.Personel,
                            item.Aciklama,





                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnExcell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            this.Cursor = Cursors.WaitCursor;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\ManuelForm.xlsx");

            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["Manuel"];
            int satirArttirimi = 9;
            if (dataGridView1.Rows.Count > 0)
            {
                
                
                DateTime bt;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    excelWorksheet.Cells[i + satirArttirimi, 2] = dataGridView1.Rows[i].Cells["Bariyer"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 3] = dataGridView1.Rows[i].Cells["Saat"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 4] = dataGridView1.Rows[i].Cells["BarkodNo"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 5] = dataGridView1.Rows[i].Cells["Aciklama"].Value.ToString();
                    excelWorksheet.Cells[i + satirArttirimi, 6] = dataGridView1.Rows[i].Cells["AdSoyad"].Value.ToString();
                    

                }
            }

            excelWorksheet.Cells[7, 2] = DateTime.Now.ToShortDateString();
            dataGridView1.DataSource = null;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
            saveDialog.Filter = "Excel Dosyası|*.xlsx";
            saveDialog.FileName = "BjvManuelRapor_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                excelWorksheet.SaveAs(saveDialog.FileName);

                MessageBox.Show("Rapor Excel Formatında Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            excelWorkbook.Close();
            excel.Quit();
            this.Cursor = Cursors.Default;
            btnExcell.Enabled = false;//en sonra al
        }

        private void btnYeni_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewRecord = true;
            comboBoxManuel.Focus();
            btnKaydet.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
            panel2.Enabled = true;
            btnYeni.Enabled = false;
        }

        private void btnYeni_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            NewRecord=true;
            comboBoxManuel.Focus();
            btnKaydet.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
            panel2.Enabled = true;
            btnYeni.Enabled = false;
        }

        
    }
}
