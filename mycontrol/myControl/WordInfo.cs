using System;
using System.Collections;
using System.Drawing;
namespace myControl
{
	public class WordInfo
	{
		public int m_fontsize;
		public float m_runspeed;
		public Color m_charcolor;
		public float m_posx;
		public float m_posy;
		public int m_count;
		public ArrayList m_charList;
		public static int m_max_xpos = 600;
		public static int m_min_count = 10;
		public static int m_max_count = 20;
		public static int m_min_fontsize = 4;
		public static int m_max_fontsize = 20;
		public static float m_min_runspeed = 0.6f;
		public static float m_max_runspeed = 4.2f;
		public static Color m_min_charcolor = Color.FromArgb(0, 255, 0);
		public static Color m_max_charcolor = Color.FromArgb(0, 55, 0);
		public static Random m_random = new Random();
		public WordInfo()
		{
			this.m_count = 1;
			this.m_fontsize = 0;
			this.m_runspeed = 0f;
			this.m_posx = 0f;
			this.m_posy = 0f;
			this.m_charcolor = Color.FromArgb(0, 255, 0);
		}
		private int GetFontSize()
		{
			int num = WordInfo.m_random.Next();
			return num % (WordInfo.m_max_fontsize - WordInfo.m_min_fontsize) + WordInfo.m_min_fontsize;
		}
		private float GetSpeed()
		{
			int num = WordInfo.m_random.Next();
			int num2 = WordInfo.m_random.Next();
			return (float)num % (WordInfo.m_max_runspeed - WordInfo.m_min_runspeed) + WordInfo.m_min_runspeed + (float)(num2 % 10) / 10f;
		}
		private Color GetColor()
		{
			int r = (int)WordInfo.m_min_charcolor.R;
			int g = (int)WordInfo.m_min_charcolor.G;
			int b = (int)WordInfo.m_min_charcolor.B;
			int r2 = (int)WordInfo.m_max_charcolor.R;
			int g2 = (int)WordInfo.m_max_charcolor.G;
			int b2 = (int)WordInfo.m_max_charcolor.B;
			int red = WordInfo.m_random.Next((r > r2) ? r2 : r, (r < r2) ? r2 : r);
			int green = WordInfo.m_random.Next((g > g2) ? g2 : g, (g < g2) ? g2 : g);
			int blue = WordInfo.m_random.Next((b > b2) ? b2 : b, (b < b2) ? b2 : b);
			return Color.FromArgb(red, green, blue);
		}
		private void SetFontAndColor()
		{
			int num = WordInfo.m_random.Next();
			int num2 = num % (WordInfo.m_max_fontsize - WordInfo.m_min_fontsize);
			this.m_fontsize = WordInfo.m_min_fontsize + num2;
			float num3 = (float)num2 / (float)(WordInfo.m_max_fontsize - WordInfo.m_min_fontsize);
			int r = (int)WordInfo.m_min_charcolor.R;
			int g = (int)WordInfo.m_min_charcolor.G;
			int b = (int)WordInfo.m_min_charcolor.B;
			int r2 = (int)WordInfo.m_max_charcolor.R;
			int g2 = (int)WordInfo.m_max_charcolor.G;
			int b2 = (int)WordInfo.m_max_charcolor.B;
			int num4 = (int)((float)Math.Abs(r - r2) * num3);
			int num5 = (int)((float)Math.Abs(g - g2) * num3);
			int num6 = (int)((float)Math.Abs(b - b2) * num3);
			int red = (r > r2) ? (r2 + num4) : (r2 - num4);
			int green = (g > g2) ? (g2 + num5) : (g2 - num5);
			int blue = (b > b2) ? (b2 + num6) : (b2 - num6);
			this.m_charcolor = Color.FromArgb(red, green, blue);
		}
		private float GetXPos()
		{
			int num = WordInfo.m_random.Next(WordInfo.m_max_xpos - 1);
			return (float)num + (float)WordInfo.m_random.Next(10) / 10f;
		}
		private int GetCount()
		{
			return WordInfo.m_random.Next(WordInfo.m_min_count, WordInfo.m_max_count);
		}
		public void SetValue()
		{
			this.m_count = this.GetCount();
			this.SetFontAndColor();
			this.m_runspeed = this.GetSpeed();
			this.m_posy = (float)(this.m_count * this.m_fontsize) * -1f;
			this.m_posx = this.GetXPos();
		}
		public Color GetColor(int index)
		{
			Color result;
			if (this.m_count == 1)
			{
				result = this.m_charcolor;
			}
			else if (index == 0)
			{
				result = this.m_charcolor;
			}
			else
			{
				int num = Math.Abs((int)(this.m_charcolor.R - WordInfo.m_max_charcolor.R)) / this.m_count * index;
				int num2 = Math.Abs((int)(this.m_charcolor.G - WordInfo.m_max_charcolor.G)) / this.m_count * index;
				int num3 = Math.Abs((int)(this.m_charcolor.B - WordInfo.m_max_charcolor.B)) / this.m_count * index;
				int arg_DC_0 = (this.m_charcolor.R > WordInfo.m_max_charcolor.R) ? ((int)this.m_charcolor.R - num) : ((int)this.m_charcolor.R + num);
				int arg_110_0 = (this.m_charcolor.G > WordInfo.m_max_charcolor.G) ? ((int)this.m_charcolor.G - num2) : ((int)this.m_charcolor.G + num2);
				int arg_145_0 = (this.m_charcolor.B > WordInfo.m_max_charcolor.B) ? ((int)this.m_charcolor.B - num3) : ((int)this.m_charcolor.B + num3);
				result = Color.FromArgb(255 - index * 255 / this.m_count, this.m_charcolor);
			}
			return result;
		}
	}
}
