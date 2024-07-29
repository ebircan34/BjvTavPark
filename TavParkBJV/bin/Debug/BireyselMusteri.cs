using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TavParkBJV
{
    public partial class BireyselMusteri : Form
    {
        public int ax = 0;
        public bool validasyon1= false;  // bireysel müşteri için boş bırakılan alan kontrolü yapar
        public BireyselMusteri()
        {
            InitializeComponent();
        }

        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        //private object errorProvider;

        private void txtAd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              txtPlakaNo.Focus();
            }
        }

        private void txtPlakaNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtTelefonNo.Focus();
            }

        }

        private void txtTelefonNo_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
               txtTcKimlikNo.Focus();
            }
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            txtAd.Text=txtAd.Text.ToUpper();
            txtAd.SelectionStart = txtAd.Text.Length;
        }

        private void BireyselMusteri_Load(object sender, EventArgs e)
        {
            txtAd.Focus();  
            btnGuncelle.Enabled = false;
            btnKaydet.Enabled = false;
            panel1.Enabled = false;

        }

        private void txtTcKimlikNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTcKimlikNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)  
            
            txtemail.Focus();
        }

        private void txtilce_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtsehir_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtilce_TextChanged(object sender, EventArgs e)
        {
            txtilce.Text = txtilce.Text.ToUpper();
            txtilce.SelectionStart = txtilce.Text.Length;
        }

        private void txtsehir_TextChanged(object sender, EventArgs e)
        {
            txtsehir.Text = txtsehir.Text.ToUpper();
            txtsehir.SelectionStart = txtsehir.Text.Length;
        }

        private void txtilce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsehir.Focus();
            }
        }
        

        private void richTextBoxAdres_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
               
                ax = ax + 1;
                if (ax == 5)
                {
                    txtilce.Focus();
                     ax = 0;
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBoxAdres.Focus();
            }
        }

        private void BtnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            BtnYeni.Enabled = false;
            panel1.Enabled = true;
            txtAd.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            BtnYeni.Enabled = true;
            btnGuncelle.Enabled = false; 
            dataGridView1.DataSource = null;
            ClearAllText(this);
            panel1.Enabled = false;
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
            richTextBoxAdres.Text = string.Empty;
       
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear(); 
            if (validasyon1==true)
            {
                              
                
                var enyuksek = db.Musteriler.Max(p => p.Id);
                int yeniMusteriID = enyuksek + 1;
                //MessageBox.Show(Convert.ToString(yeniMusteriID));

                var enyuksekMno = db.Musteriler.Max(p => p.MusteriNo);
                int yeniMusteriNo = Convert.ToInt32(enyuksekMno);
                yeniMusteriNo += 1;
                //MessageBox.Show(Convert.ToString(yeniMusteriNo));

                musteriler.Id = yeniMusteriID;
                musteriler.MusteriNo = Convert.ToString(yeniMusteriNo);
                MessageBox.Show(Convert.ToString(yeniMusteriID));
                db.Musteriler.Add(musteriler);
                db.SaveChanges();
                db.Entry(musteriler).State = EntityState.Detached;
                
                gercekMusteriler.MusteriId = yeniMusteriID;
                //txtMid.Text = Convert.ToString(yeniMusteriID);
                gercekMusteriler.AdSoyad = txtAd.Text;
                gercekMusteriler.TcKimlikNo = txtTcKimlikNo.Text;
                gercekMusteriler.PlakaNo = txtPlakaNo.Text;
                gercekMusteriler.TelefonNo = txtTelefonNo.Text;
                gercekMusteriler.AdresText = richTextBoxAdres.Text;
                gercekMusteriler.ilce = txtilce.Text;
                gercekMusteriler.Sehir = txtsehir.Text;
                gercekMusteriler.email = txtemail.Text;
                gercekMusteriler.VergiDairesi = txtVergiDairesi.Text;
                gercekMusteriler.KayitTarihi=DateTime.Now;
                db.GercekMusteriler.Add(gercekMusteriler);
                db.SaveChanges();
                db.Entry(gercekMusteriler).State = EntityState.Detached;
                MessageBox.Show("Bireysel Müşteri Kaydı Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllText(this);
                btnKaydet.Enabled = false;
                BtnYeni.Enabled = true;
                validasyon1 = false;
                panel1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Zorunlu Alanlar Boş Geçilemez!");
            }

        }

        private void txtAd_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAd.Text))
            {
                //e.Cancel = true;
               // txtAd.Focus();
                errorProvider1.SetError(txtAd, "Ad Soyad Bilgisi Boş Geçilemez!");
                validasyon1=false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon1 = true;
                errorProvider1.Clear();

            }
        }

        private void txtPlakaNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty (txtPlakaNo.Text)) 
            {
                //e.Cancel = true;
               // txtPlakaNo.Focus();
               errorProvider1.SetError(txtPlakaNo, "Plaka Bilgisi Boş Geçilemez!");
                validasyon1 = false;
            }
            else
            {
                //e.Cancel=true;
                 //errorProvider1.SetError(txtPlakaNo,null);
                errorProvider1.Clear();
                validasyon1 = true;
            }




        }

        private void txtTelefonNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefonNo.Text))
            {
               // e.Cancel = true;
               // txtTelefonNo.Focus();
                errorProvider1.SetError(txtTelefonNo, "Telefon Bilgisi Boş Geçilemez!");
                validasyon1 = false;
            }
            else
            {
                //e.Cancel = true;
                // errorProvider1.SetError(txtTelefonNo, null);
                errorProvider1.Clear();
                validasyon1 = true;
            }
        }

        private void txtTcKimlikNo_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtAd_Validated(object sender, EventArgs e)
        {
            
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            DateTime Dt1;
            String StringDt1;
            StringDt1= dateTimePickerBireysel.Value.ToString("yyyy-MM-dd"); 
            Dt1 = Convert.ToDateTime(StringDt1);
            dataGridView1.DataSource = db.GercekMusteriler.Where(x => x.KayitTarihi >= Dt1).ToList();    
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtMusteriID1.Text != string.Empty)
            {
                int id = Convert.ToInt32(txtMusteriID1.Text);
                var x = db.GercekMusteriler.Find(id);
                x.AdSoyad = txtAd.Text;
                x.PlakaNo = txtPlakaNo.Text;
                x.TcKimlikNo = txtTcKimlikNo.Text;
                x.TelefonNo = txtTelefonNo.Text;
                x.AdresText = richTextBoxAdres.Text;
                x.email = txtemail.Text;
                x.ilce = txtilce.Text;
                x.Sehir = txtsehir.Text;
                db.SaveChanges();
                MessageBox.Show("Güncelleme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnTumListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.GercekMusteriler.ToList();
        }

        private void btnMIDAra_Click(object sender, EventArgs e)
        {
            if (txtMusteriID1.Text == string.Empty)
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int BmusteriNo = Convert.ToInt16(txtMusteriID1.Text);
                var stexist = from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s.MusteriId;
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s).First();
                    txtAd.Text = st.AdSoyad;
                    txtPlakaNo.Text = st.PlakaNo;
                    txtTcKimlikNo.Text = st.TcKimlikNo;
                    txtTelefonNo.Text = st.TelefonNo;
                    richTextBoxAdres.Text = st.AdresText;
                    txtilce.Text = st.ilce;
                    txtsehir.Text = st.Sehir;
                    txtemail.Text=st.email; 
                    txtMid.Text = Convert.ToString(st.MusteriId);
                    btnGuncelle.Enabled = true;
                    btnKaydet.Enabled = false;
                    BtnYeni.Enabled = false;
                    panel1.Enabled = true;




                }
                else
                {
                    MessageBox.Show("Kayıt Bulunamadı");
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMusteriID1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btntelefonNoAra_Click(object sender, EventArgs e)
        {
            if (txtTelefonArama.Text != string.Empty)
            {
                string BtelefonNo = txtTelefonArama.Text;
                var stexist = from s in db.GercekMusteriler where s.TelefonNo == BtelefonNo select s.MusteriId;
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.GercekMusteriler where s.TelefonNo == BtelefonNo select s).First();
                    txtAd.Text = st.AdSoyad;
                    txtPlakaNo.Text = st.PlakaNo;
                    txtTcKimlikNo.Text = st.TcKimlikNo;
                    txtTelefonNo.Text = st.TelefonNo;
                    richTextBoxAdres.Text = st.AdresText;
                    txtilce.Text = st.ilce;
                    txtsehir.Text = st.Sehir;
                    txtMid.Text = Convert.ToString(st.MusteriId);
                    btnGuncelle.Enabled = true;
                    btnKaydet.Enabled = false;
                    BtnYeni.Enabled = false;
                    txtMusteriID1.Text= txtMid.Text;
                    panel1.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Müşteri Bulunamadı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Telefon Numarası Boş Geçilemez", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlakaNo_TextChanged(object sender, EventArgs e)
        {
            txtPlakaNo.Text = txtPlakaNo.Text.ToUpper();
            txtPlakaNo.SelectionStart = txtPlakaNo.Text.Length;
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtMusteriID1.Text == string.Empty)
            {
                MessageBox.Show("Lütfen Müşteri Seçimi Yapınız.");

            }
            else
            {

                DialogResult result1 = MessageBox.Show("Kayıt Silinecek eminmisin?","Dikkat", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    int idNo = Convert.ToInt32(txtMusteriID1.Text);
                    var z = db.GercekMusteriler.Find(idNo);
                    db.GercekMusteriler.Remove(z);
                    db.SaveChanges();
                    MessageBox.Show("Silme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllText(this);
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi");
                }
                
            }
        }
    }
    

}
