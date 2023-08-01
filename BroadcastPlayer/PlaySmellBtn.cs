using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CinemaSetter.ToolKit;

namespace BroadcastPlayer
{
    public partial class PlaySmellBtn : UserControl
    {
        public int _SmellID = 1;
        

        public event EventHandler ButtonClick;

        public event EventHandler StopButtonClick;

        public int SmellID {
            get {
                return _SmellID;
            }
            set {
                _SmellID = value;
                //label1.Text = _SmellID + "";
            }
        }

        public string SmellName
        {
            get
            {
                return label1.Text;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    label1.Text = SmellID + "";
                }
                else
                {
                    label1.Text = SmellID + ":" + value;
                }
            }
        }

        public static List<PlaySmellBtn> PSBList = new List<PlaySmellBtn>();

        public PlaySmellBtn()
        {
            InitializeComponent();
            UISync.Init(this);
            DrawPlayBtn();

            PSBList.Add(this);
        }

        private Boolean IsPlaying = false;
        private Thread PlaySmellThread = null;

        public void PerformencePlaySmell(int Duration)
        {

            if (IsPlaying)
                return;

            foreach (PlaySmellBtn psb in PSBList)
            {
                psb.StopPlayEffect();
            }

            DrawPlayBtn(true);
            coutdown_label2.Visible = true;
            coutdown_label2.Text = "Rest " + (Duration) + " S";
            IsPlaying = true;
            PlaySmellThread = new Thread(() => {
                int Countor = 0;
                while (Countor < Duration)
                {
                    Thread.Sleep(1000);
                    Countor++;
                    UISync.Execute(() => {
                        coutdown_label2.Text = "Rest " + (Duration - Countor) + " S";
                    });
                }
              
                UISync.Execute(() => {
                    coutdown_label2.Visible = false;
                    DrawPlayBtn();
                    IsPlaying = false;
                });
            });

            PlaySmellThread.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }

        public void StopPlayEffect()
        {
            if(null != PlaySmellThread && PlaySmellThread.IsAlive)
                 PlaySmellThread.Abort();
            coutdown_label2.Visible = false;
            DrawPlayBtn();
            IsPlaying = false;
        }

        private void PlaySmellBtn_Click(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                StopPlayEffect();
                if (null != StopButtonClick)
                    StopButtonClick.Invoke(this,e);
            }
            else
            {
                if (null != ButtonClick)
                    ButtonClick.Invoke(this,e);
            }
        }

        private void DrawPlayBtn(Boolean IsPlaying = false)
        {
            PictureBox pb1 = pictureBox1;

            Bitmap bmp = new Bitmap(pb1.Width,pb1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Pen p = new Pen(Color.Gray, 1);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawEllipse(p, 0, 0, pb1.Width - 1, pb1.Height - 1);

            if (IsPlaying)
            {
                int Radius = 7;
                int offset = 2;
                float StartX = (float)((pb1.Width - Radius * 2) / 2.0);
                Rectangle leftBox = new Rectangle((Int32)StartX, (Int32)StartX, Radius - offset, Radius * 2);

                g.FillRectangle(Brushes.Gray, leftBox);

                Rectangle rightBox = new Rectangle((Int32)StartX + Radius + offset, (Int32)StartX, Radius - offset, Radius * 2);

                g.FillRectangle(Brushes.Gray, rightBox);
            }
            else
            {
                double r = pb1.Width / 2;
                double a = 16;
                double b = Math.Pow(3, 0.5);
                PointF[] TrianglePoints = new PointF[3] {
                    new PointF( (float)(r - a / (2*b)) ,(float)(r - a / 2)),
                    new PointF( (float)(r + (b/2 -  1/(2*b) )*a ) ,(float)r),
                    new PointF( (float)(r - a / (2*b)) ,(float)(r + a / 2)),
                };
                g.FillPolygon(Brushes.Gray, TrianglePoints);
            }
            pb1.BackgroundImage = bmp;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            

        }

    }
}
