using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using System.Xml.Linq;

namespace TavParkBJV
{
    public partial class Dashboard : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            //dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
            dateTimePicker2.Value = DateTime.Now;
            tableLayoutPanel5.Enabled = false;
            comboBoxVardiya.SelectedIndex = 0;
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 7F, GraphicsUnit.Pixel);
            }
        }

        private void btnBulGetir_Click(object sender, EventArgs e)
        {
           
            DateTime Dt1,Dt2;
            String StringDt1, StringDt2;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Dt2 = Convert.ToDateTime(StringDt2);
            
            


            var toplam = db.Gelirler.Where(z=> z.Vardiya== comboBoxVardiya.Text & z.BaslangicTarihi>=Dt1 & z.BaslangicTarihi <= Dt2).Sum(p => p.GenelToplam);
            if (toplam != null)
            {
                txtToplam.Text = string.Format("{0:C}",toplam);
                toplam = 0;
            }
            else if (toplam ==null)
            {
                txtToplam.Text = "0";
                toplam= 0;
            }




            var krediKartiToplam =db.Gelirler.Where(z=> z.OdemeYontemi=="KREDI KARTI" & z.Vardiya==comboBoxVardiya.Text & z.BaslangicTarihi>=Dt1 & z.BaslangicTarihi<=Dt2 ).Sum(z => z.GenelToplam);
            if (krediKartiToplam != null)
            {
                txtKrediKarti.Text = string.Format("{0:C}", krediKartiToplam);
                krediKartiToplam= 0;    

            }
            else if (krediKartiToplam == null)
            {
                txtKrediKarti.Text = "0";
                krediKartiToplam = 0;
            }



            var nakitToplam = db.Gelirler.Where(z => z.OdemeYontemi == "Nakit" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).Sum(z => z.GenelToplam);

            if (nakitToplam != null)
            {
                txtNakit.Text = string.Format("{0:C}", nakitToplam);
            }
            else if (nakitToplam == null)
            {
                txtNakit.Text = "0";
            }

            var eft = db.Gelirler.Where(z => z.OdemeYontemi == "HAVALE-EFT" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).Sum(z => z.GenelToplam);
            if (eft != null)
            {
                txtHavaleEft.Text = string.Format("{0:C}", eft); 
            }
            else if (eft == null)
            {
                txtHavaleEft.Text = "0";
            }
            
            var _cari = db.Gelirler.Where(z => z.OdemeYontemi == "Cari" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).Sum(z => z.GenelToplam);
            if (_cari != null)
            {
                txtCari.Text = string.Format("{0:C}", _cari); 
            }
            else if (_cari == null)
            {
                txtCari.Text = "0";
            }


            var _abn = db.Gelirler.Count(z => z.Status == "ABONE" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).ToString();
            
            if (_abn != null)
            {
              txtAbone.Text = string.Format("{0:C}", _abn); 
            }
            else if (_abn == null)
            {
                txtAbone.Text = "0";
            }

            var _fat = db.Gelirler.Count(z => z.InvoiceStatus == "FATURA" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).ToString();

            if (_fat != null)
            {
                txtFatura.Text = string.Format("{0:C}", _fat); 
            }
            else if (_fat == null)
            {
               txtFatura.Text = "0";
            }

            var _cong = db.Gelirler.Count(z => z.Status == "CONGRESS" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).ToString();

            if (_cong != null)
            {
                txtCongress.Text = string.Format("{0:C}", _cong);
            }
            else if (_cong == null)
            {
                txtCongress.Text = "0";
            }

            var _key = db.Gelirler.Count(z => z.VeriTasiyici == "KEY KART" & z.Vardiya == comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).ToString();

            if (_key != null)
            {
              txtKeyKart.Text = string.Format("{0:C}", _key);
            }
            else if (_key == null)
            {
                txtKeyKart.Text = "0";
            }

            var islemadet= db.Gelirler.Where(z=>z.Vardiya==comboBoxVardiya.Text & z.BaslangicTarihi >= Dt1 & z.BaslangicTarihi <= Dt2).Count();
            if (islemadet != null)
            {
                txtTadet.Text = string.Format("{0:C}", islemadet);
            }
            else if (islemadet == null)
            {
                txtTadet.Text = "0";
            }



            var query = from item in db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Vardiya == comboBoxVardiya.Text)
                        select new
                        {

                            // item.MusteriId,
                            //0
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
                            item.BaslangicTarihi,
                            item.BitisTarihi,
                            //item.KartBiletNo, //12
                            //item.Vardiya, //13
                            // item.Id,//14


                        };
           // UpdateFont();
                
            dataGridView1.DataSource = query.ToList();
            







        }

        private void txtNakit_Leave(object sender, EventArgs e)
        {
            double para;
            if (txtNakit.Text == string.Empty) { }
            else
            {
                para = double.Parse(txtNakit.Text);
                txtNakit.Text = para.ToString("N");
                //ondalık basamaklara ayırır ve virgğülden sonra iki basamak gösterir.
                //virgülden sonra iki basamağa bağlı kalmayabilirsiniz. N'in yanına eklediğiniz sayı kadar virgül gösterebilirsiniz.
                //mesela N1 bir virgül, N4 dört virgül gösterir.
                // Ayrıca sayının para biriminin(TL) gösterilmesini isterseniz N yerine C kullanabilirsiniz.
            }
        }

        private void txtCari_Leave(object sender, EventArgs e)
        {
            double para;
            if (txtCari.Text == string.Empty) { }
            else
            {
                para = double.Parse(txtCari.Text);
                txtCari.Text = para.ToString("N");
                //ondalık basamaklara ayırır ve virgğülden sonra iki basamak gösterir.
                //virgülden sonra iki basamağa bağlı kalmayabilirsiniz. N'in yanına eklediğiniz sayı kadar virgül gösterebilirsiniz.
                //mesela N1 bir virgül, N4 dört virgül gösterir.
                // Ayrıca sayının para biriminin(TL) gösterilmesini isterseniz N yerine C kullanabilirsiniz.
            }
        }
    }
}
