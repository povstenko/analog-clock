using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
		int cntr;
		int hourCntr;
        Point centr;
        Timer timer;
        Graphics g;

		int h;
		Point secEndPoint;
		Point minEndPoint;
		Point hourEndPoint;

		public Form1()
        {
            InitializeComponent();
			hourCntr = 0;
			cntr = 0;
            centr = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();


			secEndPoint = CalculateSecondPoint(h, 100, centr);
			minEndPoint = CalculateMinutePoint(DateTime.Now.Minute, 100, centr);
			hourEndPoint = CalculateMinutePoint(DateTime.Now.Hour, 80, centr);
		}

        private void Timer_Tick(object sender, EventArgs e)
        {
			h = DateTime.Now.Second;
			secEndPoint = CalculateSecondPoint(h, 100, centr);

			if (cntr == 60)
			{
				minEndPoint = CalculateMinutePoint(DateTime.Now.Minute, 100, centr);
				cntr = 0;
				hourCntr++;
				if (hourCntr == 60)
				{
					hourEndPoint = CalculateMinutePoint(DateTime.Now.Hour, 90, centr);
					hourCntr = 0;
				}
			}
				
			g = Graphics.FromImage(bmp);
			g.Clear(Color.White);
			g.FillEllipse(Brushes.Gray, new Rectangle(new Point(this.ClientSize.Width / 2 - 118, this.ClientSize.Height / 2 - 118), new Size(236, 236)));
			g.FillEllipse(Brushes.LightGray, new Rectangle(new Point(this.ClientSize.Width / 2 - 115, this.ClientSize.Height / 2 - 115), new Size(230, 230)));
			for (int i = 0; i < 60; i++)
			{
				g.DrawLine(Pens.Gray, CalculateSecondPoint(i, 101, centr), CalculateSecondPoint(i, 105, centr));
			}
			for (int i = 0; i < 12; i++)
			{
				g.DrawLine(Pens.Black, CalculateHourPoint(i, 101, centr), CalculateHourPoint(i, 110, centr));
				
			}
			g.DrawLine(Pens.Black, centr, minEndPoint);
			g.DrawLine(Pens.Red, centr, secEndPoint);
			g.DrawLine(Pens.Black, centr, hourEndPoint);
			g.FillEllipse(Brushes.Black, new Rectangle(new Point(this.ClientSize.Width / 2 - 4, this.ClientSize.Height / 2 - 4), new Size(8, 8)));
			pictureBox1.Image = bmp;

			cntr++;
        }

        private Point CalculateSecondPoint(int h, int radius, Point center)
        {
            int x = (int)(center.X + radius * Math.Cos(Math.PI * h / 30));
            int y = (int)(center.Y + radius * Math.Sin(Math.PI * h / 30));
            return new Point(x, y);
        }
		private Point CalculateMinutePoint(int h, int radius, Point center)
		{
			int x = (int)(center.X + radius * Math.Cos(Math.PI * h / 30));
			int y = (int)(center.Y + radius * Math.Sin(Math.PI * h / 30));
			return new Point(x, y);
		}
		private Point CalculateHourPoint(int h, int radius, Point center)
		{
			int x = (int)(center.X + radius * Math.Cos(Math.PI * h / 6));
			int y = (int)(center.Y + radius * Math.Sin(Math.PI * h / 6));
			return new Point(x, y);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bmp.Save("finalClock.bmp");
        }

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
