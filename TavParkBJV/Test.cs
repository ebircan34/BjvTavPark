using DevExpress.Data.TreeList;
using DevExpress.Utils.Extensions;
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
using System.IO;
using DevExpress.XtraEditors.SyntaxEditor;
using DevExpress.Utils;

namespace TavParkBJV
{
    public partial class Test : Form

    {
        public static string[] otoparklar = new string[3];
        public static string[] odemeyonteminet = new string[6];
        public static decimal[] gTop=new decimal[18];
        public static int [] sayac=new int[18];
        public int row = 0;
        public int c = 0;
        public int col = 0;
        public int inx = 0;
        string connetionString, _shiftBlock, _Personel;
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
        tempDbx tbltempDbx = new tempDbx();
        OzetGelir ozetgelir = new OzetGelir();
        tempDbx tempdbx = new tempDbx();    
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            SD_Connect();
            DB_Connect();
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
                otoparklar[row]=tokens[0];
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
                odemeyonteminet[row]=tokens[0];
                row++;  

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



        private void button1_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2,Tarih;
            String StringDt1, StringDt2,carpark,paymentmethod;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            Dt2 = Convert.ToDateTime(StringDt2);
            otoparklariGetir();
            odemeYontemleriniGetir();
            int Perid; 
            int VarId=0;
            string vardiyaDurumu = "Open";
            string PerAd,VarSaat;
            String OpTime;
            String ClTime;

            //var stexistx = from s in db.Vardiya where s.VStatus == vardiyaDurumu select s.VStatus;

            //if (stexistx.Count() > 0)
            //{
               // var st = (from s in db.Vardiya where s.VStatus == vardiyaDurumu  select s).First();
             
            Svardiya svardiya=new Svardiya();
            var st = db.Vardiya.Where(x => x.VStatus == vardiyaDurumu);
            foreach (var item in st)
            {
                svardiya.VPerid = Convert.ToInt32(item.PerID);
                svardiya.Varid = Convert.ToInt32(item.ID);
                svardiya.VPeradSoyad=item.AdSoyad;
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
                    var result = db.Gelirler.Where(x => x.Otopark == carpark & x.OdemeYontemiNet == odeme & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi >= Dt2).Sum(x => x.GenelToplam).ToString();
                    if (result == string.Empty)
                    {
                        gTop[c] = 0;
                        sayac[c] = 0;            
                        c++;
                    }
                    else
                    {
                        var say = db.Gelirler.Where(x => x.Otopark == carpark & x.OdemeYontemiNet == odeme & x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi >= Dt2).Count();
                        gTop[c] = Convert.ToDecimal(result);
                        sayac[c] = say;
                        c++;
                    }
                
                
                
                }

            }
            c = 0;

            // int s = 0;
           
            for (int i = 0; i < 3; i++)
            {
               ozetgelir.VarBasTar = svardiya.Vtarih;
               ozetgelir.PerID = svardiya.VPerid;
               ozetgelir.Personel = svardiya.VPeradSoyad;
               ozetgelir.VardiyaSaati = svardiya.varsaat;
               ozetgelir.Otopark = otoparklar[i]; 
               ozetgelir.NakitFis = gTop[inx];
               ozetgelir.NakitFisAdet= sayac[inx];
               inx++;
               ozetgelir.NakitFatura = gTop[inx];
               ozetgelir.NakitFaturaAdet = sayac[inx];
               inx++;
               ozetgelir.KrediKartiFis= gTop[inx];
               ozetgelir.KrediKartiFisAdet = sayac[inx];
               inx++;
               ozetgelir.KrediKartiFatura = gTop[inx];
               ozetgelir.KrediKartiFaturaAdet = sayac[inx];
               inx++;
               ozetgelir.HavaleEftFatura = gTop[inx];
               ozetgelir.HavaleEftFaturaAdet= sayac[inx];
               inx++ ;
               ozetgelir.CariFatura = gTop[inx];
               ozetgelir.CariFaturaAdet = sayac[inx];
               inx++;
               db.OzetGelir.Add(ozetgelir);
               db.SaveChanges();    
            }

            inx = 0;


            
        }
            
            




        
    }
}
