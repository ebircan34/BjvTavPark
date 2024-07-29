using DevExpress.Data.Linq.Helpers;
using DevExpress.Data.Utils;
using DevExpress.Data.WcfLinq.Helpers;
using DevExpress.XtraExport.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TavParkBJV
{
    public partial class AboneRaporu : Form
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        string connetionString;
        SqlConnection baglanti, SDbaglanti;
        DateTime Dt1, Dt2,Dt3,Dt4,Dt5,Dt6;
            
        


        String StringDt1, StringDt2,StringDt3,StringDt4,StringDt5,StringDt6;

        private void btnExcell_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2;
            // dataGridView1.Rows.Clear();
            int satirsayisi;
            int i = 0; int z = 0;
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
            excelWorkbook = excel.Workbooks.Open("C:\\Rapor\\BjvOtoparkFatRaporu.xlsx");
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Item["FaturaListesi"];
            string abn = "ABONE";
            string cngr = "CONGRESS";
            string invoices = "FATURA";
            string ozsatis = "ÖZEL SATIŞ";
            string ichatotopark = "IC HAT 1 OTOPARK";

            if (dataGridView1.RowCount > 0)
            {




                //var bireysel = (from px in db.Gelirler
                //                join fx in db.GercekMusteriler
                //                on px.MusteriId equals fx.MusteriId
                //                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.InvoiceStatus == invoices
                //                select new
                //                {
                //                    bastar = px.BaslangicTarihi,
                //                    _PlakaNo = fx.PlakaNo,
                //                    _AdSoyadUnvan = fx.AdSoyad,
                //                    _tekNo = fx.TelefonNo,
                //                    _email = fx.email,
                //                    _tanim = px.Tanim,
                //                    _birimFiyat = px.SatisGeliri,
                //                    _sureAdet = px.Sure,
                //                    _AraToplam = px.AraToplam,
                //                    _keyKart = px.KeyKartGeliri,
                //                    _GenelToplam = px.GenelToplam,
                //                    _odemeYontemiNet = px.OdemeYontemiNet,
                //                    _otopark = px.Otopark,
                //                    _odemeKasai = px.OdemeKasasi,
                //                    _islemiYapan = px.Personel,
                //                    _kartBiletNo = px.KartBiletNo,
                //                    _ilce = fx.ilce,
                //                    _sehir = fx.Sehir,
                //                    _adresText = fx.AdresText,

                //                }).ToList();



                //var firmalar = (from px in db.Gelirler
                //                join fx in db.GercekMusteriler
                //                on px.MusteriId equals fx.MusteriId
                //                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.InvoiceStatus == invoices
                //                select new
                //                {
                //                    bastar = px.BaslangicTarihi,
                //                    _PlakaNo = fx.PlakaNo,
                //                    _AdSoyadUnvan = fx.AdSoyad,
                //                    _tekNo = fx.TelefonNo,
                //                    _email = fx.email,
                //                    _tanim = px.Tanim,
                //                    _birimFiyat = px.SatisGeliri,
                //                    _sureAdet = px.Sure,
                //                    _AraToplam = px.AraToplam,
                //                    _keyKart = px.KeyKartGeliri,
                //                    _GenelToplam = px.GenelToplam,
                //                    _odemeYontemiNet = px.OdemeYontemiNet,
                //                    _otopark = px.Otopark,
                //                    _odemeKasai = px.OdemeKasasi,
                //                    _islemiYapan = px.Personel,
                //                    _kartBiletNo = px.KartBiletNo,
                //                    _ilce = fx.ilce,
                //                    _sehir = fx.Sehir,
                //                    _adresText = fx.AdresText,

                //                }).ToList();


                //Data is going to stored from Row 2
                int row = 4;
                i = 0;
                int s = 1;
                bool oemLounge = false;
                //Loop Through Each Employees and Populate the worksheet
                //For Each Employee increase row by 1
                int g = 0;


                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    excelWorksheet.Cells[row + i, 1] = dataGridView1.Rows[i].Cells["SiraNo"].Value.ToString();
                    excelWorksheet.Cells[row + i, 2] = dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value.ToString();
                    excelWorksheet.Cells[row + i, 3] = dataGridView1.Rows[i].Cells["Plaka"].Value.ToString();
                    excelWorksheet.Cells[row + i, 4] = dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value.ToString();
                    excelWorksheet.Cells[row + i, 5] = dataGridView1.Rows[i].Cells["Telefon"].Value.ToString();
                    excelWorksheet.Cells[row + i, 6] = dataGridView1.Rows[i].Cells["email"].Value.ToString();
                    excelWorksheet.Cells[row + i, 7] = dataGridView1.Rows[i].Cells["Tanim"].Value.ToString();
                    excelWorksheet.Cells[row + i, 8] = dataGridView1.Rows[i].Cells["SatisGeliri"].Value.ToString();
                    excelWorksheet.Cells[row + i, 9] = dataGridView1.Rows[i].Cells["Sure"].Value.ToString();
                    //excelWorksheet.Cells[row, 10] = emp._sureAdet;
                    excelWorksheet.Cells[row + i, 10] = dataGridView1.Rows[i].Cells["AraToplam"].Value.ToString();
                    excelWorksheet.Cells[row + i, 11] = dataGridView1.Rows[i].Cells["KeyKart"].Value.ToString();
                    excelWorksheet.Cells[row + i, 12] = dataGridView1.Rows[i].Cells["GenelToplam"].Value.ToString();
                    excelWorksheet.Cells[row + i, 13] = dataGridView1.Rows[i].Cells["OdemeYonDetayi"].Value.ToString();
                    excelWorksheet.Cells[row + i, 14] = dataGridView1.Rows[i].Cells["Otopark"].Value.ToString();
                    excelWorksheet.Cells[row + i, 15] = dataGridView1.Rows[i].Cells["OdemeKasasi"].Value.ToString();
                    excelWorksheet.Cells[row + i, 16] = dataGridView1.Rows[i].Cells["Personel"].Value.ToString();
                    excelWorksheet.Cells[row + i, 17] = dataGridView1.Rows[i].Cells["Status"].Value.ToString();
                    excelWorksheet.Cells[row + i, 18] = dataGridView1.Rows[i].Cells["KartBiletNo"].Value.ToString();
                    excelWorksheet.Cells[row + i, 24] = dataGridView1.Rows[i].Cells["VergiDairesi"].Value.ToString();
                    excelWorksheet.Cells[row + i, 25] = dataGridView1.Rows[i].Cells["Ilce"].Value.ToString();
                    excelWorksheet.Cells[row + i, 26] = dataGridView1.Rows[i].Cells["Sehir"].Value.ToString();
                    excelWorksheet.Cells[row + i, 27] = dataGridView1.Rows[i].Cells["AdresText"].Value.ToString();
                    //row++;
                    //i++;
                    s++;
                    oemLounge = true;
                    //excelWorksheet.Cell(row, 1).Value = emp.Id;
                    //excelWorksheet.Cell(row, 2).Value = emp.Name;
                    //worksheet.Cell(row, 3).Value = emp.Departmet;
                    //worksheet.Cell(row, 4).Value = emp.Salary;
                    //worksheet.Cell(row, 5).Value = emp.Position;
                    //worksheet.Cell(row, 6).Value = emp.DateOfJoining;
                    //row++; //Increasing the Data Row by 1
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Kaydedilecek Yolu Seçiniz..";
                saveDialog.Filter = "Excel Dosyası|*.xlsx";
                saveDialog.FileName = "BjvOtoparkFatRaporu_" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "_" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    excelWorksheet.SaveAs(saveDialog.FileName);

                    MessageBox.Show("Rapor Excel Formatında Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                excelWorkbook.Close();
                excel.Quit();
                this.Cursor = Cursors.Default;

                //int rc = dataGridView1.RowCount;
                //int fs;
                //for (fs = g; fs < dataGridView1.RowCount; g++)
                //{
                //    excelWorksheet.Cells[row, 1] = i;
                //    excelWorksheet.Cells[row, 2] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 3] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 4] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 5] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 6] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 7] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 8] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 9] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    //excelWorksheet.Cells[row, 10] = emp._sureAdet;
                //    excelWorksheet.Cells[row, 10] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 11] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 12] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 13] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 14] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 15] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 16] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 17] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 18] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 19] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    excelWorksheet.Cells[row, 20] = dataGridView1.Rows[i].Cells[s].Value.ToString();
                //    row++;
                //    i++;
                //    s++;
                //    oemLounge = true;
                //excelWorksheet.Cell(row, 1).Value = emp.Id;
                //excelWorksheet.Cell(row, 2).Value = emp.Name;
                //worksheet.Cell(row, 3).Value = emp.Departmet;
                //worksheet.Cell(row, 4).Value = emp.Salary;
                //worksheet.Cell(row, 5).Value = emp.Position;
                //worksheet.Cell(row, 6).Value = emp.DateOfJoining;
                //row++; //Increasing the Data Row by 1
            }

            /// if (oemLounge == false) MessageBox.Show("Merhaba Dünya");


            //foreach (var item in firmalar)
            //{
            //    excelWorksheet.Cells[row, 1] = i;
            //    excelWorksheet.Cells[row, 2] = item.bastar;
            //    excelWorksheet.Cells[row, 3] = item._PlakaNo;
            //    excelWorksheet.Cells[row, 4] = item._AdSoyadUnvan;
            //    excelWorksheet.Cells[row, 5] = item._tekNo;
            //    excelWorksheet.Cells[row, 6] = item._email;
            //    excelWorksheet.Cells[row, 7] = item._tanim;
            //    excelWorksheet.Cells[row, 8] = item._birimFiyat;
            //    excelWorksheet.Cells[row, 9] = item._sureAdet;
            //    //excelWorksheet.Cells[row, 10] = emp._sureAdet;
            //    excelWorksheet.Cells[row, 10] = item._AraToplam;
            //    excelWorksheet.Cells[row, 11] = item._keyKart;
            //    excelWorksheet.Cells[row, 12] = item._GenelToplam;
            //    excelWorksheet.Cells[row, 13] = item._odemeYontemiNet;
            //    excelWorksheet.Cells[row, 14] = item._otopark;
            //    excelWorksheet.Cells[row, 15] = item._odemeKasai;
            //    excelWorksheet.Cells[row, 16] = item._islemiYapan;
            //    excelWorksheet.Cells[row, 17] = item._kartBiletNo;
            //    excelWorksheet.Cells[row, 18] = item._ilce;
            //    excelWorksheet.Cells[row, 19] = item._sehir;
            //    excelWorksheet.Cells[row, 20] = item._adresText;
            //    row++;
            //    i++;
            //    oemLounge = true;
            //    //excelWorksheet.Cell(row, 1).Value = emp.Id;
            //    //excelWorksheet.Cell(row, 2).Value = emp.Name;
            //    //worksheet.Cell(row, 3).Value = emp.Departmet;
            //    //worksheet.Cell(row, 4).Value = emp.Salary;
            //    //worksheet.Cell(row, 5).Value = emp.Position;
            //    //worksheet.Cell(row, 6).Value = emp.DateOfJoining;
            //    //row++; //Increasing the Data Row by 1
            //}
            //if (oemLounge == false) MessageBox.Show("Merhaba Dünya");
                    
            
            else
            {
                MessageBox.Show("Excel'e Gönderilecek Veri Bulunamadı.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




            




        }

        private void btnExcelSend_MouseLeave(object sender, EventArgs e)
        {
            //btnExcelSend.BackColor = SystemColors.Window;
        }

        private void btnExcelSend_MouseHover(object sender, EventArgs e)
        {
           // btnExcelSend.BackColor = Color.Goldenrod;
        }

        

        private void btnAboneAra_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2, Dt3,Dt4;
            dataGridView1.Rows.Clear();
            int satirsayisi;
            int i = 0; int z = 0;
            String StringDt1, StringDt2, StringDt3, StringDt4;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
            //StringDt3 = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            //Dt3 = Convert.ToDateTime(StringDt3);
           // StringDt4 = dateTimePicker4.Value.ToString("yyyy-MM-dd");
            //Dt4 = Convert.ToDateTime(StringDt4);

            



            i = 0;
            z = 1;


           




            



            // i = 0;
            //z = 1;

            
        }

        private void btnPlakaAra_Click(object sender, EventArgs e)
        {
            DateTime Dt1, Dt2,Dt3;
            dataGridView1.Rows.Clear();
            int satirsayisi;
            int i = 0; int z = 0;
            String StringDt1, StringDt2, StringDt3;
            StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Dt1 = Convert.ToDateTime(StringDt1);
           // StringDt3 = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            //Dt3 = Convert.ToDateTime(StringDt3);


            



            i = 0;
            z = 1;


            



            



            // i = 0;
            //z = 1;

            
        }

        public AboneRaporu()
        {
            InitializeComponent();
        }

        private void AboneRaporu_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
           // dateTimePicker3.Value = DateTime.Now;
            //dateTimePicker4.Value = DateTime.Now;
            SD_Connect();
            DB_Connect();
           // VardiyaYukle();
           // comboBoxVardiya.SelectedIndex = 0;

        }

        private void btnBulGetir_Click(object sender, EventArgs e)
        {
            
            
                DateTime Dt1, Dt2;
            string icHatOtoPark = "IC HAT 1 OTOPARK";
            string disHatOtoPark = "DIS HAT OTOPARK";
            string rentACarOtopark = "RENT A CAR OTOPARK";
            dataGridView1.Rows.Clear();
                int satirsayisi;
                int i = 0; int z = 0;
                String StringDt1, StringDt2;
                StringDt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                Dt1 = Convert.ToDateTime(StringDt1);
                StringDt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                Dt2 = Convert.ToDateTime(StringDt2);
            string invoices = "FATURA";
            // string ozsatis = "ÖZEL SATIŞ";

            var bireysel = (from px in db.Gelirler
                            join fx in db.GercekMusteriler
                            on px.MusteriId equals fx.MusteriId
                            where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.InvoiceStatus == invoices
                            select new
                            {
                                // MusteriUnvani = fx.AdSoyad,
                                //Toplam = px.GenelToplam,
                                //_araToplam = px.AraToplam,
                                //_keyKartGeliri = px.KeyKartGeliri,
                                //_plaka = fx.PlakaNo,
                                //_aratoplam = px.AraToplam,
                                //_tanim = px.Tanim,
                                //_sure = px.Sure,
                                //_birimFiyati = px.SatisGeliri,
                                //_veriTasiyici = px.VeriTasiyici,
                                //_invoice = px.InvoiceStatus,
                                //_basTar = px.BaslangicTarihi,
                                //_bitTar = px.BaslangicTarihi,
                                //_odemeYontemi = px.OdemeYontemi,
                                //_status = px.Status,
                                //_personel = px.Personel,
                                //_odemeKasasi = px.OdemeKasasi,
                                //_odYonDetayi = px.OdemeYontemiNet,
                                bastar = px.BaslangicTarihi,
                                _PlakaNo = fx.PlakaNo,
                                _AdSoyadUnvan = fx.AdSoyad,
                                _tekNo = fx.TelefonNo,
                                _email = fx.email,
                                _tanim = px.Tanim,
                                _birimFiyat = px.SatisGeliri,
                                _sureAdet = px.Sure,
                                _AraToplam = px.AraToplam,
                                _keyKart = px.KeyKartGeliri,
                                _GenelToplam = px.GenelToplam,
                                _odemeYontemiNet = px.OdemeYontemiNet,
                                _otopark = px.Otopark,
                                _odemeKasai = px.OdemeKasasi,
                                _islemiYapan = px.Personel,
                                _kartBiletNo = px.KartBiletNo,
                                _ilce = fx.ilce,
                                _sehir = fx.Sehir,
                                _adresText = fx.AdresText,
                                _satisTipi = px.Status,
                                _vergiDairesi=fx.VergiDairesi,
                            }).ToList();
                
            
               satirsayisi = bireysel.Count;



                i = 0;
                z = 1;


                bireysel.ForEach(x =>
                {


                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["SiraNo"].Value = z.ToString();
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["Plaka"].Value = x._PlakaNo.ToString();
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Telefon"].Value = x._tekNo;
                    dataGridView1.Rows[i].Cells["email"].Value = x._email;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["SatisGeliri"].Value = x._birimFiyat;
                    dataGridView1.Rows[i].Cells["Sure"].Value = x._sureAdet;
                    dataGridView1.Rows[i].Cells["AraToplam"].Value = x._AraToplam;
                    dataGridView1.Rows[i].Cells["KeyKart"].Value = x._keyKart.ToString();
                    dataGridView1.Rows[i].Cells["GenelToplam"].Value = x._GenelToplam;
                    dataGridView1.Rows[i].Cells["OdemeYonDetayi"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["OdemeKasasi"].Value = x._odemeKasai;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["Status"].Value = x._satisTipi;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["VergiDairesi"].Value = x._vergiDairesi;
                    dataGridView1.Rows[i].Cells["Ilce"].Value = x._ilce;
                    dataGridView1.Rows[i].Cells["Sehir"].Value = x._sehir;
                    dataGridView1.Rows[i].Cells["AdresText"].Value = x._adresText;
                    



                    z = z + 1;
                    i = i + 1;
                });




                var firmalar = (from px in db.Gelirler
                                join fx in db.TuzelMusteriler
                                on px.MusteriId equals fx.MusteriId
                                where px.BaslangicTarihi >= Dt1 & px.BaslangicTarihi <= Dt2 & px.InvoiceStatus == invoices 
                                select new
                                {
                                    bastar = px.BaslangicTarihi,
                                    _PlakaNo = fx.PlakaNo,
                                    _AdSoyadUnvan = fx.Unvan,
                                    _tekNo = fx.TelefonNo,
                                    _email = fx.email,
                                    _tanim = px.Tanim,
                                    _birimFiyat = px.SatisGeliri,
                                    _sureAdet = px.Sure,
                                    _AraToplam = px.AraToplam,
                                    _keyKart = px.KeyKartGeliri,
                                    _GenelToplam = px.GenelToplam,
                                    _odemeYontemiNet = px.OdemeYontemiNet,
                                    _otopark = px.Otopark,
                                    _odemeKasai = px.OdemeKasasi,
                                    _islemiYapan = px.Personel,
                                    _kartBiletNo = px.KartBiletNo,
                                    _ilce = fx.ilce,
                                    _sehir = fx.Sehir,
                                    _adresText = fx.AdresText,
                                    _satisTipi = px.Status,
                                    _vergiDairesi = fx.VergiDairesi,

                                }).ToList();
               // satirsayisi = firmalar.Count;



                // i = 0;
                //z = 1;

                firmalar.ForEach(x =>
                {


                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["SiraNo"].Value = z.ToString();
                    dataGridView1.Rows[i].Cells["BaslangicTarihi"].Value = x.bastar;
                    dataGridView1.Rows[i].Cells["Plaka"].Value = x._PlakaNo.ToString();
                    dataGridView1.Rows[i].Cells["AdSoyadUnvan"].Value = x._AdSoyadUnvan;
                    dataGridView1.Rows[i].Cells["Telefon"].Value = x._tekNo;
                    dataGridView1.Rows[i].Cells["email"].Value = x._email;
                    dataGridView1.Rows[i].Cells["Tanim"].Value = x._tanim;
                    dataGridView1.Rows[i].Cells["SatisGeliri"].Value = x._birimFiyat;
                    dataGridView1.Rows[i].Cells["Sure"].Value = x._sureAdet;
                    dataGridView1.Rows[i].Cells["AraToplam"].Value = x._AraToplam;
                    dataGridView1.Rows[i].Cells["KeyKart"].Value = x._keyKart.ToString();
                    dataGridView1.Rows[i].Cells["GenelToplam"].Value = x._GenelToplam;
                    dataGridView1.Rows[i].Cells["OdemeYonDetayi"].Value = x._odemeYontemiNet;
                    dataGridView1.Rows[i].Cells["Otopark"].Value = x._otopark;
                    dataGridView1.Rows[i].Cells["OdemeKasasi"].Value = x._odemeKasai;
                    dataGridView1.Rows[i].Cells["Personel"].Value = x._islemiYapan;
                    dataGridView1.Rows[i].Cells["Status"].Value = x._satisTipi;
                    dataGridView1.Rows[i].Cells["KartBiletNo"].Value = x._kartBiletNo;
                    dataGridView1.Rows[i].Cells["VergiDairesi"].Value = x._vergiDairesi;
                    dataGridView1.Rows[i].Cells["Ilce"].Value = x._ilce;
                    dataGridView1.Rows[i].Cells["Sehir"].Value = x._sehir;
                    dataGridView1.Rows[i].Cells["AdresText"].Value = x._adresText;
                    


                    z = z + 1;
                    i = i + 1;
                });

            var Gtoplam = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.InvoiceStatus == invoices).Sum(x => x.GenelToplam).ToString();
            if (Gtoplam == string.Empty) txtGToplam.Text = "0";
            else if (Gtoplam != string.Empty) txtGToplam.Text = Gtoplam;

            var ichGtoplam = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.InvoiceStatus == invoices & x.Otopark == icHatOtoPark).Sum(x => x.GenelToplam).ToString();
            if (ichGtoplam == string.Empty) txticFat.Text = "0";
            else if (ichGtoplam != string.Empty) txticFat.Text = ichGtoplam;

            var dhGtoplam = db.Gelirler.Where(x=> x.BaslangicTarihi>=Dt1 & x.BaslangicTarihi<=Dt2 & x.Otopark==disHatOtoPark & x.InvoiceStatus==invoices).Sum(x=> x.GenelToplam).ToString();    
            if (dhGtoplam == string.Empty) TXTdhFAT.Text= "0";
            else if (dhGtoplam != string.Empty) TXTdhFAT.Text= dhGtoplam;

            var RcarGToplam = db.Gelirler.Where(x => x.BaslangicTarihi >= Dt1 & x.BaslangicTarihi <= Dt2 & x.Otopark == rentACarOtopark & x.InvoiceStatus == invoices).Sum(x => x.GenelToplam).ToString();
            if (RcarGToplam==string.Empty) txtRcarFat.Text= "0";
            else if (RcarGToplam != string.Empty) txtRcarFat.Text = RcarGToplam;






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

        //private void VardiyaYukle()
        //{
        //    comboBoxVardiya.Items.Clear();
        //    string[] lineOfContents = File.ReadAllLines(@"data\VardiyaSaati.dat");
        //    foreach (var line in lineOfContents)
        //    {
        //        string[] tokens = line.Split(',');
        //        // get the 2nd element (the 1st item is always item 0)
        //        comboBoxVardiya.Items.Add(tokens[0]);
        //    }
        //}




    }
}
