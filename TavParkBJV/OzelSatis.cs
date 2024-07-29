using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraEditors.Repository;
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
    public partial class OzelSatis : Form
    {

        string connetionString, _shiftBlock,_Personel;
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
        OzetGelir ozetgelir = new OzetGelir();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        OzelSatisBMusteriEkle frmOzelSatisHizliMusteriEkle;
        public OzelSatis()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void OdemeKasasiYukle()
        {
            cmbOdemeKasasi.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\OdemeKasasi.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                cmbOdemeKasasi.Items.Add(tokens[0]);
            }

        }
        private void BariyerYukle()
        {
            comboboxBariyer.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\Bariyer.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                comboboxBariyer.Items.Add(tokens[0]);
            }

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

        private void OzelSatisYukle()
        {
            cmbOzTanim.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\OzelSatis.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                cmbOzTanim.Items.Add(tokens[0]);
            }
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


        private void txtMidAra_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 44)
            { e.Handled = false; }
            else { e.Handled = true; }
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
            richTextBoxNot.Text = string.Empty; 
            //cmbTime.SelectedIndex = 0;
            cmbMali.SelectedIndex = -1;
            cmbOzTanim.SelectedIndex = -1;
            cmbOtopark.SelectedIndex = -1;
            cmbOdemeYontemi.SelectedIndex = -1;
            cmbOdemeKasasi.SelectedIndex = -1;
            dataGridViewOzelSatis.DataSource = null;
            btnGuncelle.Enabled = false;
            txtucret.Text = "1";


        }




            private void txtBiletNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFisNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtucret_Leave(object sender, EventArgs e)
        {
            double para;
            double toplam = 0;
            if (txtucret.Text == string.Empty)
            { 
                MessageBox.Show("Ücret Boş Geçilemez!","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtucret.Focus();   
            }
            else
            {
                para = double.Parse(txtucret.Text);

                toplam = double.Parse(cmbTime.Text) * double.Parse(txtucret.Text);
                txtucret.Text = para.ToString("N");
                txtToplam.Text= toplam.ToString("N");   
                //ondalık basamaklara ayırır ve virgğülden sonra iki basamak gösterir.
                //virgülden sonra iki basamağa bağlı kalmayabilirsiniz. N'in yanına eklediğiniz sayı kadar virgül gösterebilirsiniz.
                //mesela N1 bir virgül, N4 dört virgül gösterir.
                // Ayrıca sayının para biriminin(TL) gösterilmesini isterseniz N yerine C kullanabilirsiniz.
            }
        }

        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal para;
            decimal toplam = 0;
            if (txtucret.Text == string.Empty )
            { MessageBox.Show("Ücret Bilgisi Boş veya Sıfır Geçilemez"); }
            else
            {
                
                para = decimal.Parse(txtucret.Text);

                int adet = Convert.ToInt32(cmbTime.Text);
                toplam =  adet* decimal.Parse(txtucret.Text);
                txtucret.Text = para.ToString("N");
                txtToplam.Text = toplam.ToString("N");
                //ondalık basamaklara ayırır ve virgğülden sonra iki basamak gösterir.
                //virgülden sonra iki basamağa bağlı kalmayabilirsiniz. N'in yanına eklediğiniz sayı kadar virgül gösterebilirsiniz.
                //mesela N1 bir virgül, N4 dört virgül gösterir.
                // Ayrıca sayının para biriminin(TL) gösterilmesini isterseniz N yerine C kullanabilirsiniz.
            }
        }

        private void txtucret_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOdemeYontemi.Focus();
            }
        }

        private void cmbPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtucret.Focus();   
        }

        private void txtBiletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFisNo.Focus();
            }

        }

        private void txtFisNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
               richTextBoxNot.Focus();
            }
        }

        private void cmbMtip_SelectedIndexChanged(object sender, EventArgs e)
        {
            











        }

        private void btnMidAra_Click(object sender, EventArgs e)
        {
            if (cmbMtip.Text == "KURUMSAL")
            {
                if (txtMidAra.Text != string.Empty)
                {
                    int BmusteriNo = Convert.ToInt16(txtMidAra.Text);
                    var stexist = from s in db.TuzelMusteriler where s.MusteriId == BmusteriNo select s.MusteriId;
                    if (stexist.Count() > 0)
                    {
                        MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var st = (from s in db.TuzelMusteriler where s.MusteriId == BmusteriNo select s).First();
                        txtFirmaUnvani.Text = st.Unvan;
                        txtPlakaNo.Text = st.PlakaNo;
                        txtTcVKimlikNo.Text = st.VergiNo;
                        txtTelefonNo.Text = st.TelefonNo;
                        richTextBoxAdres.Text = st.AdresText;
                        txtilce.Text = st.ilce;
                        txtsehir.Text = st.Sehir;
                        txtemail.Text = st.email;
                        txtID.Text = txtMidAra.Text;
                        txtYetkili.Text = st.Yetkili;
                        BtnYeni.Enabled = true;
                        txtvergiDairesi.Text = st.VergiDairesi; 

                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?", "UYARI", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {

                            frmOzelSatisHizliMusteriEkle = new OzelSatisBMusteriEkle();
                            //frmHizliMusteriEkle.ShowDialog();  
                            DialogResult response = frmOzelSatisHizliMusteriEkle.ShowDialog();
                            if (response == DialogResult.OK)
                            {
                                txtMidAra.Text = frmOzelSatisHizliMusteriEkle.IndexID;
                            }
                            if (response == DialogResult.Cancel)
                            {
                                txtMidAra.Text = "";
                            }



                        }
                        else
                        {
                            MessageBox.Show("İşlem İptal Edildi");
                            txtMidAra.Text = "";
                        }
                    }



                }
            }

            if (cmbMtip.Text == "BİREYSEL")
            {
                if (txtMidAra.Text != string.Empty)
                {
                    int BmusteriNo = Convert.ToInt16(txtMidAra.Text);
                    var stexist = from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s.MusteriId;
                    if (stexist.Count() > 0)
                    {
                        MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var st = (from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s).First();
                        txtFirmaUnvani.Text = st.AdSoyad;
                        txtPlakaNo.Text = st.PlakaNo;
                        txtTcVKimlikNo.Text = st.TcKimlikNo;
                        txtTelefonNo.Text = st.TelefonNo;
                        richTextBoxAdres.Text = st.AdresText;
                        txtilce.Text = st.ilce;
                        txtsehir.Text = st.Sehir;
                        txtemail.Text = st.email;
                        txtID.Text = txtMidAra.Text;
                        txtYetkili.Text = "";
                        BtnYeni.Enabled = true;
                        customerLock = false;

                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?", "UYARI", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {

                            frmOzelSatisHizliMusteriEkle = new OzelSatisBMusteriEkle();
                            //frmHizliMusteriEkle.ShowDialog();  
                            DialogResult response = frmOzelSatisHizliMusteriEkle.ShowDialog();
                            if (response == DialogResult.OK)
                            {
                                txtMidAra.Text = frmOzelSatisHizliMusteriEkle.IndexID;
                            }
                            if (response == DialogResult.Cancel)
                            {
                                txtMidAra.Text = "";
                            }



                        }
                        else
                        {
                            MessageBox.Show("İşlem İptal Edildi");
                            txtMidAra.Text = "";
                            ClearAllText(this);
                            customerLock = false;
                        }
                    }



                }
            }
        }

        private void BtnYeni_Click(object sender, EventArgs e)
        {
            panel3.Enabled=true;
            BtnYeni.Enabled = false;
            btnKaydet.Enabled = true;
            btnGuncelle.Enabled = false;




        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
            

                if (cmbOtopark.Text != string.Empty && cmbOdemeKasasi.Text != string.Empty && cmbOzTanim.Text != string.Empty && cmbPersonel.Text != string.Empty && txtucret.Text != string.Empty && txtToplam.Text != string.Empty && cmbOdemeYontemi.Text != string.Empty && cmbMali.Text != string.Empty && txtBiletNo.Text != string.Empty && txtFisNo.Text != string.Empty)
                {
                    gelirler.MusteriId = Convert.ToInt32(txtID.Text);
                    gelirler.Tanim = cmbOzTanim.Text;
                    gelirler.SatisGeliri = Convert.ToDecimal(txtucret.Text);
                    gelirler.KeyKartGeliri = Convert.ToDecimal(txtToplam.Text);
                    gelirler.Sure = Convert.ToInt16(cmbTime.Text);
                    gelirler.AraToplam = Convert.ToDecimal(txtucret.Text);
                    gelirler.GenelToplam = Convert.ToDecimal(txtToplam.Text);
                    gelirler.BaslangicTarihi = DateTime.Now;
                    gelirler.BitisTarihi = DateTime.Now;
                    gelirler.OdemeZamani = DateTime.Now;
                    gelirler.OdemeYontemi = cmbOdemeYontemi.Text;
                    gelirler.OdemeYontemiNet = cmbOdemeYontemi.Text + "_" + cmbMali.Text;
                    gelirler.Personel = cmbPersonel.Text;
                    gelirler.Vardiya = cmbShift.Text;
                    gelirler.Notlar=richTextBoxNot.Text;
                    gelirler.InvoiceStatus = cmbMali.Text;
                    gelirler.Otopark = cmbOtopark.Text;
                    gelirler.OdemeKasasi = cmbOdemeKasasi.Text;
                    gelirler.KartBiletNo = txtBiletNo.Text;
                    gelirler.Saat = DateTime.Now.ToShortTimeString();
                if (cmbOzTanim.Text == "KEY KART")
                {

                    gelirler.Adet = Convert.ToInt32(cmbTime.Text);
                        gelirler.VeriTasiyici = "Key Kart";
                                       
                    
                }
                else
                {
                    gelirler.Adet = 0;
                    gelirler.VeriTasiyici = "_";
                }



                    //if (cmbOzTanim.Text != "KEY KART") gelirler.VeriTasiyici = "_";
                    gelirler.FisNo=txtFisNo.Text;
                    gelirler.KartBiletNo = txtBiletNo.Text;
                    gelirler.Ext5=txtislem.Text;
                    gelirler.Ext6 = comboboxBariyer.Text;

                    gelirler.Status = "ÖZEL SATIŞ";
                    string _vardiya = "Open";

                    var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;
                    int _perid, shiftID;

                    if (stexist.Count() > 0)
                    {
                        var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                        _perid = Convert.ToInt32(st.PerID);
                        shiftID = Convert.ToInt32(st.ID);
                        gelirler.PerID = _perid;
                        gelirler.VardiyaID = shiftID;
                    }

                    db.Gelirler.Add(gelirler);
                    db.SaveChanges();

                if (cmbOzTanim.Text=="KEY KART")
                {
                    KeyKartKalanHesapla();

                }


                    decimal para = decimal.Parse(txtToplam.Text);
                    txtToplam.Text = para.ToString("C");
                    para = decimal.Parse(txtucret.Text);
                    txtucret.Text = para.ToString("C");

                    MessageBox.Show("Özel Satış Geliri Kaydı Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllText(this);
                    BtnYeni.Enabled = true;
                    btnKaydet.Enabled = false;
                    cmbPersonel.SelectedIndex = -1;
                    panel3.Enabled = false;
                    txtucret.Text = "1";
                    //cmbTime.SelectedIndex = 0;

                var query = from item in db.Gelirler.Where(f => f.BaslangicTarihi >= dateTimePicker1.Value && f.Status == "ÖZEL SATIŞ")
                            select new
                            {

                                // item.MusteriId,
                                item.BaslangicTarihi,//0
                                item.Tanim,  //1
                                item.SatisGeliri, //2
                                item.Sure,  //3
                                item.AraToplam,  //4
                                item.KeyKartGeliri,  //5
                                item.GenelToplam,  //6
                                item.OdemeKasasi, //7
                                item.OdemeYontemi, //8
                                item.InvoiceStatus, //9
                                item.Otopark, //10
                                              //item.VeriTasiyici, //10
                                item.Status, //11
                                item.KartBiletNo, //12
                                item.Vardiya, //13
                                item.Id,//14


                            };
                dataGridViewOzelSatis.DataSource = query.ToList();
                txtucret.Text = "1";
                cmbTime.SelectedIndex = 0;

            }
                else
                {
                    MessageBox.Show("Zorunlu Giriş Alanlarında Boşluk Kontrolü Yapınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

        }

        private void KeyKartKalanHesapla()
        {
            // using (var keykartstok = new KeyKartStok())


            var stexist = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
            if (stexist.Count() > 0)
            {
                var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                keykartstok.urunAdi = st.UrunAdi;
                keykartstok.ID = st.Id;
            }
            
            int stok = 0;

            stok = keykartstok.StokMiktar - Convert.ToInt32(cmbTime.Text);
            //int kkstokID = 1;
            var x = db.KeyKartStok.Find(keykartstok.ID);
            x.StokMiktar = stok;
            db.SaveChanges();

            keykarthareket.Adet = Convert.ToInt32(cmbTime.Text);
            keykarthareket.AdSoyadFirmaUnvani = txtFirmaUnvani.Text;
            keykarthareket.SatisTanimi = cmbOzTanim.Text;
            keykarthareket.Urun = keykartstok.urunAdi;
            keykarthareket.VerilisTarihi = dateTimePickerOzelSatis.Value;
            keykarthareket.BitisTarihi = dateTimePickerOzelSatis.Value;
            keykarthareket.Ucret = decimal.Parse(txtToplam.Text);
            keykarthareket.OdemeYontemi = cmbOdemeYontemi.Text + "_" + cmbMali.Text;
            keykarthareket.Personel = cmbPersonel.Text;
            keykarthareket.Otopark = cmbOtopark.Text;
            keykarthareket.KalanAdet = stok;
            db.KeyKartHareket.Add(keykarthareket);
            db.SaveChanges();
            var sonid = db.KeyKartHareket.Max(f => f.Id).ToString();
            var gelirid = db.Gelirler.Max(f => f.Id).ToString();

            int gelid = Convert.ToInt32(gelirid);
            int Gid = Convert.ToInt32(sonid);

            var z = db.Gelirler.Find(gelid);
            z.Ext7 = Convert.ToString(sonid);
            db.SaveChanges();
            stok = 0;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            oemLock = true;
            txtMidAra.Text = "";
            btnOzSatisSil.Enabled = false ;
            DateTime Dt1;
            String StringDt1;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            var query = from item in db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 && x.Status=="ÖZEL SATIŞ")
                        select new
                        {

                            // item.MusteriId,
                            item.BaslangicTarihi,//0
                            item.Tanim,  //1
                            item.SatisGeliri, //2
                            item.Sure,  //3
                            item.AraToplam,  //4
                            item.KeyKartGeliri,  //5
                            item.GenelToplam,  //6
                            item.OdemeKasasi, //7
                            item.OdemeYontemi, //8
                            item.InvoiceStatus, //9
                            item.Otopark, //10
                            //item.VeriTasiyici, //10
                            item.Status, //11
                            item.KartBiletNo, //12
                            item.Vardiya, //13
                            item.Id,//14


                        };
            dataGridViewOzelSatis.DataSource = query.ToList();
            btnKaydet.Enabled=false;
        }

        private void dataGridViewOzelSatis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (oemLock == true && dataGridViewOzelSatis.RowCount > 0)
            {
                btnIDX.Text = dataGridViewOzelSatis.CurrentRow.Cells[14].Value.ToString();
                txtID.Text =btnIDX.Text;   
                updateLock=true;
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (btnIDX.Text == string.Empty && updateLock==false)
            {
                MessageBox.Show("Müşteri No Boş Olamaz! / Listeden Kayıt Seçiniz!");
            }
            else
            {



                int idNo = Convert.ToInt16(btnIDX.Text);

                var st = (from s in db.Gelirler where s.Id == idNo select s).First();
                cmbOdemeKasasi.Text = st.OdemeKasasi;
                cmbOtopark.Text = st.Otopark;
                cmbOzTanim.Text = st.Tanim;
                txtucret.Text = Convert.ToString(st.SatisGeliri);
                cmbTime.Text = Convert.ToString(st.Sure);
                txtToplam.Text = Convert.ToString(st.GenelToplam);
                cmbPersonel.Text = st.Personel;
                cmbOdemeYontemi.Text = st.OdemeYontemi;
                cmbMali.Text = st.InvoiceStatus;
                txtBiletNo.Text = st.KartBiletNo;
                cmbShift.Text=st.Vardiya;
                txtFisNo.Text = st.FisNo;
                richTextBoxNot.Text = st.Notlar;
                btnGuncelle.Enabled = true;
                btnKaydet.Enabled = false;
                BtnYeni.Enabled = false;
                panel1.Enabled = true;
                panel3.Enabled = true;
                btnOzSatisSil.Enabled = true;
                dateTimePickerOzelSatis.Value = st.BaslangicTarihi.Value;
                btnOzSatisSil.Enabled=true;
                txtKeyHareketID.Text = st.Ext7.ToString();
            }


        }

        private void btnOzSatisSil_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Kayıt Silmek için eminmisin?","SİL", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                int idNo = Convert.ToInt16(btnIDX.Text);

                KeyKartKalanGuncelle();
                var z = db.Gelirler.Find(idNo);
                db.Gelirler.Remove(z);
                db.SaveChanges();
                MessageBox.Show("Silme İşlemi Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAllText(this);
                btnKaydet.Enabled = false;
                btnGuncelle.Enabled = false;
                BtnYeni.Enabled = false;
                btnOzSatisSil.Enabled = false;
                panel3.Enabled = false;
                cmbMali.SelectedIndex = -1;
                cmbPersonel.SelectedIndex = -1;
                updateLock = false;
                txtucret.Text = "1";
                cmbTime.SelectedIndex = 0;

                var query = from item in db.Gelirler.Where(x => x.BaslangicTarihi >= dateTimePicker1.Value && x.Status == "ÖZEL SATIŞ")
                            select new
                            {

                                // item.MusteriId,
                                item.BaslangicTarihi,//0
                                item.Tanim,  //1
                                item.SatisGeliri, //2
                                item.Sure,  //3
                                item.AraToplam,  //4
                                item.KeyKartGeliri,  //5
                                item.GenelToplam,  //6
                                item.OdemeKasasi, //7
                                item.OdemeYontemi, //8
                                item.InvoiceStatus, //9
                                item.Otopark, //10
                                              //item.VeriTasiyici, //10
                                item.Status, //11
                                item.KartBiletNo, //12
                                item.Vardiya, //13
                                item.Id,//14


                            };
                dataGridViewOzelSatis.DataSource = query.ToList();

            }
            else
            {
                //No ise yapmasını istediğiniz...
                MessageBox.Show("Silme İşlemi İptal Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int i,j;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\KayipBiletFormu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["KayipBilet"];
            string sql;
            string KayipBilet = "KAYIP BİLET";
            baglanti.Open();
            sql = "Select Saat,Ext5,FisNo,KartBiletNo,GenelToplam,Ext6,Notlar from Gelirler Where BaslangicTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Tanim='"+KayipBilet+"'";
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
                MessageBox.Show("Kayıp Bilet İşlemi Bulunamadı","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }









        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            btnKaydet.Enabled = false;
            btnGuncelle.Enabled = false;
            BtnYeni.Enabled = false;
            btnOzSatisSil.Enabled = false;  
            panel3.Enabled = false;
            cmbMali.SelectedIndex = -1;
            cmbPersonel.SelectedIndex = -1;
            txtucret.Text = "1";
            txtucret.Text = "1";
            cmbTime.SelectedIndex = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cmbMtip.Text == "KURUMSAL")
            {
                if (txtTelefonAra.Text != string.Empty)
                {
                    string BmusteriTNo = txtTelefonAra.Text;
                    var stexist = from s in db.TuzelMusteriler where s.TelefonNo == BmusteriTNo select s.TelefonNo;
                    if (stexist.Count() > 0)
                    {
                        MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var st = (from s in db.TuzelMusteriler where s.TelefonNo == BmusteriTNo select s).FirstOrDefault();
                        txtFirmaUnvani.Text = st.Unvan;
                        txtPlakaNo.Text = st.PlakaNo;
                        txtTcVKimlikNo.Text = st.VergiNo;
                        txtTelefonNo.Text = st.TelefonNo;
                        richTextBoxAdres.Text = st.AdresText;
                        txtilce.Text = st.ilce;
                        txtsehir.Text = st.Sehir;
                        txtemail.Text = st.email;
                        txtID.Text = txtMidAra.Text;
                        txtYetkili.Text = st.Yetkili;
                        BtnYeni.Enabled = true;
                        txtvergiDairesi.Text = st.VergiDairesi;

                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?", "UYARI", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {

                            frmOzelSatisHizliMusteriEkle = new OzelSatisBMusteriEkle();
                            //frmHizliMusteriEkle.ShowDialog();  
                            DialogResult response = frmOzelSatisHizliMusteriEkle.ShowDialog();
                            if (response == DialogResult.OK)
                            {
                                txtMidAra.Text = frmOzelSatisHizliMusteriEkle.IndexID;
                            }
                            if (response == DialogResult.Cancel)
                            {
                                txtMidAra.Text = "";
                            }



                        }
                        else
                        {
                            MessageBox.Show("İşlem İptal Edildi");
                            txtMidAra.Text = "";
                            ClearAllText(this);
                            customerLock = false;
                        }
                        
                    }



                }
            }

            if (cmbMtip.Text == "BİREYSEL")
            {
                if (txtTelefonAra.Text != string.Empty)
                {
                    string BmusteriTNo = txtTelefonAra.Text;
                    var stexist = from s in db.GercekMusteriler where s.TelefonNo == BmusteriTNo select s.MusteriId;
                    if (stexist.Count() > 0)
                    {
                        MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var st = (from s in db.GercekMusteriler where s.TelefonNo == BmusteriTNo select s).FirstOrDefault();
                        txtFirmaUnvani.Text = st.AdSoyad;
                        txtPlakaNo.Text = st.PlakaNo;
                        txtTcVKimlikNo.Text = st.TcKimlikNo;
                        txtTelefonNo.Text = st.TelefonNo;
                        richTextBoxAdres.Text = st.AdresText;
                        txtilce.Text = st.ilce;
                        txtsehir.Text = st.Sehir;
                        txtemail.Text = st.email;
                        txtID.Text = txtMidAra.Text;
                        txtYetkili.Text = "";
                        BtnYeni.Enabled = true;
                        customerLock = false;

                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?", "UYARI", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {

                            frmOzelSatisHizliMusteriEkle = new OzelSatisBMusteriEkle();
                            //frmHizliMusteriEkle.ShowDialog();  
                            DialogResult response = frmOzelSatisHizliMusteriEkle.ShowDialog();
                            if (response == DialogResult.OK)
                            {
                                txtMidAra.Text = frmOzelSatisHizliMusteriEkle.IndexID;
                            }
                            if (response == DialogResult.Cancel)
                            {
                                txtMidAra.Text = "";
                            }



                        }
                        else
                        {
                            MessageBox.Show("İşlem İptal Edildi");
                            txtMidAra.Text = "";
                            ClearAllText(this);
                            customerLock = false;
                        }
                       
                    }



                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (updateLock == true)
            {
                int id = Convert.ToInt32(txtID.Text);
                 var x = db.Gelirler.Find(id);
                x.BaslangicTarihi = dateTimePickerOzelSatis.Value;
                x.BitisTarihi = dateTimePickerOzelSatis.Value;
                x.Otopark = cmbOtopark.Text;
                x.OdemeKasasi = cmbOdemeKasasi.Text;
                x.Tanim = cmbOzTanim.Text;
                x.SatisGeliri = Convert.ToDecimal(txtucret.Text);
                x.Sure = Convert.ToInt16(cmbTime.Text);
                x.AraToplam = Convert.ToDecimal(txtToplam.Text);
                x.GenelToplam = Convert.ToDecimal(txtToplam.Text);
                x.Personel = cmbPersonel.Text;
                x.OdemeYontemi = cmbOdemeYontemi.Text;
                x.InvoiceStatus = cmbMali.Text; ;
                x.OdemeYontemiNet = cmbOdemeYontemi.Text + "_" + cmbOdemeYontemi.Text;
                x.KartBiletNo = txtBiletNo.Text;
                x.FisNo = txtFisNo.Text;
                x.Notlar = richTextBoxNot.Text;
                x.Vardiya = cmbShift.Text;
                x.Ext5=comboboxBariyer.Text;
                x.Ext6 = txtislem.Text;
                x.Notlar=richTextBoxNot.Text;
                if (cmbOzTanim.Text == "KEY KART")
                {

                    gelirler.Adet = Convert.ToInt32(cmbTime.Text);
                        gelirler.VeriTasiyici = "Key Kart";


                }
                else
                {
                    gelirler.Adet = 0;
                    gelirler.VeriTasiyici = "_";
                }

                db.SaveChanges();



                MessageBox.Show("Güncelleme İşlemi Tamamlandı");
                ClearAllText(this);
                btnKaydet.Enabled = false;
                btnGuncelle.Enabled = false;
                BtnYeni.Enabled = false;
                btnOzSatisSil.Enabled = false;
                panel3.Enabled = false;
                cmbMali.SelectedIndex = -1;
                cmbPersonel.SelectedIndex = -1;
                updateLock = false;
                txtucret.Text = "1";
                txtucret.Text = "1";
                cmbTime.SelectedIndex = 0;


                var query = from item in db.Gelirler.Where(f => f.BaslangicTarihi >= dateTimePicker1.Value && f.Status == "ÖZEL SATIŞ")
                            select new
                            {

                                // item.MusteriId,
                                item.BaslangicTarihi,//0
                                item.Tanim,  //1
                                item.SatisGeliri, //2
                                item.Sure,  //3
                                item.AraToplam,  //4
                                item.KeyKartGeliri,  //5
                                item.GenelToplam,  //6
                                item.OdemeKasasi, //7
                                item.OdemeYontemi, //8
                                item.InvoiceStatus, //9
                                item.Otopark, //10
                                              //item.VeriTasiyici, //10
                                item.Status, //11
                                item.KartBiletNo, //12
                                item.Vardiya, //13
                                item.Id,//14


                            };
                dataGridViewOzelSatis.DataSource = query.ToList();
                btnOzSatisSil.Enabled = false;
            }
            else
            {
                MessageBox.Show("Listele İşlemini Yapınız");
            }
            



        }

        private void KeyKartKalanGuncelle()
        {
            var stexist = from s in db.KeyKartStok where s.UrunId == 1 select s.StokMiktar;
            if (stexist.Count() > 0)
            {
                var st = (from s in db.KeyKartStok where s.Id == 1 select s).First();
                keykartstok.StokMiktar = Convert.ToInt32(st.StokMiktar);
                keykartstok.urunAdi = st.UrunAdi;
                keykartstok.ID = st.Id;
            }
            int stok = 0;
            
            
            stok = keykartstok.StokMiktar + Convert.ToInt32(cmbTime.Text);
            var x = db.KeyKartStok.Find(keykartstok.ID);
            x.StokMiktar = stok;
            db.SaveChanges();
            int id = Convert.ToInt32(txtKeyHareketID.Text);
            var xz = db.KeyKartHareket.Find(id);
            db.KeyKartHareket.Remove(xz);
            db.SaveChanges();
            stok = 0;
        }

        private void OzelSatis_Load(object sender, EventArgs e)
            {
           try
           {
                BariyerYukle();
                SD_Connect();
                DB_Connect();
                OdemeKasasiYukle();
                OzelSatisYukle();
                keykartUcretiniOgren();
                OtoparkYukle();
                PersonelYukle();
                BtnYeni.Enabled = false;
                txtMidAra.Focus();
                DateTime date = DateTime.Now;
                date = date.AddDays(1);
                // panel6.Enabled = false; 
                btnGuncelle.Enabled = false;
                dateTimePicker1.Value = DateTime.Now;
                btnKaydet.Enabled = false;
                //dateTimePickerUpdate.Value = DateTime.Now;
                dateTimePickerOzelSatis.Value = DateTime.Now;
                cmbMtip.SelectedIndex = 0;
                cmbTime.SelectedIndex = 0;
                panel3.Enabled= false;  
                btnOzSatisSil.Enabled = false;
                string _vardiya = "Open";

                var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;


                if (stexist.Count() > 0)
                {
                    var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                    _shiftBlock = st.Vardiya1;
                    _Personel = st.AdSoyad;
                    cmbPersonel.Text = _Personel;
                    cmbShift.Text=_shiftBlock;


                }
              
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Kodu 005", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }


            
            }
    }
}
