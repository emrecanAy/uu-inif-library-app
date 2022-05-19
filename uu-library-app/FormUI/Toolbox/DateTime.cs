using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace uu_library_app.FormUI.Toolbox
{
    public class DateTime : DateTimePicker
    {

        //Özellikleri
        private Color skinColor = Color.MediumSlateBlue;
        private Color textColor = Color.White;
        private Color borderColor = Color.PaleVioletRed;
        private int borderSize = 0;


        //Diğer Değerler

        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.calendawhitepng;
        private RectangleF iconButtonArea;
        private const int calendarIconWidth = 34;
        private const int arrowIconWidth = 17;


        //Properties'leri
        public Color SkinColor
        {
            get {
                return skinColor; 
            }
            set
            {
                skinColor = value;
                if (skinColor.GetBrightness() >= 1F)
                    calendarIcon = Properties.Resources.calendawhitepng;
                else calendarIcon = Properties.Resources.calendawhitepng;
                this.Invalidate();
            }
        } 
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        //Constructor
        public DateTime()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(0, 35);
            this.Font = new Font(this.Font.Name, 9.5F);
        }

        //Overriden Methods
        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            droppedDown = true;
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
            base.OnCloseUp(eventargs);
            droppedDown = false;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Graphics graphics =this.CreateGraphics())
            using (Pen penBorder = new Pen(borderColor,borderSize))
            using (SolidBrush skinBrush=new SolidBrush(skinColor))
            using (SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(50,64,64,64)))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            using (StringFormat textFormat =new StringFormat())
            {
                RectangleF clientArea = new RectangleF(0, 0, this.Width - 0.5F, this.Height - 0.5F);
                RectangleF iconArea = new RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);
                penBorder.Alignment = PenAlignment.Inset;
                textFormat.LineAlignment = StringAlignment.Center;

                //Draw Surface
                graphics.FillRectangle(skinBrush, clientArea);
                //Text
                graphics.DrawString("   " + this.Text, this.Font, textBrush, clientArea, textFormat);
                //Draw Open calendar icon highlight
                if (droppedDown == true) graphics.FillRectangle(openIconBrush, iconArea);
                //drawborder
                if (borderSize >= 1) graphics.DrawRectangle(penBorder,clientArea.X,clientArea.Y,clientArea.Width,clientArea.Height);
                //draw icon
                graphics.DrawImage(calendarIcon, this.Width - calendarIcon.Width - 9, (this.Height - calendarIcon.Height) / 2);

            }

        }




    }
}
