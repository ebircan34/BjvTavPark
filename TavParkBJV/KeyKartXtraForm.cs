using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace TavParkBJV
{
    public partial class KeyKartXtraForm : DevExpress.XtraEditors.XtraForm
    {
        ParkDBXEntities db = new ParkDBXEntities();
        TuzelMusteriler tuzelMusteriler = new TuzelMusteriler();
        Musteriler musteriler = new Musteriler();
        GercekMusteriler gercekMusteriler = new GercekMusteriler();
        Gelirler gelirler = new Gelirler();
        KeyKartStok KeyKartStok = new KeyKartStok();
        KeyKartHareket keykarthareket = new KeyKartHareket();
        KeyKartKalanTakip keykartstok = new KeyKartKalanTakip();
        public KeyKartXtraForm()
        {
            InitializeComponent();
        }

        private void KeyKartXtraForm_Load(object sender, EventArgs e)
        {
           

        }
    }
}