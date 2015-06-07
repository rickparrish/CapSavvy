/*

CapSavvy, a program to track bandwidth usage for customers of TekSavvy and their resellers
Copyright (C) 2014 Adam Zey, Darryl Tam

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

*/

using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Text;

namespace CapSavvy
{
    class SysTrayGraphics
    {
        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        static extern bool DestroyIcon(IntPtr hIcon);

        private System.Windows.Forms.NotifyIcon sysTrayIcon;

        private Bitmap bitmap = new Bitmap(16, 16);
        private SolidBrush brush;
        private Pen pen;
        private Font font = new Font(FontFamily.GenericSansSerif, 7);
        private Graphics graphics;

        public Color backColour;
        public Color foreColour;
        public bool transparent = true;

        public SysTrayGraphics(System.Windows.Forms.NotifyIcon sysTrayIcon)
        {
            this.sysTrayIcon = sysTrayIcon;

            graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            brush = new SolidBrush(foreColour);
            pen = new Pen(foreColour, 1);
        }

        private void InitIcon()
        {
            brush.Color = foreColour;
            pen.Color = foreColour;
            graphics.Clear(transparent ? Color.Transparent : backColour);
        }

        public void UpdateStartupTray()
        {
            InitIcon();

            /*graphics.DrawString("T", font, brush, -2, -3);
            graphics.DrawString("E", font, brush, 3, -3);
            graphics.DrawString("K", font, brush, 8, -3);
            graphics.DrawString("C", font, brush, -3, 6);
            graphics.DrawLine(pen, 6, 10, 6, 18);
            graphics.DrawLine(pen, 9, 10, 9, 18);
            graphics.DrawLine(pen, 7, 9, 8, 9);
            graphics.DrawLine(pen, 7, 12, 8, 12);
            graphics.DrawString("P", font, brush, 8, 6);*/

            graphics.DrawIcon((Icon)(new System.ComponentModel.ComponentResourceManager(typeof(CapSavvy))).GetObject("$this.Icon"), new Rectangle(0, 0, 16, 16));

            SetIcon();
        }

        public void UpdateErrorTray()
        {
            InitIcon();

            int x = -2; int y = -3;

            graphics.DrawString("E", font, brush, x - 1, y);
            graphics.DrawString("R", font, brush, x + 4, y);
            graphics.DrawString("R", font, brush, x + 10, y);
            graphics.DrawString("E", font, brush, x - 1, y + 9);
            graphics.DrawString("R", font, brush, x + 4, y + 9);
            graphics.DrawString("R", font, brush, x + 10, y + 9);

            SetIcon();
        }

        public void SetIcon()
        {
            IntPtr hIcon = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(hIcon);
            sysTrayIcon.Icon = icon;

            // Free icon on Win32, ignore failure on Linux
            try {DestroyIcon(hIcon);} catch {}
        }

        public void UpdateTray(double first, double second)
        {
            InitIcon();

            if (second < 0)
            {
                DrawNum(0, 5, first);
            }
            else
            {
                DrawNum(0, 0, first);
                DrawNum(0, 9, second);
            }

            SetIcon();
        }

        private void DrawNum(float x, float y, double num)
        {
            x -= 2; y -= 3;

            if (num < 10)
            {
                num = RoundUp(num, 2);
                graphics.DrawString(num.ToString().Substring(0, 1), font, brush, x, y);
                graphics.DrawString(".", font, brush, x + 4, y);
                graphics.DrawString(num.ToString().Length >= 3 ? num.ToString().Substring(2, 1) : "0", font, brush, x + 7, y);
                graphics.DrawString(num.ToString().Length >= 4 ? num.ToString().Substring(3, 1) : "0", font, brush, x + 12, y);
            }
            else if (num < 100)
            {
                num = RoundUp(num, 1);
                graphics.DrawString(num.ToString().Substring(0, 1), font, brush, x, y);
                graphics.DrawString(num.ToString().Length >= 2 ? num.ToString().Substring(1, 1) : "0", font, brush, x + 5, y);
                graphics.DrawString(".", font, brush, x + 9, y);
                graphics.DrawString(num.ToString().Length >= 4 ? num.ToString().Substring(3, 1) : "0", font, brush, x + 12, y);
            }
            else if (num < 1000)
            {
                graphics.DrawString(Math.Floor(num).ToString(), font, brush, x, y);
            }
            else if (num < 10000)
            {
                num = RoundUp(num / 1000, 1);
                graphics.DrawString(num.ToString().Substring(0, 1), font, brush, x, y);
                graphics.DrawString(".", font, brush, x + 4, y);
                graphics.DrawString(num.ToString().Length >= 3 ? num.ToString().Substring(2, 1) : "0", font, brush, x + 7, y);
                graphics.DrawString("k", font, brush, x + 12, y);
            }
            else if (num < 100000)
            {
                num = RoundUp(num / 1000, 0);
                graphics.DrawString(num.ToString().Substring(0, 1), font, brush, x, y);
                graphics.DrawString(num.ToString().Length >= 2 ? num.ToString().Substring(1, 1) : "0", font, brush, x + 5, y);
                graphics.DrawString("k", font, brush, x + 10, y);
            }
            else
            {
                graphics.DrawString("...", font, brush, x, y);
            }
        }

        double RoundUp(double num, int precision)
        {
            num *= Math.Pow(10, precision);
            num = Math.Ceiling(num);
            num /= Math.Pow(10, precision);
            return num;
        }
    }
}
