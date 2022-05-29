
namespace uu_library_app.FormUI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panelForeGround = new System.Windows.Forms.Panel();
            this.loginTextBox1 = new uu_library_app.FormUI.TextBoxHelper.LoginTextBox();
            this.panelForeGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForeGround
            // 
            this.panelForeGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelForeGround.Controls.Add(this.loginTextBox1);
            this.panelForeGround.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.panelForeGround.Location = new System.Drawing.Point(-5, -18);
            this.panelForeGround.Name = "panelForeGround";
            this.panelForeGround.Size = new System.Drawing.Size(581, 709);
            this.panelForeGround.TabIndex = 0;
            // 
            // loginTextBox1
            // 
            this.loginTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.loginTextBox1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.loginTextBox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.loginTextBox1.BorderRadius = 0;
            this.loginTextBox1.BorderSize = 2;
            this.loginTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.loginTextBox1.Location = new System.Drawing.Point(107, 218);
            this.loginTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.loginTextBox1.Multiline = false;
            this.loginTextBox1.Name = "loginTextBox1";
            this.loginTextBox1.Padding = new System.Windows.Forms.Padding(7);
            this.loginTextBox1.PasswordChar = false;
            this.loginTextBox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.loginTextBox1.PlaceholderText = "login textbox hazır";
            this.loginTextBox1.Size = new System.Drawing.Size(261, 40);
            this.loginTextBox1.TabIndex = 0;
            this.loginTextBox1.Texts = "";
            this.loginTextBox1.UnderlinedStyle = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(564, 676);
            this.Controls.Add(this.panelForeGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panelForeGround.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForeGround;
        private TextBoxHelper.LoginTextBox loginTextBox1;
    }
}