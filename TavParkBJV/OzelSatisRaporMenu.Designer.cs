namespace TavParkBJV
{
    partial class OzelSatisRaporMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OzelSatisRaporMenu));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnKayipBiletExcel = new System.Windows.Forms.Button();
            this.btnZorunluBilet = new System.Windows.Forms.Button();
            this.btnExUcret = new System.Windows.Forms.Button();
            this.btnArBilForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(22, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(105, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2024, 6, 8, 0, 0, 0, 0);
            // 
            // btnKayipBiletExcel
            // 
            this.btnKayipBiletExcel.Location = new System.Drawing.Point(22, 48);
            this.btnKayipBiletExcel.Name = "btnKayipBiletExcel";
            this.btnKayipBiletExcel.Size = new System.Drawing.Size(105, 27);
            this.btnKayipBiletExcel.TabIndex = 3;
            this.btnKayipBiletExcel.Text = "Kayıp Bilet Form";
            this.btnKayipBiletExcel.UseVisualStyleBackColor = true;
            this.btnKayipBiletExcel.Click += new System.EventHandler(this.btnOzelsatisForm_Click);
            // 
            // btnZorunluBilet
            // 
            this.btnZorunluBilet.Location = new System.Drawing.Point(22, 81);
            this.btnZorunluBilet.Name = "btnZorunluBilet";
            this.btnZorunluBilet.Size = new System.Drawing.Size(105, 27);
            this.btnZorunluBilet.TabIndex = 4;
            this.btnZorunluBilet.Text = "Zorunlu Bilet Form";
            this.btnZorunluBilet.UseVisualStyleBackColor = true;
            this.btnZorunluBilet.Click += new System.EventHandler(this.btnZorunluBilet_Click);
            // 
            // btnExUcret
            // 
            this.btnExUcret.Location = new System.Drawing.Point(22, 114);
            this.btnExUcret.Name = "btnExUcret";
            this.btnExUcret.Size = new System.Drawing.Size(105, 27);
            this.btnExUcret.TabIndex = 5;
            this.btnExUcret.Text = "Ekst. Ücret Form";
            this.btnExUcret.UseVisualStyleBackColor = true;
            this.btnExUcret.Click += new System.EventHandler(this.btnExUcret_Click);
            // 
            // btnArBilForm
            // 
            this.btnArBilForm.Location = new System.Drawing.Point(22, 147);
            this.btnArBilForm.Name = "btnArBilForm";
            this.btnArBilForm.Size = new System.Drawing.Size(105, 27);
            this.btnArBilForm.TabIndex = 6;
            this.btnArBilForm.Text = "Arızalı Bilet Form";
            this.btnArBilForm.UseVisualStyleBackColor = true;
            this.btnArBilForm.Click += new System.EventHandler(this.btnArBilForm_Click);
            // 
            // OzelSatisRaporMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(156, 216);
            this.Controls.Add(this.btnArBilForm);
            this.Controls.Add(this.btnExUcret);
            this.Controls.Add(this.btnZorunluBilet);
            this.Controls.Add(this.btnKayipBiletExcel);
            this.Controls.Add(this.dateTimePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OzelSatisRaporMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OzelSatisRaporMenu";
            this.Load += new System.EventHandler(this.OzelSatisRaporMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnKayipBiletExcel;
        private System.Windows.Forms.Button btnZorunluBilet;
        private System.Windows.Forms.Button btnExUcret;
        private System.Windows.Forms.Button btnArBilForm;
    }
}