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

namespace TavParkBJV
{
    public partial class KurumsalMusteri : Form
    {
        public KurumsalMusteri()
        {
            InitializeComponent();
        }
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();

        public int az = 0;
        public bool validasyon2 =false;
        private void KurumsalMusteri_Load(object sender, EventArgs e)
        {
            BtnYeni.Enabled = true;
            btnFirmaKaydet.Enabled = false;
            btnFirmaGuncelle.Enabled = false;
            panel1.Enabled = false;
        }

        private void btnfirmaiptal_Click(object sender, EventArgs e)
        {
            BtnYeni.Enabled = true;
            btnFirmaKaydet.Enabled = false;
            btnFirmaGuncelle.Enabled = false;
            panel1.Enabled = false;
            grid1FirmaMusteri.DataSource = null;
            ClearAllText(this);
        }

        private void txtUnvan_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtFirmaPlakaNo.Focus();

            }
        }



        private void txtFirmaTelefonNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVergiKimlikNo.Focus();
            }
        }

        private void txtFirmaPlakaNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFirmaTelefonNo.Focus();
            }
        }

        private void txtVergiKimlikNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVergiDairesi.Focus();
            }
        }

        private void txtFirmaYetkilisi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfirmaEmail.Focus();
            }
        }

        private void richTextBoxFirmaAdresi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (az == 5)
                {
                    txtFirmailce.Focus();
                    az = 0;
                }

                else
                {
                    az = az + 1;
                }



            }
        }

        private void txtFirmailce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFirmasehir.Focus();
            }

        }

        private void txtFirmaPlakaNo_KeyPress(object sender, KeyPressEventArgs e)
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

            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFirmaPlakaNo_TextChanged(object sender, EventArgs e)
        {
            txtFirmaPlakaNo.Text = txtFirmaPlakaNo.Text.ToUpper();
            txtFirmaPlakaNo.SelectionStart = txtFirmaPlakaNo.Text.Length;
        }

        private void txtFirmaTelefonNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtVergiKimlikNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFirmaYetkilisi_TextChanged(object sender, EventArgs e)
        {
            txtFirmaYetkilisi.Text = txtFirmaYetkilisi.Text.ToUpper();
            txtFirmaYetkilisi.SelectionStart = txtFirmaYetkilisi.Text.Length;
        }

        private void txtFirmaYetkilisi_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFirmailce_TextChanged(object sender, EventArgs e)
        {
            txtFirmailce.Text = txtFirmailce.Text.ToUpper();
            txtFirmailce.SelectionStart = txtFirmailce.Text.Length;
        }

        private void txtFirmailce_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFirmasehir_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFirmasehir_TextChanged(object sender, EventArgs e)
        {
            txtFirmasehir.Text = txtFirmasehir.Text.ToUpper();
            txtFirmasehir.SelectionStart = txtFirmasehir.Text.Length;
        }

        private void txtfirmaEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBoxFirmaAdresi.Focus();
            }
        }

        private void txtUnvan_TextChanged(object sender, EventArgs e)
        {
            txtUnvan.Text = txtUnvan.Text.ToUpper();
            txtUnvan.SelectionStart = txtUnvan.Text.Length;
        }

        private void txtUnvan_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFTelefonNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
            richTextBoxFirmaAdresi.Text = string.Empty;

        }

        private void BtnYeni_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            btnFirmaKaydet.Enabled = true;
            BtnYeni.Enabled = true;
            btnFirmaGuncelle.Enabled = false;
            grid1FirmaMusteri.DataSource = null;
            ClearAllText(this);
            txtUnvan.Focus();   
        }

        private void txtUnvan_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUnvan.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtUnvan, "Firma Adı/Ünvanı Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtFirmaPlakaNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmaPlakaNo.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtFirmaPlakaNo, "Plaka Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtFirmaTelefonNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmaTelefonNo.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtFirmaTelefonNo, "Plaka Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtVergiKimlikNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVergiKimlikNo.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtVergiKimlikNo, "Vergi Numarası Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtFirmaYetkilisi_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmaYetkilisi.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtFirmaYetkilisi, "Firma Yetkilisi Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtfirmaEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtfirmaEmail.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtfirmaEmail, "e-mail Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void richTextBoxFirmaAdresi_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBoxFirmaAdresi.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(richTextBoxFirmaAdresi, "Adres Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtFirmailce_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmailce.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtFirmailce, "Adres/İlçe Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void txtFirmasehir_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirmasehir.Text))
            {
                //e.Cancel = true;
                // txtAd.Focus();
                errorProvider1.SetError(txtFirmasehir, "Adres/Şehir Bilgisi Boş Geçilemez!");
                validasyon2 = false;

            }
            else
            {
                // e.Cancel=true;
                //errorProvider1.SetError(txtAd,null);
                validasyon2 = true;
                errorProvider1.Clear();

            }
        }

        private void btnFirmaKaydet_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (validasyon2==true) 
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
                db.Musteriler.Add(musteriler);
                db.SaveChanges();
                db.Entry(musteriler).State = EntityState.Detached;
                tuzelMusteriler.MusteriId = yeniMusteriID;
                txtMid.Text= Convert.ToString(yeniMusteriID);
                tuzelMusteriler.Unvan=txtUnvan.Text;
                tuzelMusteriler.VergiNo= txtVergiKimlikNo.Text;
                tuzelMusteriler.PlakaNo =txtFirmaPlakaNo.Text;
                tuzelMusteriler.TelefonNo = txtFirmaTelefonNo.Text; 
                tuzelMusteriler.email=txtfirmaEmail.Text;
                tuzelMusteriler.AdresText=richTextBoxFirmaAdresi.Text;
                tuzelMusteriler.Sehir= txtFirmasehir.Text;
                tuzelMusteriler.ilce=txtFirmailce.Text; 
                tuzelMusteriler.KayitTarihi=DateTime.Now;
                tuzelMusteriler.Yetkili=txtFirmaYetkilisi.Text; 
                tuzelMusteriler.VergiDairesi=txtVergiDairesi.Text;
                db.TuzelMusteriler.Add(tuzelMusteriler);
                db.SaveChanges();
                db.Entry(tuzelMusteriler).State = EntityState.Detached;
                MessageBox.Show("Firma Kaydı Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllText(this);
                btnFirmaKaydet.Enabled = false;
                BtnYeni.Enabled = true;
                validasyon2=false;
                panel1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Zorunlu Alanlar Boş Geçilemez!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }





        }

        private void btnFirmaGuncelle_Click(object sender, EventArgs e)
        {

            if (txtFirMusteriBul.Text != string.Empty)
            {

                int id = Convert.ToInt32(txtFirMusteriBul.Text);
                var x = db.TuzelMusteriler.Find(id);
                x.Unvan = txtUnvan.Text;
                x.PlakaNo = txtFirmaPlakaNo.Text;
                x.VergiNo = txtVergiKimlikNo.Text;
                x.TelefonNo = txtFirmaTelefonNo.Text;
                x.AdresText = richTextBoxFirmaAdresi.Text;
                x.email = txtfirmaEmail.Text;
                x.ilce = txtFirmailce.Text;
                x.il = txtFirmasehir.Text;
                x.Yetkili = txtFirmaYetkilisi.Text;
                x.VergiDairesi = txtVergiDairesi.Text;  
                db.SaveChanges();
                MessageBox.Show("Güncelleme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            else
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnFirMIDAra_Click(object sender, EventArgs e)
        {

            if (txtFirMusteriBul.Text == string.Empty)
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int FmusteriNo = Convert.ToInt16(txtFirMusteriBul.Text);
                var stexist = from s in db.TuzelMusteriler where s.MusteriId == FmusteriNo select s.MusteriId;
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.TuzelMusteriler where s.MusteriId == FmusteriNo select s).First();
                    txtUnvan.Text = st.Unvan;
                    txtFirmaPlakaNo.Text = st.PlakaNo;

                    txtFirmaTelefonNo.Text = st.TelefonNo;
                    txtVergiKimlikNo.Text = st.VergiNo;
                    txtFirmaYetkilisi.Text = st.Yetkili;
                    txtFirmailce.Text = st.ilce;
                    txtFirmasehir.Text = st.il;
                    txtfirmaEmail.Text = st.email;
                    txtVergiDairesi.Text=st.VergiDairesi;
                    txtMid.Text = Convert.ToString(st.MusteriId);
                    btnFirmaGuncelle.Enabled = true;
                    btnFirmaKaydet.Enabled = false;
                    BtnYeni.Enabled = false;
                    panel1.Enabled = true;
                    btnFirmaGuncelle.Enabled=true;

                }
                else
                {
                    MessageBox.Show("Kayıt Bulunamadı","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

                             




            }

            

        }

        private void btnTumListele_Click(object sender, EventArgs e)
        {
            try
            {
                //  Block of code to try
                grid1FirmaMusteri.DataSource = db.TuzelMusteriler.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Veri Tabanı Bağlantı Hatası","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }

        private void grid1FirmaMusteri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFirMusteriBul.Text = grid1FirmaMusteri.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnFirmaTelefonNoAra_Click(object sender, EventArgs e)
        {
            if (txtFTelefonNo.Text != string.Empty)
            {
                string FtelefonNo = txtFTelefonNo.Text;
                var stexist = from s in db.TuzelMusteriler where s.TelefonNo == FtelefonNo select s.MusteriId;
               
                if (stexist.Count() > 0)
                {
                    var st = (from s in db.TuzelMusteriler where s.TelefonNo == FtelefonNo select s).First();
                    txtUnvan.Text = st.Unvan;
                    txtFirmaPlakaNo.Text = st.PlakaNo;
                    txtVergiKimlikNo.Text = st.VergiNo;
                    txtFirmaTelefonNo.Text = st.TelefonNo;
                    richTextBoxFirmaAdresi.Text = st.AdresText;
                    txtFirmailce.Text = st.ilce;
                    txtFirmasehir.Text = st.il;
                    txtVergiDairesi.Text = st.VergiDairesi; 
                    txtfirmaEmail.Text=st.email;
                    txtFirmaYetkilisi.Text = st.Yetkili;
                    txtMid.Text = Convert.ToString(st.MusteriId);
                    btnFirmaGuncelle.Enabled = true;
                    btnFirmaKaydet.Enabled = false;
                    BtnYeni.Enabled = false;
                    panel1.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Müşteri Bulunamadı","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }

            }
            else
            {
                MessageBox.Show("Telefon Numarası Boş Geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtVergiDairesi.Text = txtVergiDairesi.Text.ToUpper();
            txtVergiDairesi.SelectionStart = txtVergiDairesi.Text.Length;
        }

        private void txtVergiDairesi_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtVergiDairesi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtFirmaYetkilisi.Focus();   
                 
            
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtFirMusteriBul.Text == string.Empty)
            {
                MessageBox.Show("Lütfen Müşteri Seçimi Yapınız.");

            }
            else
            {

                DialogResult result1 = MessageBox.Show("Kayıt Silinecek eminmisin?", "Dikkat", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    int idNo = Convert.ToInt32(txtFirMusteriBul.Text);
                    var z = db.TuzelMusteriler.Find(idNo);
                    db.TuzelMusteriler.Remove(z);
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
