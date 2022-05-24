
namespace uu_library_app
{
    partial class student_listing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(student_listing));
            this.label1 = new System.Windows.Forms.Label();
            this.picboxBack = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbAranacakAlan = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picboxBack)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(98, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 40);
            this.label1.TabIndex = 89;
            this.label1.Text = "Öğrenci Listeleme";
            // 
            // picboxBack
            // 
            this.picboxBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.picboxBack.Image = ((System.Drawing.Image)(resources.GetObject("picboxBack.Image")));
            this.picboxBack.Location = new System.Drawing.Point(12, 12);
            this.picboxBack.Name = "picboxBack";
            this.picboxBack.Size = new System.Drawing.Size(80, 80);
            this.picboxBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picboxBack.TabIndex = 90;
            this.picboxBack.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtAra);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbAranacakAlan);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 640);
            this.panel1.TabIndex = 91;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.Location = new System.Drawing.Point(3, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(983, 583);
            this.dataGridView1.TabIndex = 83;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.panel2.Location = new System.Drawing.Point(-1, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(995, 1);
            this.panel2.TabIndex = 0;
            // 
            // cmbAranacakAlan
            // 
            this.cmbAranacakAlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.cmbAranacakAlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAranacakAlan.Font = new System.Drawing.Font("Nirmala UI", 13F);
            this.cmbAranacakAlan.ForeColor = System.Drawing.Color.White;
            this.cmbAranacakAlan.FormattingEnabled = true;
            this.cmbAranacakAlan.Location = new System.Drawing.Point(141, 6);
            this.cmbAranacakAlan.Name = "cmbAranacakAlan";
            this.cmbAranacakAlan.Size = new System.Drawing.Size(344, 31);
            this.cmbAranacakAlan.TabIndex = 243;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 13F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 21);
            this.label3.TabIndex = 242;
            this.label3.Text = "Aranacak Alan:";
            // 
            // txtAra
            // 
            this.txtAra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtAra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAra.Font = new System.Drawing.Font("Nirmala UI", 15F);
            this.txtAra.ForeColor = System.Drawing.Color.White;
            this.txtAra.Location = new System.Drawing.Point(543, 9);
            this.txtAra.Name = "txtAra";
            this.txtAra.Size = new System.Drawing.Size(430, 27);
            this.txtAra.TabIndex = 272;
            this.txtAra.TextChanged += new System.EventHandler(this.txtAra_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 13F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(497, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 21);
            this.label2.TabIndex = 271;
            this.label2.Text = "Ara:";
            // 
            // student_listing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1015, 750);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picboxBack);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "student_listing";
            this.Text = "student_listing";
            this.Load += new System.EventHandler(this.student_listing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picboxBack)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picboxBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbAranacakAlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Label label2;
    }
}