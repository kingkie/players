using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadcastPlayer
{
    public partial class UserMasker : UserControl
    {
        public UserMasker()
        {
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
        }

        public void CoverOnForm(Control Form1)
        {
            if (Form1 == null)
                return;

            this.Location = new Point(0, 0);
            this.Size = Form1.ClientSize;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            Form1.Controls.Add(this);
            this.Visible = true;
            this.BringToFront();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(
                new SolidBrush(Color.FromArgb(100, 0, 0, 0)),
                new Rectangle(0, 0, this.Width, this.Height));

            Font font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            String Text = "Processing。。。";
            SizeF size = e.Graphics.MeasureString(Text, font);
            e.Graphics.DrawString(Text, font,Brushes.White,new PointF( (this.Width - size.Width)/2, (this.Height - size.Height) / 2) );

        }
    }
}
