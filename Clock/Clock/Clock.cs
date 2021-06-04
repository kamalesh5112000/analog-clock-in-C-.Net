using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Clock
{
    public partial class Clock : Form
    {
        private Rectangle rect;
        private LinearGradientBrush lgb;
        private SolidBrush tarczaKolor;
        private SolidBrush liczbyKolor;
        private SolidBrush podpisKolor;
        private Pen cienTarczyKolor;
        private Pen pen;
        private Pen pensec;
        private int srednica = 120;
        public Clock()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            Clockmetion();
            this.Paint += new PaintEventHandler(MainForm_Paint);
            DateTime dt = DateTime.Now;

            textBox1.Text = ("Time:"+dt.Hour+":"+dt.Minute+" Date : "+dt.Day);
        }
        public void Clockmetion()
        {
            rect = new Rectangle(this.ClientSize.Width / 2 - srednica / 2, this.ClientSize.Height / 2 - srednica / 2, srednica, srednica);
            lgb = new LinearGradientBrush(rect, Color.FromArgb(90, 56, 38), Color.FromArgb(55, 52, 42), 60);
            tarczaKolor = new SolidBrush(Color.WhiteSmoke);
            liczbyKolor = new SolidBrush(Color.FromArgb(10, 10, 10));
            podpisKolor = new SolidBrush(Color.Blue);
            cienTarczyKolor = new Pen(Color.FromArgb(180, 180, 180), 3);
            pen = new Pen(Color.FromArgb(10, 45, 180), 4);
            pensec = new Pen(Color.Red, 2);

            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            pensec.EndCap = LineCap.ArrowAnchor;
            pensec.StartCap = LineCap.RoundAnchor;
            cienTarczyKolor.EndCap = LineCap.ArrowAnchor;
            cienTarczyKolor.StartCap = LineCap.RoundAnchor;
        }
        private void MainForm_Load(object sender, EventArgs e)
        { }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            rysuj(e.Graphics);
        }

        public void rysuj(Graphics graphics)
        {
           
            int srWidth;
            int srHeight;
  
            int minuty;
            int godziny;
            double sekundy;
 
            double minutyTic;
            double godzinyTic;
            double sekundyTic;
            float promien = 89;
            int ramka; 
            float stopien = 30.0f; 
            DateTime czas;
    
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;


            srWidth = this.ClientSize.Width / 2;
            srHeight = this.ClientSize.Height / 2;
            ramka = 18;


            graphics.FillEllipse(lgb, (srWidth - (srednica / 2) - (ramka / 2)), (srHeight - (srednica / 2) - (ramka / 2)), srednica + ramka, srednica + ramka);
            graphics.FillEllipse(tarczaKolor, rect);
            graphics.DrawEllipse(cienTarczyKolor, rect);

            graphics.TranslateTransform(srWidth, srHeight);


            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Font textFont = new Font("Arial", 12F, FontStyle.Italic);

            for (int i = 1; i <= 12; i++)
            {
                graphics.DrawString(i.ToString(), textFont, liczbyKolor, (-1 * obliczX(i * stopien + 90, promien)) + 1, -1 * obliczY(i * stopien + 90, promien) + 2, format);
            }
            for (int i = 1; i <= 12; i++)
            {
                graphics.DrawEllipse(new Pen(Color.FromArgb(90, 56, 38), 4), (-1 * obliczX(i * stopien + 90, promien + 12)) - 1, -1 * obliczY(i * stopien + 90, promien + 12) - 1, 2, 2);
            }

            czas = DateTime.Now;
            godziny = czas.Hour;
            minuty = czas.Minute;
            sekundy = czas.Second + (czas.Millisecond * 0.001);


            godzinyTic = 2.0 * Math.PI * (godziny + minuty / 60.0) / 12.0;
            int promienG = 30;
            Point pktSrodek = new Point(0, 0);
            Point pktCienGodzina = new Point((int)((promienG * Math.Sin(godzinyTic)) + 2), (int)((-(promienG) * Math.Cos(godzinyTic)) + 2));
            graphics.DrawLine(cienTarczyKolor, pktSrodek, pktCienGodzina);

            Point pktWskGodzina = new Point((int)(promienG * Math.Sin(godzinyTic)), (int)(-(promienG) * Math.Cos(godzinyTic)));
            graphics.DrawLine(pen, pktSrodek, pktWskGodzina);


            minutyTic = 2.0 * Math.PI * (minuty + sekundy / 60.0) / 60.0;
            int promienM = 57;
            Point pktCienMinuta = new Point((int)(promienM * Math.Sin(minutyTic) + 2), (int)(-(promienM) * Math.Cos(minutyTic) + 2));
            graphics.DrawLine(cienTarczyKolor, pktSrodek, pktCienMinuta);
            Point pktWskMinuta = new Point((int)(promienM * Math.Sin(minutyTic)), (int)(-(promienM) * Math.Cos(minutyTic)));
            graphics.DrawLine(pen, pktSrodek, pktWskMinuta);



            sekundyTic = 2.0 * Math.PI * (sekundy) / 60.0;
            int promienS = 49;
            Point pktCienSekunda = new Point((int)(promienS * Math.Sin(sekundyTic)), (int)(-(promienS) * Math.Cos(sekundyTic)));
            graphics.DrawLine(pensec, pktSrodek, pktCienSekunda);

            Point pktWskSekunda = new Point((int)(promienS * Math.Sin(sekundyTic)), (int)(-(promienS) * Math.Cos(sekundyTic)));
            graphics.DrawLine(pensec, pktSrodek, pktWskSekunda);


            Invalidate();


        }

        private float obliczX(float stopnie, float r)
        {
            return (float)(r * Math.Cos((Math.PI / 180) * stopnie));
        }

        private float obliczY(float stopnie, float r)
        {
            return (float)(r * Math.Sin((Math.PI / 180) * stopnie));
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rect = new Rectangle(this.ClientSize.Width / 2 - srednica / 2, this.ClientSize.Height / 2 - srednica / 2, srednica, srednica);
            lgb = new LinearGradientBrush(rect, Color.FromArgb(90, 56, 38), Color.FromArgb(55, 52, 42), 60);
            tarczaKolor = new SolidBrush(Color.WhiteSmoke);
            liczbyKolor = new SolidBrush(Color.FromArgb(55, 55, 55));
            podpisKolor = new SolidBrush(Color.Blue);
            cienTarczyKolor = new Pen(Color.FromArgb(180, 180, 180), 3);
            pen = new Pen(Color.FromArgb(10, 45, 180), 4);
            pensec = new Pen(Color.Red, 2);

            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            pensec.EndCap = LineCap.ArrowAnchor;
            pensec.StartCap = LineCap.RoundAnchor;
            cienTarczyKolor.EndCap = LineCap.ArrowAnchor;
            cienTarczyKolor.StartCap = LineCap.RoundAnchor;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //this.BackColor = Color.Lime;
            rect = new Rectangle(this.ClientSize.Width / 2 - srednica / 2, this.ClientSize.Height / 2 - srednica / 2, srednica, srednica);
            lgb = new LinearGradientBrush(rect, Color.FromArgb(90, 56, 38), Color.FromArgb(55, 52, 42), 60);
            tarczaKolor = new SolidBrush(Color.WhiteSmoke);
            liczbyKolor = new SolidBrush(Color.FromArgb(240, 20, 240));
            podpisKolor = new SolidBrush(Color.Blue);
            cienTarczyKolor = new Pen(Color.FromArgb(240, 20, 240), 3);
            pen = new Pen(Color.FromArgb(240, 20, 240), 4);
            pensec = new Pen(Color.Lime, 2);

            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            pensec.EndCap = LineCap.ArrowAnchor;
            pensec.StartCap = LineCap.RoundAnchor;
            cienTarczyKolor.EndCap = LineCap.ArrowAnchor;
            cienTarczyKolor.StartCap = LineCap.RoundAnchor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rect = new Rectangle(this.ClientSize.Width / 2 - srednica / 2, this.ClientSize.Height / 2 - srednica / 2, srednica, srednica);
            lgb = new LinearGradientBrush(rect, Color.FromArgb(90, 56, 38), Color.FromArgb(55, 52, 42), 60);
            tarczaKolor = new SolidBrush(Color.WhiteSmoke);
            liczbyKolor = new SolidBrush(Color.FromArgb(240, 20, 20));
            podpisKolor = new SolidBrush(Color.Blue);
            cienTarczyKolor = new Pen(Color.FromArgb(240, 20, 20), 3);
            pen = new Pen(Color.FromArgb(240, 20, 20), 4);
            pensec = new Pen(Color.Blue, 2);

            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            pensec.EndCap = LineCap.ArrowAnchor;
            pensec.StartCap = LineCap.RoundAnchor;
            cienTarczyKolor.EndCap = LineCap.ArrowAnchor;
            cienTarczyKolor.StartCap = LineCap.RoundAnchor;

        }
    }
    
}
