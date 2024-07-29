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
using DevExpress.Utils.Animation;
using DevExpress.XtraVerticalGrid.ViewInfo;
using DevExpress.DirectX.Common.Direct2D;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace TavParkBJV
{
    public partial class Main : Form
    {
        public static string[] otoparklar = new string[3];
        public static string[] odemeyonteminet = new string[6];
        public static string[] ozelsatisRap= new string[4];
        public static decimal[] gTop = new decimal[18];
        public static int[] sayac = new int[18];
        public static string[] satistip = new string[3];
        string vkapat = "Close";
        string vardiyasaati, VardiyaAc, VardiyaKapat, kasa;
        public int row = 0;
        public int c = 0;
        public int col = 0;
        public int inx = 0;
        Dashboard frmDashBoard;
        FCiftGecis frmCiftGecis;
        KeyKartSatisListesi frmkeykartsatislistesi;
        BireyselMusteri frmBireyselMusteri;
        KurumsalMusteri frmKurumsalMusteri;
        BireyselSatis frmBireyselSatis;
        KurumsalSatis frmKurumsalSatis;
        OzelSatisRaporMenu frmOzelSatisRapor;
        SatisRaporu frmSatisRaporu;
        Test frmTest;
        ParkDBXEntities db = new ParkDBXEntities();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        OzelSatis frmOzelSatis;
        FManuelAcma frmManuelAcma = new FManuelAcma();
        AboneRaporu frmAboneRaporu = new AboneRaporu();
        MuhasebeRapor frmMuhasebeRaporu = new MuhasebeRapor();
        Vardiya BJVvardiya = new Vardiya();
        VardiyaRaporu frmVardiyaRaporu = new VardiyaRaporu();
        SqlConnection baglanti, SDbaglanti;
        string connetionString;
        OzetGelir ozetgelir = new OzetGelir();
        tempDbx tempdbx = new tempDbx();
        KeyKartXtraForm frmkeykartform = new KeyKartXtraForm();
        KeyKartTakip frmkeykarttakip = new KeyKartTakip();
        public Main()
        {
            InitializeComponent();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                StreamReader oku = new StreamReader(@"data\SC_DB.dat");
                connetionString = oku.ReadLine();
                SDbaglanti = new SqlConnection(connetionString);
                SDbaglanti.Open();
                MessageBox.Show("SKIDATA Bağlantısı OK  !");
                SDbaglanti.Close();
            }
            catch  
            { 
            MessageBox.Show("SKIDATA Bağlantı Hatası","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmManuelAcma = new FManuelAcma();
            frmManuelAcma.ShowDialog();
            frmManuelAcma = null;
            this.Show();
        }

        private void btnDasboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmDashBoard == null)
            {
                frmDashBoard = new Dashboard();
                frmDashBoard.MdiParent = this;
                frmDashBoard.FormClosed += new FormClosedEventHandler(FrmDashboard_FormClosed);
                frmDashBoard.Show();
            }
            else
            {
                frmDashBoard.Activate();
            }
        }

        private void FrmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmDashBoard = null;
        }

        private void btnBireyselMusteri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.Hide();
            //frmBireyselMusteri = new BireyselMusteri();
            //frmBireyselMusteri.ShowDialog();
            //frmBireyselMusteri = null;
            //this.Show();


            if (frmBireyselMusteri == null)
            {
                frmBireyselMusteri = new BireyselMusteri();
                frmBireyselMusteri.MdiParent = this;
                frmBireyselMusteri.FormClosed += new FormClosedEventHandler(frmBireyselMusteri_FormClosed);
                frmBireyselMusteri.Show();
            }
            else
            {
                frmBireyselMusteri.Activate();
            }
        }

        private void frmBireyselMusteri_FormClosed(object sender, FormClosedEventArgs e)
        {
           frmBireyselMusteri = null;
        }

        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //this.Hide();
            //frmKurumsalMusteri = new KurumsalMusteri();
            //frmKurumsalMusteri.ShowDialog();
            //frmKurumsalMusteri = null;
            //this.Show();

            if (frmKurumsalMusteri == null)
            {
                frmKurumsalMusteri = new KurumsalMusteri();
                frmKurumsalMusteri.MdiParent = this;
                frmKurumsalMusteri.FormClosed += new FormClosedEventHandler(frmKurumsalMusteri_FormClosed);
                frmKurumsalMusteri.Show();
            }
            else
            {
              frmKurumsalMusteri.Activate();
            }

        }
        private void frmKurumsalMusteri_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmKurumsalMusteri = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBireyselAbone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Hide();
            frmBireyselSatis = new BireyselSatis();
            frmBireyselSatis.ShowDialog();
            frmBireyselSatis = null;
            this.Show();
        }

        private void btnKurumsalAbone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmKurumsalSatis = new KurumsalSatis();
            frmKurumsalSatis.ShowDialog();
            frmKurumsalSatis = null;
            this.Show();
        }

        private void btnOzelSatis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmOzelSatis = new OzelSatis();
            frmOzelSatis.ShowDialog();
            frmOzelSatis = null;
            this.Show();
        }

        private void btnCiftGecis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmCiftGecis == null)
            {
                frmCiftGecis = new FCiftGecis();
                frmCiftGecis.MdiParent = this;
                frmCiftGecis.FormClosed += new FormClosedEventHandler(frmCiftGecis_FormClosed);
                frmCiftGecis.Show();
            }
            else
            {
                frmCiftGecis.Activate();
            }
        }
        private void frmCiftGecis_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmCiftGecis = null;


        }

        private void otoparklariGetir()
        {
            row = 0;
            //cmbOtopark.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\Carpark.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //cmbOtopark.Items.Add(tokens[0]);
                otoparklar[row] = tokens[0];
                row++;
            }
        }

        private void odemeYontemleriniGetir()
        {
            row = 0;
            //cmbOtopark.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\OdemeYontemiDetayi.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //cmbOtopark.Items.Add(tokens[0]);
                odemeyonteminet[row] = tokens[0];
                row++;

            }
        }

        private void satisTipleriniGetir()
        {
            row = 0;
            //cmbOtopark.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\satistipi.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //cmbOtopark.Items.Add(tokens[0]);
                satistip[row] = tokens[0];
                row++;

            }
        }


        private void OzetRaporDByaz()
        {
         


        }

        private void btnVardiyaKapa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

            
            
            int id = Convert.ToInt32(label5.Text);
            

            

            DialogResult result1 = MessageBox.Show("Vardiya Kapanışı için eminmisin?", "UYARI", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result1 == DialogResult.Yes)
            {
                 

                DateTime Dt1, Dt2, Tarih;
                String StringDt1, StringDt2, carpark, paymentmethod;
                //StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                //StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                //Dt1 = Convert.ToDateTime(StringDt1);
                //Dt2 = Convert.ToDateTime(StringDt2);
                otoparklariGetir();
                odemeYontemleriniGetir();
                int Perid;
                int VarId = 0;
                string vardiyaDurumu = "Open";
                string PerAd, VarSaat;
                String OpTime;
                String ClTime;

                //var stexistx = from s in db.Vardiya where s.VStatus == vardiyaDurumu select s.VStatus;

                //if (stexistx.Count() > 0)
                //{
                // var st = (from s in db.Vardiya where s.VStatus == vardiyaDurumu  select s).First();

                Svardiya svardiya = new Svardiya();
                var st = db.Vardiya.Where(v => v.VStatus == vardiyaDurumu);
                foreach (var item in st)
                {
                    svardiya.VPerid = Convert.ToInt32(item.PerID);
                    svardiya.Varid = Convert.ToInt32(item.ID);
                    svardiya.VPeradSoyad = item.AdSoyad;
                    svardiya.Vtarih = Convert.ToDateTime(item.OpenTime);
                    svardiya.varsaat = item.Vardiya1;

                }


                //}

                string odeme;

                for (int i = 0; i < 3; i++)
                {
                    // MessageBox.Show(otoparklar[i].ToString());
                    for (int j = 0; j < 6; j++)
                    {
                        carpark = otoparklar[i];
                        odeme = odemeyonteminet[j];
                        var result = db.Gelirler.Where(tp => tp.Otopark == carpark & tp.OdemeYontemiNet == odeme & tp.VardiyaID==id).Sum(tp => tp.GenelToplam).ToString();
                        if (result == string.Empty)
                        {
                            gTop[c] = 0;
                            sayac[c] = 0;
                            c++;
                        }
                        else
                        {
                            var say = db.Gelirler.Where(tp => tp.Otopark == carpark & tp.OdemeYontemiNet == odeme & tp.VardiyaID == id).Count();
                            gTop[c] = Convert.ToDecimal(result);
                            sayac[c] = say;
                            c++;
                        }



                    }

                }
                

                ////// Vardiya Tablosu İçin Close işlemini yap////

                var x = db.Vardiya.Find(id);
                string ClosedDate = DateTime.Now.ToString();
                string PerSAdSoyad, VClosedDateTime;
                x.VStatus = vkapat;
                x.CloseTime = DateTime.Now;
                db.SaveChanges();
                ///////////////////////////////////////////////
                

                ///txt dosyaya yaz///
                var z = db.Vardiya.Find(id);
                VardiyaAc = z.OpenTime.ToString();
                VardiyaKapat = z.CloseTime.ToString();
                kasa = z.DeviceDesing;
                PerSAdSoyad = z.AdSoyad;
                vardiyasaati = z.Vardiya1;
                string PersID = z.PerID.ToString();

                string dosyaYAZ = VardiyaAc + "&" + ClosedDate.ToString() + "&" + label4.Text + "&" + PerSAdSoyad + "&" + kasa + "&" + "Closed" + "&" + PersID + "&" + id.ToString();
                StreamWriter SW = File.AppendText(@"data\ShiftLOG.dat");
                SW.WriteLine(dosyaYAZ);
                SW.Close();
                ////////////////////////
                string tip;




                c = 0;

                // int s = 0;

                for (int i = 0; i < 3; i++)
                {
                    ozetgelir.VID = svardiya.Varid;
                    ozetgelir.VarBasTar = svardiya.Vtarih;
                    ozetgelir.VarBtTar = DateTime.Now;
                    ozetgelir.PerID = svardiya.VPerid;
                    ozetgelir.Personel = svardiya.VPeradSoyad;
                    ozetgelir.VardiyaSaati = svardiya.varsaat;
                    ozetgelir.Otopark = otoparklar[i];
                    ozetgelir.NakitFis = gTop[inx];
                    ozetgelir.NakitFisAdet = sayac[inx];
                    inx++;
                    ozetgelir.NakitFatura = gTop[inx];
                    ozetgelir.NakitFaturaAdet = sayac[inx];
                    inx++;
                    ozetgelir.KrediKartiFis = gTop[inx];
                    ozetgelir.KrediKartiFisAdet = sayac[inx];
                    inx++;
                    ozetgelir.KrediKartiFatura = gTop[inx];
                    ozetgelir.KrediKartiFaturaAdet = sayac[inx];
                    inx++;
                    ozetgelir.HavaleEftFatura = gTop[inx];
                    ozetgelir.HavaleEftFaturaAdet = sayac[inx];
                    inx++;
                    ozetgelir.CariFatura = gTop[inx];
                    ozetgelir.CariFaturaAdet = sayac[inx];
                    inx++;
                    db.OzetGelir.Add(ozetgelir);
                    db.SaveChanges();
                }
                inx = 0;

                c = 0;
                for (int i = 0; i < 3; i++)
                {
                    // MessageBox.Show(otoparklar[i].ToString());
                    for (int j = 0; j < 3; j++)
                    {
                        carpark = otoparklar[i];
                        tip = satistip[j];
                        var result = db.Gelirler.Where(tp => tp.Otopark == carpark & tp.Status == tip & tp.VardiyaID == id).Sum(tp => tp.GenelToplam).ToString();
                        if (result == string.Empty)
                        {
                            gTop[c] = 0;
                            sayac[c] = 0;
                            c++;
                        }
                        else
                        {
                            var say = db.Gelirler.Where(tp => tp.Otopark == carpark & tp.Status == tip & tp.VardiyaID == id).Count();
                            gTop[c] = Convert.ToDecimal(result);
                            sayac[c] = say;
                            c++;
                        }



                    
                    }


                }

                for (int i = 0; i < 3;i++)
                {
                    tempdbx.VarId = svardiya.Varid;
                    tempdbx.Perid = svardiya.VPerid;
                    tempdbx.Personel=svardiya.VPeradSoyad;
                    tempdbx.Tarih = svardiya.Vtarih;
                    tempdbx.Otopark = otoparklar[i];
                    tempdbx.Congress=gTop[c];
                    tempdbx.CongressAdet = sayac[c];
                    c++;
                    tempdbx.Abone=gTop[c];
                    tempdbx.AboneAdet= sayac[c];
                    c++;
                    tempdbx.OzelSatis = gTop[c];
                    tempdbx.OzelSatisAdet = sayac[c];
                    c++;
                     db.tempDbx.Add(tempdbx);
                    db.SaveChanges();


                }
                c = 0;










                MessageBox.Show("Vardiya Kapanışı Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();


            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            string VOpen = "Open";
            List<Vardiya> Search_option = db.Vardiya.Where(p => p.VStatus == VOpen).ToList();
            label3.Text = Search_option[0].AdSoyad;
            label4.Text = Search_option[0].Vardiya1;
            label5.Text=  Search_option[0].ID.ToString();

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnAboneRaporu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.Hide();
            frmAboneRaporu = new AboneRaporu();
            frmAboneRaporu.ShowDialog();
            frmAboneRaporu = null;
            this.Show();
        }

        private void btnKeyKart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmkeykarttakip = new KeyKartTakip();
            frmkeykarttakip.ShowDialog();
            frmkeykarttakip = null;
            this.Show();


        }

        private void btnKeyKartListeTakip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmkeykartsatislistesi = new KeyKartSatisListesi();
            frmkeykartsatislistesi.ShowDialog();
            frmkeykartsatislistesi = null;
            this.Show();

            
        }

        private void btnSatisRaporu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmVardiyaRaporu = new VardiyaRaporu();
            frmVardiyaRaporu.ShowDialog();
            frmVardiyaRaporu = null;
            this.Show();
        }

        private void btnReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            frmMuhasebeRaporu = new MuhasebeRapor();
            frmMuhasebeRaporu.ShowDialog();
            frmMuhasebeRaporu = null;
            this.Show();
        }

        private void btnTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            //frmTest = new Test();
            //frmTest.ShowDialog();
            //frmTest = null;
            frmOzelSatisRapor = new OzelSatisRaporMenu();
            frmOzelSatisRapor.ShowDialog(); 
            frmOzelSatisRapor=null;
            this.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            string __vardiya = "Open";
            int _shiftID = 0;


            var stexist = from s in db.Vardiya where s.VStatus == __vardiya select s.VStatus;

            if (stexist.Count() > 0)
            {
                if (e.CloseReason != CloseReason.ApplicationExitCall)
                {

                    DialogResult closing = MessageBox.Show("Vardiya Kapanışı Yapılmamış. Çıkış Yapmak İstiyor musunuz?", "UYARI", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                    if (closing == DialogResult.No)
                    {
                        e.Cancel = true; // if you don't want to exit the game (if you pressed No), cancel the closing   
                    }
                    if (closing == DialogResult.Yes)
                    {
                        Application.Exit(); // exit application // if you don't want to exit the game (if you pressed No), cancel the closing   
                    }
                }
            }
        }
      }
            
         
}
