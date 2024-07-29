using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TavParkBJV
{
    public partial class FCiftGecis : Form
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
        public FCiftGecis()
        {
            InitializeComponent();
        }

        private void txtPlaka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBoxLokasyon.Focus();
            }
        }

        private void comboBoxLokasyon_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtModel.Focus();
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAciklama.Focus();
            }

        }

        private void txtAciklama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUcret.Focus();
            }
        }

        private void txtUcret_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboboxOdemeSekli.Focus();
            }

        }

        private void comboboxOdemeSekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtYapilanIslem.Focus();
        }

        private void txtYapilanIslem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPersonel.Focus();    
            }
    
        }

        private void txtPersonel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFirma.Focus();
            }

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

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAciklama_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtYapilanIslem_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPersonel_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFirma_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            txtModel.Text = txtModel.Text.ToUpper();
            txtModel.SelectionStart = txtModel.Text.Length;
        }

        private void txtAciklama_TextChanged(object sender, EventArgs e)
        {
            txtAciklama.Text=txtAciklama.Text.ToUpper();
            txtAciklama.SelectionStart = txtAciklama.Text.Length;
        }

        private void txtYapilanIslem_TextChanged(object sender, EventArgs e)
        {
            txtYapilanIslem.Text=txtYapilanIslem.Text.ToUpper();
            txtYapilanIslem.SelectionStart=txtYapilanIslem.Text.Length;
        }

        private void txtPersonel_TextChanged(object sender, EventArgs e)
        {
            txtPersonel.Text=txtPersonel.Text.ToUpper();    
            txtPersonel.SelectionStart=txtPersonel.Text.Length; 
        }

        private void txtFirma_TextChanged(object sender, EventArgs e)
        {
            txtFirma.Text=txtFirma.Text.ToUpper();  
            txtFirma.SelectionStart=txtFirma.Text.Length;   
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled=true;
            NewRecord = true;
            groupBox2.Enabled=true;
            btnYeni.Enabled=false;
            dataGridView1.DataSource=null;
            btnGuncelle.Enabled=false;
            btnsil.Enabled=false;
        }

        private void FCiftGecis_Load(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            btnsil.Enabled = false; 
            btnYeni.Enabled = true;
            btnExcell.Enabled = Enabled;
            groupBox2.Enabled = false; 
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            btnYeni.Enabled=true;
            dataGridView1.DataSource=null;
            btnExcell.Enabled = false;
            groupBox2.Enabled = false;
            btnGuncelle.Enabled=false;
            btnsil.Enabled=false;  
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text != string.Empty && comboBoxLokasyon.Text != string.Empty && txtModel.Text != string.Empty && txtAciklama.Text != string.Empty && comboboxOdemeSekli.Text != string.Empty && txtYapilanIslem.Text != string.Empty && txtPersonel.Text != string.Empty && txtFirma.Text != string.Empty)
            {
                ciftgecis.Saat=dateTimePicker4.Value.ToShortTimeString();   
                ciftgecis.Tarih = dateTimePickerCiftGecis.Value;  
                ciftgecis.Plaka =txtPlaka.Text;
                ciftgecis.Lokasyon= comboBoxLokasyon.Text;
                ciftgecis.Model=txtModel.Text;
                ciftgecis.Eylem=txtEylem.Text;
                ciftgecis.Aciklama=txtAciklama.Text;
                ciftgecis.Ucret=decimal.Parse(txtUcret.Text);   
                ciftgecis.OdemeYontemi=comboboxOdemeSekli.Text;   
                ciftgecis.YapilanIslem=txtYapilanIslem.Text;
                ciftgecis.Personel=txtPersonel.Text;
                ciftgecis.Firma = txtFirma.Text;  
                db.CiftGecis.Add(ciftgecis);
                db.SaveChanges();
                 MessageBox.Show("Veri Girişi Başarı İle Yapıldı","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information); 
                 NewRecord = false;
                 btnKaydet.Enabled = false;
                 btnYeni.Enabled = true;
                ClearAllText(this);
            }
            else
            {
                MessageBox.Show("Zorunlu Giriş Alanlarında Veri Eksikliği","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2;
            String StringDt1, StringDt2;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt2 = Convert.ToDateTime(StringDt2);
            var query = from item in db.CiftGecis.Where(x => x.Tarih >=Dt1 & x.Tarih<=Dt2 )
                        select new
                        {

                            // item.MusteriId,
                            item.ID,  //0
                            item.Tarih, //1
                            item.Saat,  //2
                            item.Plaka,  //3
                            item.Firma,  //4
                            item.Eylem,
                            item.Ucret,//5
                            
                            


                        };
            dataGridView1.DataSource = query.ToList();
        }




            private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
            if (dataGridView1.Rows.Count > 0) 
            {
             
             txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
             oemLock=true;
             groupBox2.Enabled = true; 

            }
               
            }

        private void btnKayitGetir_Click(object sender, EventArgs e)
        {
            updateLock = true;
            if (txtID.Text == string.Empty)
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int IdNo = Convert.ToInt16(txtID.Text);
                var stexist = from s in db.CiftGecis where s.ID == IdNo select s.ID;
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.CiftGecis where s.ID == IdNo select s).First();
                    dateTimePicker4.Value=Convert.ToDateTime(st.Saat);
                    dateTimePickerCiftGecis.Value=st.Tarih.Value;
                    txtPlaka.Text = st.Plaka;
                    comboBoxLokasyon.Text = st.Lokasyon;
                    comboBoxLokasyon.Text=st.Lokasyon;
                    txtModel.Text = st.Model; 
                    txtAciklama.Text = st.Aciklama;
                    txtUcret.Text=st.Ucret.ToString();
                    comboboxOdemeSekli.Text = st.OdemeYontemi;
                    txtYapilanIslem.Text = st.YapilanIslem;
                    txtPersonel.Text = st.Personel;
                    txtFirma.Text = st.Firma;                                     
                    btnGuncelle.Enabled = true;
                    btnKaydet.Enabled = false;
                    btnYeni.Enabled = false;
                    btnsil.Enabled = true ;
                    groupBox2.Enabled = true;


                }
                else
                {
                    MessageBox.Show("Kayıt Bulunamadı");
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtID.Text != string.Empty && updateLock==true && oemLock==true)
            {
                int id = Convert.ToInt32(txtID.Text);
                var x = db.CiftGecis.Find(id);
                x.Saat=dateTimePicker4.Value.ToShortTimeString();   
                x.Tarih = dateTimePickerCiftGecis.Value;
                x.Plaka = txtPlaka.Text;
                x.Lokasyon = comboBoxLokasyon.Text;
                x.Model=txtModel.Text;
                x.Aciklama = txtAciklama.Text;
                x.Ucret= decimal.Parse(txtUcret.Text);  
                x.OdemeYontemi=comboboxOdemeSekli.Text; 
                x.YapilanIslem=txtYapilanIslem.Text;
                x.Personel=txtPersonel.Text;
                x.Firma=txtFirma.Text;
                db.SaveChanges();
                MessageBox.Show("Güncelleme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Enabled= false;
                btnYeni.Enabled= true;
                var query = from item in db.CiftGecis.Where(z => z.Tarih >= dateTimePicker1.Value && z.Tarih <= dateTimePicker2.Value)
                            select new
                            {

                                // item.MusteriId,
                                item.ID,  //0
                                item.Tarih, //1
                                item.Saat,  //2
                                item.Plaka,  //3
                                item.Firma,  //4
                                item.Eylem,//5
                                item.Ucret//6




                            };
                dataGridView1.DataSource = query.ToList();
                updateLock=false;
                oemLock = false;
                ClearAllText(this);


            }
            else
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Kayıt Silmek için eminmisin?", "SİL", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                int idNo = Convert.ToInt16(txtID.Text);

                var z = db.CiftGecis.Find(idNo);
                db.CiftGecis.Remove(z);
                db.SaveChanges();
                MessageBox.Show("Silme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Enabled = false;
                btnYeni.Enabled = true;
                var query = from item in db.CiftGecis.Where(f => f.Tarih >= dateTimePicker1.Value && f.Tarih <= dateTimePicker2.Value)
                            select new
                            {

                                // item.MusteriId,
                                item.ID,  //0
                                item.Tarih, //1
                                item.Saat,  //2
                                item.Plaka,  //3
                                item.Firma,  //4
                                item.Eylem,//5
                                item.Ucret//6




                            };
                dataGridView1.DataSource = query.ToList();
                updateLock = false;
                oemLock = false;
                ClearAllText(this);

            }
        }

        private void btnExcell_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) 
            {
            DateTime Dt1, Dt2;
            // dataGridView1.Rows.Clear();
            int satirsayisi;
            int i = 0; int z = 0;
            int row = 3;
            String StringDt1, StringDt2;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt2 = Convert.ToDateTime(StringDt2);
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\CiftGecisRaporu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["CiftGecisRaporu"];

            var query = from item in db.CiftGecis.Where(x => x.Tarih >= Dt1 && x.Tarih <= Dt2)
                        select new
                        {

                            // item.MusteriId,
                            item.ID,  //0
                            item.Tarih, //1
                            item.Saat,  //2
                            item.Plaka,  //3
                            item.Firma,  //4
                            item.Eylem,
                            item.Ucret,//5
                            item.Lokasyon,
                            item.Model,
                            item.Aciklama,
                            item.OdemeYontemi,
                            item.YapilanIslem,
                            item.Personel,




                        };

            i = 1;
            //Loop Through Each Employees and Populate the worksheet
            //For Each Employee increase row by 1
            foreach (var q in query)
            {
                excelWorksheet.Cells[row, 1].Value = i;
                excelWorksheet.Cells[row, 2].Value = q.Saat;
                excelWorksheet.Cells[row, 3].Value = q.Tarih;
                excelWorksheet.Cells[row, 4].Value = q.Lokasyon;
                excelWorksheet.Cells[row, 5].Value = q.Plaka;
                excelWorksheet.Cells[row, 6].Value = q.Model;
                excelWorksheet.Cells[row, 7].Value = q.Firma;
                excelWorksheet.Cells[row, 8].Value = q.Eylem;
                excelWorksheet.Cells[row, 9].Value = q.Aciklama;
                excelWorksheet.Cells[row, 10].Value = q.Ucret;
                excelWorksheet.Cells[row, 11].Value = q.OdemeYontemi;
                excelWorksheet.Cells[row, 12].Value = q.YapilanIslem;
                excelWorksheet.Cells[row, 13].Value = q.Personel;

                row++; //Increasing the Data Row by 1
                i++;
            }
            excelWorksheet.Columns.AutoFit();
             SaveFileDialog saveDialog = new SaveFileDialog();       
            saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
            saveDialog.Filter = "Excel Dosyası|*.xlsx";
            saveDialog.FileName = "CiftGecisRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");

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

        private void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
            comboBoxLokasyon.SelectedIndex = -1;
            comboboxOdemeSekli.SelectedIndex = -1;
            dateTimePickerCiftGecis.Value=DateTime.Now;
            btnsil.Enabled=false;
            btnGuncelle.Enabled=false;
            btnKaydet.Enabled=false;
            btnExcell.Enabled=false;
            txtEylem.Text = "ÇİFT GEÇİŞ";

        }

        
    }
}
