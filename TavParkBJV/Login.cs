using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;

namespace TavParkBJV
{
    
    public partial class Login : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        OzetGelir ozetgelir = new OzetGelir();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        Vardiya BJVvardiya = new Vardiya();
        public string connetionString,Per_AdSoyad,Per_Vardiya,DeviceD;
        public int Per_ID;
        SqlConnection baglanti, SDbaglanti;
        Thread th;
        DateTime AcilisTarihi;
        public Login()
        {
            InitializeComponent();
        }

        public void openmainform(object obj)
        {
            Application.Run(new Main());
        }

        private void DBConnectEnter()
        {

            StreamReader oku = new StreamReader(@"data\Connection_DB.dat");
            connetionString = oku.ReadLine();                      
            baglanti = new SqlConnection(connetionString);
            baglanti.Open();
            MessageBox.Show("Connection Open  !");
            baglanti.Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string __vardiya = "Open";
            int _shiftID = 0;


            var stexit = from s in db.Vardiya where s.VStatus == __vardiya select s.VStatus;
           
            if (stexit.Count() > 0)
            {
                MessageBox.Show("Kapatılmamış Vardiya Tespit Edildi.Bir Önceki Vardiyadan devam edilecektir","UYARI",MessageBoxButtons.OK, MessageBoxIcon.Error);

                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select * From BjvPersonel where PerAdSoyad= @PerAdSoyad and PerPassword=@PerPassword", baglanti);
                cmd.Parameters.AddWithValue("@PerAdSoyad", comboBoxPersonel.Text);
                cmd.Parameters.AddWithValue("@PerPassword", textBoxPassword.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {

                    if (reader.Read())
                    {
                        
                        this.Close();
                        th = new Thread(openmainform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();


                    }
                    else
                    {
                        MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");

                    }


                }
                reader.Close();
                baglanti.Close();


            }

            else
            {
          
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From BjvPersonel where PerAdSoyad= @PerAdSoyad and PerPassword=@PerPassword", baglanti);
            cmd.Parameters.AddWithValue("@PerAdSoyad", comboBoxPersonel.Text);
            cmd.Parameters.AddWithValue("@PerPassword", textBoxPassword.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {

                if (reader.Read())
                {
                  
                    Per_ID = Convert.ToInt32(reader[0].ToString());
                    Per_AdSoyad=comboBoxPersonel.Text;
                    Per_Vardiya =comboBoxVardiya.Text;
                    DeviceD=cmbOdemeKasasi.Text;
                    AcilisTarihi = DateTime.Now;
                    BJVvardiya.PerID = Per_ID;
                    BJVvardiya.AdSoyad = Per_AdSoyad;
                    BJVvardiya.Vardiya1 = Per_Vardiya;
                    BJVvardiya.OpenTime = AcilisTarihi;
                    BJVvardiya.DeviceDesing=DeviceD;
                    BJVvardiya.VStatus = "Open";
                    db.Vardiya.Add(BJVvardiya);
                    db.SaveChanges();
                   
                    string _vardiya = "Open";

                    var stexistx = from s in db.Vardiya where s.VStatus == _vardiya select s.VStatus;
                        

                        if (stexistx.Count() > 0)
                        {
                            var st = (from s in db.Vardiya where s.VStatus == _vardiya select s).First();
                           _shiftID = st.ID;
                            
                        }
                        


                    string dosyaYAZ=AcilisTarihi.ToString()+"&"+comboBoxVardiya.Text+"&"+Per_AdSoyad+"&"+ DeviceD+"&"+"Open"+"&"+Per_ID+"&"+Convert.ToString(_shiftID);
                    StreamWriter SW = File.AppendText(@"data\ShiftLOG.dat");
                    SW.WriteLine(dosyaYAZ);
                    SW.Close();
                    this.Close();
                    th = new Thread(openmainform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();


                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre");

                }


            }
            reader.Close();
            baglanti.Close();

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

        private void personel_yukle()
        {
            comboBoxPersonel.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from BjvPersonel", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBoxPersonel.Items.Add(dr["PerAdSoyad"]);

            }
            baglanti.Close();
            dr.Close();



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

        private void VardiyaSaatiYukle()
        {
            comboBoxVardiya.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\VardiyaSaati.dat");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                comboBoxVardiya.Items.Add(tokens[0]);
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                DBConnectEnter();
                personel_yukle();
                SD_Connect();
                OdemeKasasiYukle();
                VardiyaSaatiYukle();

            }
            catch (Exception)
            {

                MessageBox.Show("Veri Tabanı Bağlantı Hatası");
                Application.Exit();

            }
        }
    }
}
