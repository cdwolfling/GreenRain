using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GreenRain
{
    public partial class MainForm : Form
    {
        string strAlibaba = "";
        DateTime dtKeyDown = DateTime.Now;
        Boolean m_IsFullScreen = false;//标记是否全屏

        /// <summary>
        ///  全屏 事件
        /// </summary>
        private void Change()
        {
            m_IsFullScreen = !m_IsFullScreen;//点一次全屏，再点还原。  
            this.SuspendLayout();
            if (m_IsFullScreen)//全屏 ,按特定的顺序执行
            {
                SetFormFullScreen(m_IsFullScreen);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.Activate();//
            }
            else//还原，按特定的顺序执行——窗体状态，窗体边框，设置任务栏和工作区域
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                SetFormFullScreen(m_IsFullScreen);
                this.Activate();
            }
            this.ResumeLayout(false);
        }

        /// <summary>  
        /// 设置全屏或这取消全屏  
        /// </summary>  
        /// <param name="fullscreen">true:全屏 false:恢复</param>  
        /// <param name="rectOld">设置的时候，此参数返回原始尺寸，恢复时用此参数设置恢复</param>  
        /// <returns>设置结果</returns>  
        public Boolean SetFormFullScreen(Boolean fullscreen)//, ref Rectangle rectOld
        {
            Rectangle rectOld = Rectangle.Empty;
            Int32 hwnd = 0;
            hwnd = FindWindow("Shell_TrayWnd", null);//获取任务栏的句柄

            if (hwnd == 0) return false;

            if (fullscreen)//全屏
            {
                ShowWindow(hwnd, SW_HIDE);//隐藏任务栏

                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//get屏幕范围
                Rectangle rectFull = Screen.PrimaryScreen.Bounds;//全屏范围
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectFull, SPIF_UPDATEINIFILE);//窗体全屏幕显示
            }
            else//还原 
            {
                ShowWindow(hwnd, SW_SHOW);//显示任务栏
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//窗体还原
            }
            return true;
        }

        #region user32.dll
        public const Int32 SPIF_UPDATEINIFILE = 0x1;
        public const Int32 SPI_SETWORKAREA = 47;
        public const Int32 SPI_GETWORKAREA = 48;
        public const Int32 SW_SHOW = 5;
        public const Int32 SW_HIDE = 0;

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern Int32 ShowWindow(Int32 hwnd, Int32 nCmdShow);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern Int32 FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, ref Rectangle lpvParam, Int32 fuWinIni);
        #endregion


        public MainForm()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;     //设置窗体为无边框样式
            //this.WindowState = FormWindowState.Maximized;    //最大化窗体 
            this.ControlBox = false;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "黑客帝国";
        }

        private void button_text_Click(object sender, EventArgs e)
        {
            string temp = textBox_charlist.Text.Trim();
            if (temp != string.Empty)
            {
                string[] words = temp.Split(',');
                wordRain.SetArrayValue(new System.Collections.ArrayList(words));
            }
        }

        private void button_fontsize_Click(object sender, EventArgs e)
        {
            if (textBox_fontsizemin.Text.Trim() == string.Empty || textBox_fontsizemax.Text.Trim() == string.Empty)
            {
                return;
            }
            int min = int.Parse(textBox_fontsizemin.Text.Trim());
            int max = int.Parse(textBox_fontsizemax.Text.Trim());
            if (min > 50 || min < 1 || max > 50 || min < 1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain.SetFontSizeRange(min, max);
        }

        private void button_count_Click(object sender, EventArgs e)
        {
            if (textBox_countmin.Text.Trim() == string.Empty || textBox_countmin.Text.Trim() == string.Empty)
            {
                return;
            }
            int min = int.Parse(textBox_countmin.Text.Trim());
            int max = int.Parse(textBox_countmax.Text.Trim());
            if (min > 100 || min < 1 || max > 100 || max < 1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain.SetCountRange(min, max);
        }

        private void button_speed_Click(object sender, EventArgs e)
        {
            if (textBox_speedmin.Text.Trim() == string.Empty || textBox_speedmin.Text.Trim() == string.Empty)
            {
                return;
            }
            float min = float.Parse(textBox_speedmin.Text.Trim());
            float max = float.Parse(textBox_speedmax.Text.Trim());
            if (min > 100.0 || min < 0.1 || max > 100.0 || max < 0.1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain.SetSpeedRange(min, max);
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            if (textBox_colorRmin.Text.Trim() == string.Empty || textBox_colorRmax.Text.Trim() == string.Empty
                || textBox_colorGmin.Text.Trim() == string.Empty || textBox_colorGmax.Text.Trim() == string.Empty
                || textBox_colorBmin.Text.Trim() == string.Empty || textBox_colorBmax.Text.Trim() == string.Empty)
            {
                return;
            }
            int rmin = int.Parse(textBox_colorRmin.Text.Trim());
            int rmax = int.Parse(textBox_colorRmax.Text.Trim());
            int gmin = int.Parse(textBox_colorGmin.Text.Trim());
            int gmax = int.Parse(textBox_colorGmax.Text.Trim());
            int bmin = int.Parse(textBox_colorBmin.Text.Trim());
            int bmax = int.Parse(textBox_colorBmax.Text.Trim());
            wordRain.SetFontColorRange(Color.FromArgb(rmin, gmin, bmin), Color.FromArgb(rmax, gmax, bmax));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartShow();
        }

        private void StartShow()
        {
            Change();
            groupBoxSetting.Visible = false;
            wordRain.Width = 4096;
            wordRain.Height = 2160;
            wordRain.Left = 0;
            wordRain.Top = 0;
        }

        private void Resume()
        {
            Change();
            groupBoxSetting.Visible = true;
            wordRain.Width = 100;
            wordRain.Height = 100;
            wordRain.Left = 0;
            wordRain.Top = 0;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.B || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (dtKeyDown.AddMilliseconds(500) >= DateTime.Now)
                    strAlibaba += e.KeyCode.ToString();
                else
                    strAlibaba = e.KeyCode.ToString();
                DebugShow();
                if (strAlibaba == "UpUpDownDownLeftRightLeftRightBABA")
                {
                    Resume();
                }
                if (strAlibaba == "BABA")
                {
                    Resume();
                }
                dtKeyDown = DateTime.Now;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if( groupBoxSetting.Visible == true)
                    this.Dispose();
                else
                    Resume();
            }
            else if (e.KeyCode == Keys.F1)
            {
                StartShow();
            }
        }

        [Conditional("DEBUG")]
        private void DebugShow()
        {
            lblShowDebugInfo.Text = this.strAlibaba;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
