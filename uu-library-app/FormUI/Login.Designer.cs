
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
            this.wehTextBox2 = new uu_library_app.FormUI.TextBoxHelper.WehTextBox();
            this.panelForeGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForeGround
            // 
            this.panelForeGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panelForeGround.Controls.Add(this.wehTextBox2);
            this.panelForeGround.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.panelForeGround.Location = new System.Drawing.Point(-5, -18);
            this.panelForeGround.Name = "panelForeGround";
            this.panelForeGround.Size = new System.Drawing.Size(581, 709);
            this.panelForeGround.TabIndex = 0;
            // 
            // wehTextBox2
            // 
            this.wehTextBox2.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.wehTextBox2.BorderFocusColor = System.Drawing.Color.Thistle;
            this.wehTextBox2.BorderRadius = 10;
            this.wehTextBox2.BorderSize = 2;
            this.wehTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.wehTextBox2.Location = new System.Drawing.Point(111, 225);
            this.wehTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.wehTextBox2.Multiline = false;
            this.wehTextBox2.Name = "wehTextBox2";
            this.wehTextBox2.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.wehTextBox2.PasswordChar = false;
            this.wehTextBox2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.wehTextBox2.PlaceholderText = "Email";
            this.wehTextBox2.Size = new System.Drawing.Size(313, 46);
            this.wehTextBox2.TabIndex = 1;
            this.wehTextBox2.Texts = "";
            this.wehTextBox2.UnderlinedStyle = false;
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
        private TextBoxHelper.WehTextBox wehTextBox2;
    }
}