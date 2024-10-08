﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TavParkBJV
{
    public partial class MultiMusteriEkle : Form
    {
        public int iindex = 0;
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
        String connetionString;
        BireyselSatis frmBireyselSatis;



        public MultiMusteriEkle()
        {
            InitializeComponent();
        }

        public string IndexID
        {
            get
            {
                return iindex.ToString();
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

        private void MultiMusteriEkle_Load(object sender, EventArgs e)
        {
            //btnGonder.DialogResult = DialogResult.OK;
            panel2.Enabled =false;
            SD_Connect();
            DB_Connect();
            btnKaydet.Enabled = false;
            


        }

        private void txtAdSoyadUnvan_TextChanged(object sender, EventArgs e)
        {
            txtAdSoyadUnvan.Text = txtAdSoyadUnvan.Text.ToUpper();
            txtAdSoyadUnvan.SelectionStart = txtAdSoyadUnvan.Text.Length;
        }

        private void richTextBoxAdres_TextChanged(object sender, EventArgs e)
        {
            richTextBoxAdres.Text = richTextBoxAdres.Text.ToUpper();
            richTextBoxAdres.SelectionStart = richTextBoxAdres.Text.Length;
        }

        private void txtVergiDairesi_TextChanged(object sender, EventArgs e)
        {
            txtVergiDairesi.Text = txtVergiDairesi.Text.ToUpper();
            txtVergiDairesi.SelectionStart = txtVergiDairesi.Text.Length;
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

        private void txtTcKimlikNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtTelefonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtPlakaNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtilce_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAdSoyadUnvan_KeyDown(object sender, KeyEventArgs e)
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
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
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

        private void txtTcKimlikNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBoxAdres.Focus();
            }
        }

        private void richTextBoxAdres_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                if (iindex == 4)
                {
                    txtilce.Focus();
                }
                iindex++;
            }
            

        }

        private void txtilce_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                txtsehir.Focus();                   
                
               
            }
        }

        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);

            }
            richTextBoxAdres.Text = "";
        }

        private void txtsehir_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                txtVergiDairesi.Focus();


            }
        }

        private void btnYeni_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel2.Enabled = true;
            btnKaydet.Enabled = true;
            btnYeni.Enabled = false;
            txtAdSoyadUnvan.Focus();
        }

        private void btnKaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtAdSoyadUnvan.Text=="" || txtTelefonNo.Text=="" || txtPlakaNo.Text=="" )
            {
                MessageBox.Show("Zorunlu Alanlarda Giriş Bilgisi Eksik","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
               
                var enyuksek = db.Musteriler.Max(p => p.Id);
                int yeniMusteriID = enyuksek + 1;
                //MessageBox.Show(Convert.ToString(yeniMusteriID));

                var enyuksekMno = db.Musteriler.Max(p => p.MusteriNo);
                int yeniMusteriNo = Convert.ToInt32(enyuksekMno);
                yeniMusteriNo += 1;
                //MessageBox.Show(Convert.ToString(yeniMusteriNo));

                musteriler.Id = yeniMusteriID;
                txtMid.Text= yeniMusteriID.ToString();
                musteriler.MusteriNo = Convert.ToString(yeniMusteriNo);
               // MessageBox.Show(Convert.ToString(yeniMusteriID));
                db.Musteriler.Add(musteriler);
                db.SaveChanges();
                db.Entry(musteriler).State = EntityState.Detached;

                gercekMusteriler.MusteriId = yeniMusteriID;
                //txtMid.Text = Convert.ToString(yeniMusteriID);
                gercekMusteriler.AdSoyad = txtAdSoyadUnvan.Text;
                gercekMusteriler.TcKimlikNo = txtTcKimlikNo.Text;
                gercekMusteriler.PlakaNo = txtPlakaNo.Text;
                gercekMusteriler.TelefonNo = txtTelefonNo.Text;
                gercekMusteriler.AdresText = richTextBoxAdres.Text;
                gercekMusteriler.ilce = txtilce.Text;
                gercekMusteriler.Sehir = txtsehir.Text;
                gercekMusteriler.email = txtemail.Text;
                gercekMusteriler.VergiDairesi = txtVergiDairesi.Text;
                gercekMusteriler.KayitTarihi = DateTime.Now;
                db.GercekMusteriler.Add(gercekMusteriler);
                db.SaveChanges();
                db.Entry(gercekMusteriler).State = EntityState.Detached;
                MessageBox.Show("Bireysel Müşteri Kaydı Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ClearAllText(this);
                btnYeni.Enabled = true;
                btnKaydet.Enabled = false;
                panel2.Enabled=false;


            }

          
        }

        private void btnGonder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMid.Text == "")
            {
                MessageBox.Show("Müşteri Numarası Boş Olarak Gönderilemez","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
            else
            {
                frmBireyselSatis.txtMidAra.Text = "";
                frmBireyselSatis.txtMidAra.Text = txtMid.Text;
                //MessageBox.Show("Gönderildi");
                this.Hide();    
               // frmBireyselSatis.Show();

                //ClearAllText(this);
            }  
            
        }

        private void btniptal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearAllText(this);
            panel2.Enabled=false;
            
        }

        private void butonGonder_Click(object sender, EventArgs e)
        {
            if (txtMid.Text == "")
            {
                MessageBox.Show("Müşteri Numarası Boş Olarak Gönderilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                iindex = Convert.ToInt32(txtMid.Text);
            }
        }

        private void butonIptal_Click(object sender, EventArgs e)
        {
            iindex = 0;
        }

        private void txtPlakaNo_TextChanged(object sender, EventArgs e)
        {
            txtPlakaNo.Text = txtPlakaNo.Text.ToUpper();
            txtPlakaNo.SelectionStart = txtPlakaNo.Text.Length;
        }

        private void txtVergiDairesi_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

               


            }
        }
    }
}
