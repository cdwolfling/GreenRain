using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace myControl
{
    //from http://blog.csdn.net/qian_f/article/details/19285075
	public class WordRain : UserControl
	{
		private Timer m_timer;
		private Timer m_newtimer;
		private Graphics m_g;
		private ArrayList m_charlist;
		private ArrayList m_selectlist;
		private WordInfoSorter m_sorter;
		private Random m_random;
		private IContainer components = null;
		[DllImport("User32.dll ", EntryPoint = "SendMessage ")]
		private static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
		public WordRain()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.Selectable, true);
			this.BackColor = Color.Black;
			this.m_charlist = new ArrayList();
			WordInfo wordInfo = new WordInfo();
			wordInfo.SetValue();
			this.m_charlist.Add(wordInfo);
			this.m_sorter = new WordInfoSorter();
			string[] c = new string[]
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9"
			};
			this.m_selectlist = new ArrayList(c);
			this.m_random = new Random();
			this.m_timer = new Timer();
			this.m_newtimer = new Timer();
			this.m_timer.Interval = 10;
			this.m_newtimer.Interval = 50;
			this.m_timer.Enabled = true;
			this.m_newtimer.Enabled = true;
			this.m_timer.Tick += new EventHandler(this.m_timer_Tick);
			this.m_newtimer.Tick += new EventHandler(this.m_newtimer_Tick);
		}
		private string GetStringFromArrayList()
		{
			return (string)this.m_selectlist[this.m_random.Next(this.m_selectlist.Count)];
		}
		private void m_newtimer_Tick(object sender, EventArgs e)
		{
			WordInfo wordInfo = new WordInfo();
			wordInfo.SetValue();
			this.m_charlist.Add(wordInfo);
		}
		private void m_timer_Tick(object sender, EventArgs e)
		{
			base.Invalidate();
			ArrayList arrayList = new ArrayList();
			foreach (WordInfo wordInfo in this.m_charlist)
			{
				wordInfo.m_posy += wordInfo.m_runspeed;
				if (wordInfo.m_posy > (float)(base.Height + wordInfo.m_fontsize * wordInfo.m_count))
				{
					arrayList.Add(wordInfo);
				}
			}
			foreach (WordInfo wordInfo in arrayList)
			{
				this.m_charlist.Remove(wordInfo);
			}
			arrayList.Clear();
		}
		private void WordRain_Paint(object sender, PaintEventArgs e)
		{
			this.m_g = e.Graphics;
			this.m_charlist.Sort(this.m_sorter);
			this.SetXRange(base.Width);
			foreach (WordInfo wordInfo in this.m_charlist)
			{
				using (Font font = new Font("Arial", (float)wordInfo.m_fontsize, GraphicsUnit.Pixel))
				{
					using (SolidBrush solidBrush = new SolidBrush(wordInfo.m_charcolor))
					{
						for (int i = 0; i < wordInfo.m_count; i++)
						{
							solidBrush.Color = wordInfo.GetColor(i);
							this.m_g.DrawString(this.GetStringFromArrayList(), font, solidBrush, wordInfo.m_posx, wordInfo.m_posy - (float)(wordInfo.m_fontsize * i));
						}
					}
				}
			}
		}
		public void SetFontSizeRange(int minsize, int maxsize)
		{
			if (minsize > maxsize)
			{
				throw new ArgumentException("参数minsize必须小于等于参数maxsize");
			}
			if (minsize < 1 || maxsize > 50)
			{
				throw new ArgumentException("参数minsize和maxsize必须在区间[1,50]内");
			}
			WordInfo.m_min_fontsize = minsize;
			WordInfo.m_max_fontsize = maxsize;
		}
		public void SetFontColorRange(Color firstColor, Color lastColor)
		{
			WordInfo.m_min_charcolor = firstColor;
			WordInfo.m_max_charcolor = lastColor;
		}
		public void SetXRange(int value)
		{
			WordInfo.m_max_xpos = value;
		}
		public void SetSpeedRange(float minspeed, float maxspeed)
		{
			if (minspeed > maxspeed)
			{
				throw new ArgumentException("参数minspeed必须小于等于参数maxspeed");
			}
			if ((double)minspeed < 0.1 || (double)maxspeed > 100.0)
			{
				throw new ArgumentException("参数minspeed和maxspeed必须在区间[0.1,100.0]内");
			}
			WordInfo.m_min_runspeed = minspeed;
			WordInfo.m_min_runspeed = maxspeed;
		}
		public void SetCountRange(int mincount, int maxcount)
		{
			if (mincount > maxcount)
			{
				throw new ArgumentException("参数mincount必须小于等于参数maxcount");
			}
			if (mincount < 1 || maxcount > 100)
			{
				throw new ArgumentException("参数mincount和maxcount必须在区间[1,100]内");
			}
			WordInfo.m_min_count = mincount;
			WordInfo.m_max_count = maxcount;
		}
		public void SetArrayValue(ArrayList list)
		{
			if (list.Count == 0)
			{
				throw new ArgumentException("参数list中的元素个数不能为0");
			}
			this.m_selectlist.Clear();
			this.m_selectlist = list;
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "WordRain";
			base.Size = new Size(600, 400);
			base.Paint += new PaintEventHandler(this.WordRain_Paint);
			base.ResumeLayout(false);
		}
	}
}
