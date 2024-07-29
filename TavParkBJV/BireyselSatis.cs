using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors.Filtering.Templates;
using System.Security;
using System.Diagnostics.Eventing.Reader;

namespace TavParkBJV
{
    public partial class BireyselSatis : Form
    {
        string connetionString;
        public static int TblKeyHrID;
        public static string _ksbFat,_shiftBlock;
        public bool updateLock=false;
        public bool oemLock=false;
        public bool updateMode=false;
        public bool NewRecord =false;   
        public string _register = "NULL";
        public string _status="NULL";
        SqlConnection baglanti, SDbaglanti;
        public int _period;
        public int abnAdet,mnoid;
        public decimal araToplam = 0;
        public decimal keyKartUcreti = 0; public decimal genelToplam = 0;
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        MultiMusteriEkle frmHizliMusteriEkle;
        BireyselSatis frmBireyselSatis;
        public BireyselSatis()
        {
            InitializeComponent();
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
        private void articleLoad()
        {

            cmbArticle.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from Tanimlar", baglanti);
            SqlDataReader dr_validasyon = cmd.ExecuteReader();

            while (dr_validasyon.Read())
            {
                cmbArticle.Items.Add(dr_validasyon["Tanim"]);

            }
            baglanti.Close();
            dr_validasyon.Close();
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

        private void abonelikHesapla()
        {
            araToplam = 0;
            txtTanimUcreti.Text = "0";

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from Tanimlar", baglanti);
            SqlDataReader drarticle = cmd.ExecuteReader();

            while (drarticle.Read())
            {
                if (cmbArticle.Text == drarticle[1].ToString())
                {
                    dateTimePickerBasTar.Value = DateTime.Today;
                    txtTanimUcreti.Text = drarticle[2].ToString();
                    //timerr_Count = Convert.ToInt32(drarticle[4]);
                    _period = Convert.ToInt32(drarticle[3]);
                    DateTime date = DateTime.Now;
                    _period = int.Parse(cmbAbonelikSuresi.Text) * _period;
                    date = date.AddDays(_period); // Adds days to the date
                    dateTimePickerBtTar.Value = Convert.ToDateTime(date);

                }
            }
            //textBoxOdemeTutari.Text = Convert.ToString(textBoxRevenue.Text);

            //textBoxAraToplam.Text = Convert.ToString(0);
            baglanti.Close();
            araToplam = decimal.Parse(cmbAbonelikSuresi.Text) * decimal.Parse(txtTanimUcreti.Text);
            txtAraToplam.Text = araToplam.ToString("N");
            
           



        }


        private void BireyselSatis_Load(object sender, EventArgs e)
        {
            try
            {

                SD_Connect();
                DB_Connect();
                articleLoad();
                OdemeKasasiYukle();
                keykartUcretiniOgren();
                OtoparkYukle();
                panel3.Enabled = false;
                panel4.Enabled = false;
                panel5.Enabled = false;
                BtnYeni.Enabled = false;
                
                cmbAbonelikSuresi.SelectedIndex = 0;
               
                txtMidAra.Focus();
                dateTimePicker1.Value= DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                DateTime date = DateTime.Now;
                date= date.AddDays(1);
                dateTimePicker2.Value = date;
               // panel6.Enabled = false; 
                btnONAY.Enabled = false;
                btnGuncelle.Enabled = false;
                btnONAY.Enabled=false;
                dateTimePickerUpdate.Value = DateTime.Now;
                btnKaydet.Enabled = false;
                string _vardiya = "Open";

                var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;
                

                if (stexist.Count() > 0)
                {
                    var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                    _shiftBlock = st.Vardiya1;
                    txtPersonel.Text = st.AdSoyad;
                  
                  
                }
                cmbVardiya.Text=_shiftBlock;


            }
            catch (Exception)
            {
                MessageBox.Show("Hata Kodu 001","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnMidAra_Click(object sender, EventArgs e)
        {
            string _vardiya = "Open";

            var sx = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;


            if (sx.Count() > 0)
            {
                var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                _shiftBlock = st.Vardiya1;
                txtPersonel.Text = st.AdSoyad;


            }
            cmbVardiya.Text = _shiftBlock;

            if (txtMidAra.Text != string.Empty)
            {
                int BmusteriNo = Convert.ToInt16(txtMidAra.Text);
                var stexist = from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s.MusteriId;
                if (stexist.Count() > 0)
                {
                    MessageBox.Show("Müşteri Kayıtlı","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var st = (from s in db.GercekMusteriler where s.MusteriId == BmusteriNo select s).First();
                    txtAdSoyad.Text = st.AdSoyad;
                    txtPlakaNo.Text = st.PlakaNo;
                    txtTcKimlikNo.Text = st.TcKimlikNo;
                    txtTelefonNo.Text = st.TelefonNo;
                    richTextBoxAdres.Text = st.AdresText;
                    txtilce.Text = st.ilce;
                    txtsehir.Text = st.Sehir;
                    txtemail.Text = st.email;
                    txtID.Text = txtMidAra.Text;
                    

                }
                else
                {
                    DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?","UYARI", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        
                        frmHizliMusteriEkle = new MultiMusteriEkle();
                        //frmHizliMusteriEkle.ShowDialog();  
                        DialogResult response = frmHizliMusteriEkle.ShowDialog();
                        if (response==DialogResult.OK)
                        {
                            txtMidAra.Text=frmHizliMusteriEkle.IndexID;
                        }
                        if (response == DialogResult.Cancel)
                        {
                            txtMidAra.Text = "";
                        }



                    }
                    else
                    {
                        MessageBox.Show("İşlem İptal Edildi");
                    }
                }

            }


            else
            {
                MessageBox.Show("Müşteri Numarası Boş Geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        
        BtnYeni.Enabled = true;
        
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnYeni_Click(object sender, EventArgs e)
        {
            panel4.Enabled = Enabled;
            panel5.Enabled = Enabled;
            panel6.Enabled= Enabled;
            NewRecord = true;
            btnGuncelle.Enabled = false;
            btnONAY.Enabled = false;
            btnCongressData.Enabled = true;
            btnAboneDATA.Enabled = true; 
            BtnYeni.Enabled = false;
            btnKaydet.Enabled = true;
            btnListele.Enabled = false;

        }

        private void cmbAbonelikSuresi_SelectedIndexChanged(object sender, EventArgs e)
        {
            abonelikHesapla();
            txtAboneSurei.Text = cmbAbonelikSuresi.Text;
            genelToplam = 0;
            genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
            txtOdemeTutari.Text = genelToplam.ToString("N");

        }

        private void cmbAbonelikSuresi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbAbonelikSuresi_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ksbFat=cmbArticle.Text.Trim();
            abonelikHesapla();
            genelToplam = 0;
            genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
            txtOdemeTutari.Text = genelToplam.ToString("N");
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                txtKeyKart.Text = Convert.ToString(keyKartUcreti);
                //araToplam = araToplam + keyKartUcreti;
                abonelikHesapla();
                genelToplam = 0;
                genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
                txtOdemeTutari.Text = genelToplam.ToString("N");



            }
        }

        private void radioButtonBarkod_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBarkod.Checked == true)
            {

                txtKeyKart.Text = "0";
                abonelikHesapla();
                genelToplam = 0;
                genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
                txtOdemeTutari.Text = genelToplam.ToString("N");
            }
        }

        private void radioButtonAbnYenile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAbnYenile.Checked == true)
            {
                txtKeyKart.Text = "0";
                abonelikHesapla();
                genelToplam = 0;
                genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
                txtOdemeTutari.Text = genelToplam.ToString("N");

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbArticle.Text != string.Empty && cmbInvoice.Text != string.Empty && cmbOdeYontemi.Text != string.Empty && cmbAbonelikDurumu.Text!=string.Empty && cmbOtopark.Text !=string.Empty && cmbAbonelikDurumu.Text !=string.Empty && cmbInvoice.Text!=string.Empty )
                {


                    gelirler.MusteriId = Convert.ToInt32(txtID.Text);
                    gelirler.Tanim = cmbArticle.Text;
                    gelirler.SatisGeliri = Convert.ToDecimal(txtTanimUcreti.Text);
                    gelirler.KeyKartGeliri = Convert.ToDecimal(txtKeyKart.Text);
                    gelirler.Sure = Convert.ToInt16(cmbAbonelikSuresi.Text);
                    gelirler.AraToplam = Convert.ToDecimal(txtAraToplam.Text);
                    gelirler.GenelToplam = Convert.ToDecimal(txtOdemeTutari.Text);
                    gelirler.BaslangicTarihi = dateTimePickerBasTar.Value;
                    gelirler.BitisTarihi = dateTimePickerBtTar.Value;
                    gelirler.OdemeZamani = DateTime.Now;
                    gelirler.OdemeYontemi = cmbOdeYontemi.Text;
                    gelirler.OdemeYontemiNet = cmbOdeYontemi.Text + "_" + cmbInvoice.Text;
                    gelirler.Personel = txtPersonel.Text;
                    gelirler.Vardiya = cmbVardiya.Text;
                    gelirler.InvoiceStatus = cmbInvoice.Text;
                    gelirler.Otopark = cmbOtopark.Text;
                    gelirler.OdemeKasasi = cmbOdemeKasasi.Text;
                    gelirler.KartBiletNo = txtBarkod.Text;
                    gelirler.Saat = DateTime.Now.ToShortTimeString();
                    if (radioButton1.Checked == true) 
                    {
                        gelirler.Adet = 1;
                        gelirler.VeriTasiyici = "Key Kart";
                    }
                    else
                    {
                        gelirler.Adet = 0;
                        //gelirler.VeriTasiyici = "Key Kart";
                    }

                    if (radioButtonBarkod.Checked == true) gelirler.VeriTasiyici = "Barkod";
                    if (radioButtonAbnYenile.Checked == true) gelirler.VeriTasiyici = "Abonelik Yenileme";
                    gelirler.Status = cmbAbonelikDurumu.Text;
                    string _vardiya = "Open";

                    var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;
                    int _perid, shiftID;

                    if (stexist.Count() > 0)
                    {
                     var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                      _perid=Convert.ToInt32(st.PerID);
                      shiftID=Convert.ToInt32(st.ID);
                      gelirler.PerID = _perid;
                      gelirler.VardiyaID = shiftID;
                    }
                    
                    db.Gelirler.Add(gelirler);
                    db.SaveChanges();



                    if (radioButton1.Checked == true)
                    {
                        KeyKartKalanHesapla();
                    }

                    decimal para = decimal.Parse(txtOdemeTutari.Text);
                    txtOdemeTutari.Text = para.ToString("C");
                    para = decimal.Parse(txtTanimUcreti.Text);
                    txtTanimUcreti.Text = para.ToString("C");
                    para = decimal.Parse(txtAraToplam.Text);
                    txtAraToplam.Text = para.ToString("C");
                    MessageBox.Show("Bireysel Abonelik Geliri Kaydı Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAllText(this);
                    BtnYeni.Enabled = true;
                    btnKaydet.Enabled = false;
                    panel3.Enabled = false;
                    txtTanimUcreti.Text = "0";
                    txtAboneSurei.Text = "1";
                    txtAraToplam.Text = "0";
                    txtKeyKart.Text = "0";
                    txtOdemeTutari.Text = "0";
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Zorunlu Alanlarda Seçim Yapılmamış","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           catch (Exception)
            {
                MessageBox.Show("Hata Kodu 002", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }


           



            //ondalık basamaklara ayırır ve virgğülden sonra iki basamak gösterir.
            //virgülden sonra iki basamağa bağlı kalmayabilirsiniz. N'in yanına eklediğiniz sayı kadar virgül gösterebilirsiniz.
            //mesela N1 bir virgül, N4 dört virgül gösterir.
            // Ayrıca sayının para biriminin(TL) gösterilmesini isterseniz N yerine C kullanabilirsiniz.
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
            stok = keykartstok.StokMiktar - 1;
            var x= db.KeyKartStok.Find(keykartstok.ID);
            x.StokMiktar = stok;
            db.SaveChanges();
            
            keykarthareket.Adet= Convert.ToInt16(cmbAbonelikSuresi.Text);
            keykarthareket.AdSoyadFirmaUnvani = txtAdSoyad.Text;
            keykarthareket.SatisTanimi = cmbArticle.Text;
            keykarthareket.Urun = keykartstok.urunAdi;
            keykarthareket.VerilisTarihi = dateTimePickerBasTar.Value;
            keykarthareket.BitisTarihi=dateTimePickerBtTar.Value;
            keykarthareket.Ucret=decimal.Parse(txtKeyKart.Text);
            keykarthareket.OdemeYontemi= cmbOdeYontemi.Text + "_" + cmbInvoice.Text;
            keykarthareket.Personel = txtPersonel.Text;
            keykarthareket.KalanAdet = stok;
            keykarthareket.Otopark=cmbOtopark.Text;
            db.KeyKartHareket.Add(keykarthareket);
            db.SaveChanges();

            var sonid = db.KeyKartHareket.Max(f => f.Id).ToString();
            var gelirid = db.Gelirler.Max(f => f.Id).ToString();
            
            int gelid = Convert.ToInt32(gelirid);
            int Gid= Convert.ToInt32(sonid);
            
            var z = db.Gelirler.Find(gelid);
            z.Ext7=Convert.ToString(sonid);
            db.SaveChanges();

            stok = 0;
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
            cmbAbonelikSuresi.SelectedIndex = 0;
            cmbVardiya.SelectedIndex = -1;
            cmbInvoice.SelectedIndex = -1;
            cmbArticle.SelectedIndex = -1;
            cmbOtopark.SelectedIndex = -1;
            cmbOdeYontemi.SelectedIndex = -1;    
            cmbOdemeKasasi.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButtonAbnYenile.Checked = false;
            radioButtonBarkod.Checked = false;
            panel4.Enabled = false;
            panel5.Enabled = false;
            BtnYeni.Enabled = false;
            panel6.Enabled = true;
            dataGridViewUpdate.DataSource = null;
            btnGuncelle.Enabled = false;
            btnONAY.Enabled = false;    
            oemLock=false;
            updateLock = false;


        }


            private void txtBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            //btnCongressData.Enabled = true;
            //btnAboneDATA.Enabled = true;
            btnListele.Enabled = true;
            NewRecord=false;
            oemLock = false;
            updateLock=false;
            txtTanimUcreti.Text = "0";
            txtAboneSurei.Text = "1";
            txtAraToplam.Text = "0";
            txtKeyKart.Text = "0";
            txtOdemeTutari.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            string _vardiya = "Open";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;


            if (stexist.Count() > 0)
            {
                var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                _shiftBlock = st.Vardiya1;
                txtPersonel.Text = st.AdSoyad;


            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            // btnCongressData.Enabled = true;
            // btnAboneDATA.Enabled = true;
            btnListele.Enabled = true;
            NewRecord = false;
            oemLock = false;
            updateLock = false;
            txtTanimUcreti.Text = "0";
            txtAboneSurei.Text = "1";
            txtAraToplam.Text = "0";
            txtKeyKart.Text = "0";
            txtOdemeTutari.Text = "0";
            string _vardiya = "Open";
            dateTimePicker1.Value=DateTime.Now; 
            dateTimePicker2.Value=DateTime.Now;
            var stexist = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;


            if (stexist.Count() > 0)
            {
                var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                _shiftBlock = st.Vardiya1;
                txtPersonel.Text = st.AdSoyad;


            }
        }

        private void btnCongress_Click(object sender, EventArgs e)
        { 
            oemLock=false;
            updateLock=false;
            btnONAY.Enabled = false;
            btnGuncelle.Enabled = false;
            dataGridViewUpdate.DataSource = null;
            string readText = "Select RevenuePayments.Time,PaymentWithValidationProviders.ValidationProvider,RevenuePayments.DeviceDesig,RevenuePayments.CarparkDesig,RevenuePayments.OperatorSurname,PaymentWithValidationProviders.OpenAmount,RevenuePayments.PaymentType from PaymentWithValidationProviders INNER JOIN RevenuePayments ON PaymentWithValidationProviders.TransactionNo=RevenuePayments.TransactionNo where RevenuePayments.Time>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' and RevenuePayments.InvoiceNo=PaymentWithValidationProviders.InvoiceNo";
            oemLock=false;
            updateLock = false;
            SDbaglanti.Open();
            SqlCommand cmd = new SqlCommand(readText, SDbaglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewUpdate.DataSource = dt;
            
            //MessageBox.Show("Git işlem ok");
            SDbaglanti.Close();
            //btnabnDataGonder.Enabled = false;   
            if (dt.Rows.Count > 0)
            {
                _register = "CONGRESS";
                _status = "CONGRESS";
                cmbAbonelikDurumu.Text= _status;  

            }
            else
            {
                MessageBox.Show("Listelenecek Veri Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _register = "NULL";
            }
        }

        private void dataGridViewBireyselSatis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           // if (_register == "CONGRESS")
            //{

                //cmbArticle.DropDownStyle = ComboBoxStyle.DropDown;
                //cmbOtopark.DropDownStyle = ComboBoxStyle.DropDown;
                //cmbOdemeKasasi.DropDownStyle = ComboBoxStyle.DropDown;  
                cmbArticle.Text = dataGridViewUpdate.CurrentRow.Cells[1].Value.ToString();
                cmbOtopark.Text = dataGridViewUpdate.CurrentRow.Cells[3].Value.ToString();
                cmbOdemeKasasi.Text = dataGridViewUpdate.CurrentRow.Cells[2].Value.ToString();
                txtPersonel.Text= dataGridViewUpdate.CurrentRow.Cells[4].Value.ToString();
                updateLock=true;
             
            if (_register=="CONGRESS")
                {
                    if (dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString() == "6")
                     {
                    cmbOdeYontemi.Text = "KREDI KARTI";
                     }
                    if (dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString() == "1")
                    {
                    cmbOdeYontemi.Text = "Nakit";
                    }
                }
                if (_register=="ABONE")
                {
                cmbOdeYontemi.Text = dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString();
                }

               
            //cmbArticle.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbOtopark.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbOdemeKasasi.DropDownStyle = ComboBoxStyle.DropDownList;

            //}


            // if (_register == "ABONE")
            // {

            //cmbArticle.DropDownStyle = ComboBoxStyle.DropDown;
            // cmbArticle.Text = dataGridViewBireyselSatis.CurrentRow.Cells[1].Value.ToString();
            //cmbOtopark.Text = dataGridViewBireyselSatis.CurrentRow.Cells[2].Value.ToString();
            //cmbOdemeKasasi.Text= dataGridViewBireyselSatis.CurrentRow.Cells[3].Value.ToString();

            //}

            if (_register == "NULL")
            {

                //MessageBox.Sho)
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            genelToplam = 0;
            //abonelikHesapla();
            decimal ucret = decimal.Parse(txtTanimUcreti.Text);
            decimal abone_adeti = decimal.Parse(cmbAbonelikSuresi.Text);
            araToplam=ucret*abone_adeti;    
            genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
            txtOdemeTutari.Text = genelToplam.ToString("N");
        }

        private void cmbArticle_SelectedValueChanged(object sender, EventArgs e)
        {
            //abonelikHesapla();
            //genelToplam = 0;
            //genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
            //txtOdemeTutari.Text = genelToplam.ToString("N");
        }

        private void cmbArticle_TextChanged(object sender, EventArgs e)
        {
            abonelikHesapla();
            genelToplam = 0;
            genelToplam = araToplam + decimal.Parse(txtKeyKart.Text);
            txtOdemeTutari.Text = genelToplam.ToString("N");
        }

        private void cmbArticle_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void btnCongGonder_Click(object sender, EventArgs e)
        {
            
        }

        private void btnabnDataGonder_Click(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (updateLock==true)
            {
                btnKaydet.Enabled = false;
                btnONAY.Enabled = true;
                panel5.Enabled = true;
                panel4.Enabled = true;
                btnAboneDATA.Enabled = false;  
                btnCongressData.Enabled = false;
                btnGuncelle.Enabled = false;
                BtnYeni.Enabled = false;
            }
            else
            {
                MessageBox.Show("Listeden Kayıt Seçiniz","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            oemLock=true;
            DateTime Dt1;
            String StringDt1;
            StringDt1 = dateTimePickerUpdate.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            var query = from item in db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1) 
                        select new
                        {
                            
                           // item.MusteriId,
                            item.Tanim,  //0
                            item.SatisGeliri, //1
                            item.Sure,  //2
                            item.AraToplam,  //3
                            item.KeyKartGeliri,  //4
                            item.GenelToplam,  //5
                            item.OdemeKasasi, //6
                            item.OdemeYontemi, //7
                            item.InvoiceStatus, //8
                            item.Otopark, //9
                            item.VeriTasiyici, //10
                            item.Status, //11
                            item.KartBiletNo, //12
                            item.Vardiya, //13
                            item.Id,//14
                           

                        };
            dataGridViewUpdate.DataSource = query.ToList();  
            // btnONAY.Enabled = false;
           // dataGridViewBireyselSatis.DataSource = db.Gelirler.Where(x => x.BaslangicTarihi >= dateTimePickerUpdate.Value).ToList();


        }

        private void btnONAY_Click(object sender, EventArgs e)
        {
            if (oemLock==true && updateLock==true)
            {

                if (radioButtonBarkod.Checked == true || radioButtonAbnYenile.Checked == true)
                {
                    KeyKartKalanGuncelle();

                }
                int id = Convert.ToInt32(txtID.Text);
               var x =db.Gelirler.Find(id);
               // x.MusteriId = Convert.ToInt32(txtID.Text);
                x.Tanim = cmbArticle.Text;
                x.SatisGeliri = Convert.ToDecimal(txtTanimUcreti.Text);
                x.KeyKartGeliri = Convert.ToDecimal(txtKeyKart.Text);
                x.Sure = Convert.ToInt16(cmbAbonelikSuresi.Text);
                x.AraToplam = Convert.ToDecimal(txtAraToplam.Text);
                x.GenelToplam = Convert.ToDecimal(txtOdemeTutari.Text);
                x.BaslangicTarihi = dateTimePickerBasTar.Value;
                x.BitisTarihi = dateTimePickerBtTar.Value;
                x.OdemeZamani = DateTime.Now;
                x.OdemeYontemi = cmbOdeYontemi.Text;
                x.OdemeYontemiNet = cmbOdeYontemi.Text + "_" + cmbInvoice.Text;
                x.Personel = txtPersonel.Text;
                x.Vardiya = cmbVardiya.Text;
                x.InvoiceStatus = cmbInvoice.Text;
                x.Otopark = cmbOtopark.Text;
                x.OdemeKasasi = cmbOdemeKasasi.Text;
                x.KartBiletNo = txtBarkod.Text;
                x.Saat = DateTime.Now.ToShortTimeString();
                if (radioButton1.Checked == true)
                {
                    gelirler.Adet = 1;
                    gelirler.VeriTasiyici = "Key Kart";
                }
                else
                {
                    gelirler.Adet = 0;
                    
                }

                if (radioButtonBarkod.Checked == true) x.VeriTasiyici = "Barkod";
                if (radioButtonAbnYenile.Checked == true) x.VeriTasiyici = "Abonelik Yenileme";
                x.Status = cmbAbonelikDurumu.Text;
                // gelirler.FatNo = "";
                //db.Gelirler.Add(gelirler);
                db.SaveChanges();

                




                decimal para = decimal.Parse(txtOdemeTutari.Text);
                txtOdemeTutari.Text = para.ToString("C");
                para = decimal.Parse(txtTanimUcreti.Text);
                txtTanimUcreti.Text = para.ToString("C");
                para = decimal.Parse(txtAraToplam.Text);
                txtAraToplam.Text = para.ToString("C");
                MessageBox.Show("Bireysel Abonelik Geliri Kaydı Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



                // ef core update command
                // oemLock = false;
                updateLock = false;
                btnONAY.Enabled=false;
                //ClearAllText(this);
                //dataGridViewUpdate.DataSource = null;
                btnGuncelle.Enabled=true;
                btnONAY.Enabled = false;
                btnCongressData.Enabled=true;  
                btnAboneDATA.Enabled=true;
                txtTanimUcreti.Text = "0";
                txtAboneSurei.Text = "1";
                txtAraToplam.Text = "0";
                txtKeyKart.Text = "0";
                txtOdemeTutari.Text = "0";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
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
            stok = keykartstok.StokMiktar +1;
            var x = db.KeyKartStok.Find(keykartstok.ID);
            x.StokMiktar = stok;
            db.SaveChanges();            
            int id = Convert.ToInt32(txtKeyHareketID.Text);                                  
            var xz = db.KeyKartHareket.Find(id);            
            db.KeyKartHareket.Remove(xz);
            db.SaveChanges();              
            stok = 0;
        }

        private void BireyselSatis_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void BireyselSatis_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmBireyselSatis = new BireyselSatis();
            frmBireyselSatis = null;
            this.Show();
        }

        private void txtTelefonAra_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTelefonAra_Click(object sender, EventArgs e)
        {
            string _vardiya = "Open";

            var sx = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;


            if (sx.Count() > 0)
            {
                var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                _shiftBlock = st.Vardiya1;
                txtPersonel.Text = st.AdSoyad;


            }
            cmbVardiya.Text = _shiftBlock;

            if ( txtTelefonAra.Text!= string.Empty)
            {
                string Btelefon = txtTelefonAra.Text;
                var stexist = from s in db.GercekMusteriler where s.TelefonNo == Btelefon select s.TelefonNo;
                if (stexist.Count() > 0)
                {
                    MessageBox.Show("Müşteri Kayıtlı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var st = (from s in db.GercekMusteriler where s.TelefonNo == Btelefon select s).FirstOrDefault();
                    txtAdSoyad.Text = st.AdSoyad;
                    txtPlakaNo.Text = st.PlakaNo;
                    txtTcKimlikNo.Text = st.TcKimlikNo;
                    txtTelefonNo.Text = st.TelefonNo;
                    richTextBoxAdres.Text = st.AdresText;
                    txtilce.Text = st.ilce;
                    txtsehir.Text = st.Sehir;
                    txtemail.Text = st.email;
                    txtID.Text = txtMidAra.Text;


                }
                else
                {
                    DialogResult result1 = MessageBox.Show("Müşteri Bulunamadı! Yeni Kayıt Yapılsın mı?", "UYARI", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        frmHizliMusteriEkle = new MultiMusteriEkle();
                        //frmHizliMusteriEkle.ShowDialog();  
                        DialogResult response = frmHizliMusteriEkle.ShowDialog();
                        if (response == DialogResult.OK)
                        {
                            txtMidAra.Text = frmHizliMusteriEkle.IndexID;
                        }
                        if (response == DialogResult.Cancel)
                        {
                            txtMidAra.Text = "";
                        }



                    }
                    else
                    {
                        MessageBox.Show("İşlem İptal Edildi");
                    }
                }


            }
        }

        private void dataGridViewBireyselSatis_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
           if (oemLock==true && dataGridViewUpdate.RowCount>0)
            {
                cmbArticle.Text = dataGridViewUpdate.CurrentRow.Cells[0].Value.ToString();
                cmbAbonelikSuresi.Text = dataGridViewUpdate.CurrentRow.Cells[2].Value.ToString();

                string _trim = dataGridViewUpdate.CurrentRow.Cells[10].Value.ToString();
                _trim = _trim.Trim();

                if (_trim == "Key Kart")
                {
                    radioButton1.Checked=true;
                    
                }
                if (_trim == "Barkod")
                {
                    radioButtonBarkod.Checked = true;
                }
                if (_trim == "Abonelik Yenileme")
                {
                    radioButtonAbnYenile.Checked = true;
                }
                //cmbOtopark.Text= dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString().Trim();
                cmbOdemeKasasi.Text= dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString().Trim();
                cmbOdeYontemi.Text = dataGridViewUpdate.CurrentRow.Cells[7].Value.ToString().Trim();
                cmbInvoice.Text = dataGridViewUpdate.CurrentRow.Cells[8].Value.ToString().Trim();
                cmbOtopark.Text = dataGridViewUpdate.CurrentRow.Cells[9].Value.ToString().Trim();
                cmbAbonelikDurumu.Text= dataGridViewUpdate.CurrentRow.Cells[11].Value.ToString().Trim();
                txtBarkod.Text = dataGridViewUpdate.CurrentRow.Cells[12].Value.ToString().Trim();
                cmbVardiya.Text= dataGridViewUpdate.CurrentRow.Cells[13].Value.ToString().Trim();
                txtID.Text = dataGridViewUpdate.CurrentRow.Cells[14].Value.ToString().Trim();

                if (radioButton1.Checked == true)
                {
                    int ix = Convert.ToInt32(txtID.Text);
                    var HrID = db.Gelirler.Find(ix);
                    txtKeyHareketID.Text = HrID.Ext7.ToString();
                }
                
                 

                updateMode = true; 
                updateLock=true;
                btnGuncelle.Enabled=true;
                


            }
            if (NewRecord==true && dataGridViewUpdate.Rows.Count>0)
            {
                cmbArticle.Text= dataGridViewUpdate.CurrentRow.Cells[1].Value.ToString().Trim();
                cmbOdemeKasasi.Text= dataGridViewUpdate.CurrentRow.Cells[2].Value.ToString().Trim();
                cmbOtopark.Text= dataGridViewUpdate.CurrentRow.Cells[3].Value.ToString().Trim();
                txtPersonel.Text= dataGridViewUpdate.CurrentRow.Cells[4].Value.ToString().Trim();
                if (dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString() == "6")
                {
                    cmbOdeYontemi.Text = "KREDI KARTI";
                }
                if (dataGridViewUpdate.CurrentRow.Cells[6].Value.ToString() == "1")
                {
                    cmbOdeYontemi.Text = "Nakit";
                }
               
            }













        }

        private void btnAboneDATA_Click(object sender, EventArgs e)
        {
            oemLock = false;
            updateLock = false;
            btnONAY.Enabled = false;
            btnGuncelle.Enabled = false;
            dataGridViewUpdate.DataSource = null;
            string readText = "SELECT Sgr.Time,Sgr.ArticleDesig,Sgr.DeviceDesig,Sgr.CarparkDesig,Sgr.OperatorSurname,Sgr.Revenue,Sgr.ManualPaymentMethodDesig FROM (SELECT  RevenueManualPaymentMethods.Time,RevenueSales.ArticleDesig,RevenueManualPaymentMethods.DeviceDesig,RevenueManualPaymentMethods.CarparkDesig,RevenueManualPaymentMethods.OperatorSurname,  RevenueSales.Revenue,RevenueManualPaymentMethods.ManualPaymentMethodDesig FROM RevenueManualPaymentMethods, RevenueSales WHERE (RevenueManualPaymentMethods.Time > '" + dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' AND RevenueManualPaymentMethods.Time <= '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "') AND RevenueManualPaymentMethods.TransactionNo = RevenueSales.TransactionNo AND RevenueManualPaymentMethods.DeviceNo = RevenueSales.DeviceNo AND DATEDIFF(mi, RevenueManualPaymentMethods.Time, RevenueSales.Time) = 0 UNION Select Abonelik_Uzatim_Geliri.Odeme_Zamani as Time,Abonelik_Uzatim_Geliri.Kart_Tanim_Ad as ArticleDesig ,Abonelik_Uzatim_Geliri.Cihaz_Ad as DeviceDesig , Abonelik_Uzatim_Geliri.Otopark_Ad as CarparkDesig,Abonelik_Uzatim_Geliri.Operator_Isim as OperatorSurname,Abonelik_Uzatim_Geliri.Gelir as Revenue, Abonelik_Uzatim_Geliri.OdemeTuru as ManualPaymentMethod from Abonelik_Uzatim_Geliri Where Abonelik_Uzatim_Geliri.Odeme_Zamani >='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and Abonelik_Uzatim_Geliri.Odeme_Zamani <='"+dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ) as Sgr" ;
            // string readText = "select * from RevenuePayments";
            oemLock = false;
            updateLock = false;
            SDbaglanti.Open();
            SqlCommand cmd = new SqlCommand(readText, SDbaglanti);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewUpdate.DataSource = dt;
            //MessageBox.Show("Git işlem ok");
            SDbaglanti.Close();
            //btnabnDataGonder.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                _register = "ABONE";
                _status = "ABONE";
                cmbAbonelikDurumu.Text = _status;
            }
            else
            {
                MessageBox.Show("Listelenecek Veri Bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _register = "NULL";
            }
        }
    }
}
