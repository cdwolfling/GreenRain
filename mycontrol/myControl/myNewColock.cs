using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace myControl
{
	public class myNewColock : UserControl
	{
		private Timer myTimer;
		private Graphics g;
		private Pen pen;
		private int width;
		private int height;
		private Color hourColor = Color.Red;
		private Color minuteColor = Color.Green;
		private Color secondColor = Color.Blue;
		private Color bigScaleColor = Color.DarkGreen;
		private Color litterScaleColor = Color.Olive;
		private Color textColor = Color.White;
		private Color bigBackColor = Color.Black;
		private Color litterBackColor = Color.White;
		private IContainer components = null;
		[Category("颜色"), Description("时钟颜色")]
		public Color HourColor
		{
			get
			{
				return this.hourColor;
			}
			set
			{
				this.hourColor = value;
			}
		}
		[Category("颜色"), Description("时钟颜色")]
		public Color MinuteColor
		{
			get
			{
				return this.minuteColor;
			}
			set
			{
				this.minuteColor = value;
			}
		}
		[Category("颜色"), Description("秒钟颜色")]
		public Color SecondColor
		{
			get
			{
				return this.secondColor;
			}
			set
			{
				this.secondColor = value;
			}
		}
		[Category("颜色"), Description("大刻度颜色")]
		public Color BigScaleColor
		{
			get
			{
				return this.bigScaleColor;
			}
			set
			{
				this.bigScaleColor = value;
			}
		}
		[Category("颜色"), Description("小刻度颜色")]
		public Color LitterScaleColor
		{
			get
			{
				return this.litterScaleColor;
			}
			set
			{
				this.litterScaleColor = value;
			}
		}
		[Category("颜色"), Description("刻度值颜色")]
		public Color TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}
		[Category("颜色"), Description("外圆背景颜色")]
		public Color BigBackColor
		{
			get
			{
				return this.bigBackColor;
			}
			set
			{
				this.bigBackColor = value;
			}
		}
		[Category("颜色"), Description("内圆颜色")]
		public Color LitterBackColor
		{
			get
			{
				return this.litterBackColor;
			}
			set
			{
				this.litterBackColor = value;
			}
		}
		public myNewColock()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.Selectable, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.myTimer = new Timer();
			this.myTimer.Interval = 1000;
			this.myTimer.Enabled = true;
			this.myTimer.Tick += new EventHandler(this.myTimer_Tick);
		}
		private void myTimer_Tick(object sender, EventArgs e)
		{
			base.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.g = e.Graphics;
			this.g.SmoothingMode = SmoothingMode.AntiAlias;
			this.g.SmoothingMode = SmoothingMode.HighQuality;
			this.width = base.Width;
			this.height = base.Height;
			int num = 0;
			int num2 = 0;
			this.g.FillEllipse(new SolidBrush(this.bigBackColor), num + 2, num2 + 2, this.width - 4, this.height - 4);
			this.pen = new Pen(new SolidBrush(this.litterBackColor), 2f);
			this.g.DrawEllipse(this.pen, num + 7, num2 + 7, this.width - 13, this.height - 13);
			this.g.TranslateTransform((float)(num + this.width / 2), (float)(num2 + this.height / 2));
			this.g.FillEllipse(Brushes.White, -5, -5, 10, 10);
			for (int i = 0; i < 60; i++)
			{
				this.g.FillRectangle(new SolidBrush(this.litterScaleColor), new Rectangle(-2, (int)((Convert.ToInt16(this.height - 8) / 2 - 2) * -1), 3, 10));
				this.g.RotateTransform(6f);
			}
			for (int j = 12; j > 0; j--)
			{
				string text = j.ToString();
				this.g.FillRectangle(new SolidBrush(this.bigScaleColor), new Rectangle(-3, (int)((Convert.ToInt16(this.height - 8) / 2 - 2) * -1), 6, 20));
				this.g.DrawString(text, new Font(new FontFamily("Times New Roman"), 14f, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(this.textColor), new PointF((float)(text.Length * -6), (float)((this.height - 8) / -2 + 26)));
				this.g.RotateTransform(-30f);
			}
			int second = DateTime.Now.Second;
			int minute = DateTime.Now.Minute;
			int hour = DateTime.Now.Hour;
			this.pen = new Pen(this.secondColor, 1f);
			this.pen.EndCap = LineCap.ArrowAnchor;
			this.g.RotateTransform((float)(6 * second));
			float y = (float)(-1.0 * ((double)this.height / 2.75));
			this.g.DrawLine(this.pen, new PointF(0f, 0f), new PointF(0f, y));
			this.pen = new Pen(this.minuteColor, 4f);
			this.pen.EndCap = LineCap.ArrowAnchor;
			this.g.RotateTransform((float)(-6 * second));
			this.g.RotateTransform((float)((double)second * 0.1 + (double)(minute * 6)));
			y = (float)(-1.0 * ((double)(this.height - 30) / 2.75));
			this.g.DrawLine(this.pen, new PointF(0f, 0f), new PointF(0f, y));
			this.pen = new Pen(this.hourColor, 6f);
			this.pen.EndCap = LineCap.ArrowAnchor;
			this.g.RotateTransform((float)((double)(-(double)second) * 0.1 - (double)(minute * 6)));
			this.g.RotateTransform((float)((double)second * 0.01 + (double)minute * 0.1 + (double)(hour * 30)));
			y = (float)(-1.0 * ((double)(this.height - 45) / 2.75));
			this.g.DrawLine(this.pen, new PointF(0f, 0f), new PointF(0f, y));
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "myNewColock";
			base.Size = new Size(279, 278);
			base.ResumeLayout(false);
		}
	}
}
