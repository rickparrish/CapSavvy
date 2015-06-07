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
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using Microsoft.Win32;
using CapSavvy.Modules;
using CapSavvy.Data;

// Notes
//
// Changelog (v4.3):
// - Support for TekSavvy's MyAccount portal usage checker via API key
// - Rewrote Start.ca usage module to be more robust
//
// Todo:
// - Survive explorer crash
// - Decouple main program functionality from UI (move a lot of this code to another class)

namespace CapSavvy
{
    public partial class CapSavvy : Form
    {
        string appName = "CapSavvy v4.3";

        private UsageData usage = new UsageData();
        private mod_base usageInfo;
        private SysTrayGraphics sysTray;
        private BackgroundWorker updateWorker = new BackgroundWorker();

        private mod_base[] loadedModules;

        public CapSavvy()
        {
            // Override culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ca");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-ca");

            InitializeComponent();

            updateWorker.DoWork += new DoWorkEventHandler(UpdateWork);
            updateWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdateCompleted);

            sysTray = new SysTrayGraphics(sysTrayIcon);

            // Define the modules, order counts! If one does't match it falls through to the next, teksavvy is the catchall.
            loadedModules = new mod_base[] {
                new mod_ebox_res_dsl(usage),
                new mod_ebox_bus_dsl(usage),
                new mod_videotron_tpia(usage),
                new mod_caneris_dsl(usage),
                new mod_start_fdsl(usage),
                new mod_start_fcable(usage),
                new mod_start_fwireless(usage),
                new mod_start_wdsl(usage),
                new mod_start_wcable(usage),
                new mod_start_wwireless(usage),
                new mod_teksavvy(usage)
            };

            usageInfo = new mod_default(usage);

            Timer_Update.Interval = usageInfo.interval * 1000;
            Timer_Update.Enabled = true;

            this.Text = appName;

            // Get text colour value from registry
            int? colour = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("colour") as int?;

            if (colour.HasValue)
            {
                sysTray.foreColour = Color.FromArgb(colour.Value);
                Text_Colour.ForeColor = sysTray.foreColour;
            }
            else
            {
                sysTray.foreColour = Color.White;
                Text_Colour.ForeColor = Color.White;
            }

            // Get back colour value from registry
            colour = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("backColour") as int?;

            if (colour.HasValue)
            {
                sysTray.backColour = Color.FromArgb(colour.Value);
                Text_Colour.BackColor = sysTray.backColour;
            }
            else
            {
                sysTray.backColour = Color.Black;
                Text_Colour.BackColor = Color.Black;
            }

            // Get the transparent value
            int? transparent = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("transparent") as int?;

            if (transparent.HasValue)
            {
                Checkbox_Transparent.Checked = Convert.ToBoolean(transparent);
                sysTray.transparent = Convert.ToBoolean(transparent);
                Checkbox_Transparent_CheckedChanged(null, null);
            }
            else
            {
                Checkbox_Transparent.Checked = true;
                sysTray.transparent = true;
                Checkbox_Transparent_CheckedChanged(null, null);
            }

            UpdateDisplay(); // Trigger the startup display

            // Get view from registry
            int? view = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("view") as int?;

            switch (view)
            {
                case 0:
                    RadioButton_TotalPredicted.Checked = true;
                    break;
                case 1:
                    RadioButton_UpDown.Checked = true;
                    break;
                case 2:
                    RadioButton_Total.Checked = true;
                    break;
                case 3:
                    RadioButton_DownUp.Checked = true;
                    break;
                case 4:
                    RadioButton_DownPredicted.Checked = true;
                    break;
                case 5:
                    RadioButton_Down.Checked = true;
                    break;
                default:
                    RadioButton_TotalPredicted.Checked = true;
                    break;
            }

            // Get viewUsage (total/peak) from registry
            int? viewUsage = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("viewUsage") as int?;

            if (viewUsage.HasValue)
            {
                Checkbox_Peak.Checked = Convert.ToBoolean(viewUsage);
            }
            else
            {
                Checkbox_Peak.Checked = false;
            }

            // Get username from registry
            string username = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").GetValue("username") as string;

            if (!string.IsNullOrEmpty(username))
            {
                Textbox_Username.Text = username.ToString();

                if (!GetUpdate()) this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Button_Check_Click(object sender, EventArgs e)
        {
            GetUpdate();
        }

        private bool ValidateUsername(bool showDialog)
        {
            foreach (mod_base usageModule in loadedModules)
            {
                Match match = Regex.Match(Textbox_Username.Text, usageModule.validUsernameRegex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("username", Textbox_Username.Text, RegistryValueKind.String);
                    usageInfo = usageModule;
                    Timer_Update.Interval = usageInfo.interval * 1000;
                    usageInfo.Username = Textbox_Username.Text;
                    Label_format.Text = usageModule.moduleName;
                    return true;
                }
            }

            if (showDialog)
                MessageBox.Show("Invalid username", appName);
            usageInfo = new mod_default(usage);
            Timer_Update.Interval = usageInfo.interval * 1000;
            Label_format.Text = usageInfo.moduleName;
            return false;
        }

        private bool GetUpdate()
        {
            if (ValidateUsername(false))
            {
                GC.Collect();

                Button_Check.Enabled = false;
                Checkbox_Peak.Visible = usageInfo.supportsOffPeak;
                contextMenuSysTray.Items["updateNowToolStripMenuItem"].Enabled = false;
                updateWorker.RunWorkerAsync();

                return true;
            }
            else
            {
                UpdateDisplay();
                return false;
            }
        }

        private void UpdateWork(object sender, EventArgs e)
        {
            usageInfo.GetUsage();
        }

        private void UpdateCompleted(object sender, EventArgs e)
        {
            UpdateDisplay();
            UpdateStats();
            Button_Check.Enabled = true;
            contextMenuSysTray.Items["updateNowToolStripMenuItem"].Enabled = true;
        }

        private void UpdateDisplay()
        {
            double first = 0, second = 0;

            if (usageInfo.Error == mod_base.ErrorState.OK)
            {
                if (Checkbox_Peak.Checked)
                {
                    if (RadioButton_TotalPredicted.Checked)
                    {
                        first = usage.Peak.Total;
                        second = usage.Peak.TotalPredicted;
                    }
                    else if (RadioButton_UpDown.Checked)
                    {
                        first = usage.Peak.Up;
                        second = usage.Peak.Down;
                    }
                    else if (RadioButton_Total.Checked)
                    {
                        first = usage.Peak.Total;
                        second = -1;
                    }
                    else if (RadioButton_DownUp.Checked)
                    {
                        first = usage.Peak.Down;
                        second = usage.Peak.Up;
                    }
                    else if (RadioButton_DownPredicted.Checked)
                    {
                        first = usage.Peak.Down;
                        second = usage.Peak.DownPredicted;
                    }
                    else if (RadioButton_Down.Checked)
                    {
                        first = usage.Peak.Down;
                        second = -1;
                    }
                }
                else
                {
                    if (RadioButton_TotalPredicted.Checked)
                    {
                        first = usage.All.Total;
                        second = usage.All.TotalPredicted;
                    }
                    else if (RadioButton_UpDown.Checked)
                    {
                        first = usage.All.Up;
                        second = usage.All.Down;
                    }
                    else if (RadioButton_Total.Checked)
                    {
                        first = usage.All.Total;
                        second = -1;
                    }
                    else if (RadioButton_DownUp.Checked)
                    {
                        first = usage.All.Down;
                        second = usage.All.Up;
                    }
                    else if (RadioButton_DownPredicted.Checked)
                    {
                        first = usage.All.Down;
                        second = usage.All.DownPredicted;
                    }
                    else if (RadioButton_Down.Checked)
                    {
                        first = usage.All.Down;
                        second = -1;
                    }
                }
            }

            switch (usageInfo.Error)
            {
                case mod_base.ErrorState.OK:
                    if (Checkbox_Peak.Checked)
                    {
                        if (RadioButton_Total.Checked || RadioButton_TotalPredicted.Checked)
                            sysTrayIcon.Text = "Up: " + RoundUp(usage.Peak.Up, 2).ToString() + " GB\nDown: " + RoundUp(usage.Peak.Down, 2).ToString() + " GB";
                        else
                            sysTrayIcon.Text = "Total: " + RoundUp(usage.Peak.Total, 2).ToString() + " GB\nPredicted: " + RoundUp(usage.Peak.TotalPredicted, 2).ToString() + " GB";
                    }
                    else
                    {
                        if (RadioButton_Total.Checked || RadioButton_TotalPredicted.Checked)
                            sysTrayIcon.Text = "Up: " + RoundUp(usage.All.Up, 2).ToString() + " GB\nDown: " + RoundUp(usage.All.Down, 2).ToString() + " GB";
                        else
                            sysTrayIcon.Text = "Total: " + RoundUp(usage.All.Total, 2).ToString() + " GB\nPredicted: " + RoundUp(usage.All.TotalPredicted, 2).ToString() + " GB";
                    }

                    sysTray.UpdateTray(first, second);

                    break;
                case mod_base.ErrorState.Startup:
                    sysTrayIcon.Text = appName;
                    sysTray.UpdateStartupTray();
                    break;
                default:
                    sysTrayIcon.Text = "Error getting usage.";
                    sysTray.UpdateErrorTray();
                    break;
            }
        }

        string FormatGigs(double gigs)
        {
            return RoundUp(gigs, 2).ToString() + " GB";
        }

        double RoundUp(double num, int precision)
        {
            num *= Math.Pow(10, precision);
            num = Math.Ceiling(num);
            num /= Math.Pow(10, precision);
            return num;
        }

        private void UpdateStats()
        { 
            DataGrid_Stats.Rows.Clear();
            switch (usageInfo.Error)
            {
                case mod_base.ErrorState.OK:
                    DataGrid_Stats.Rows.Add(new object[] { "Usage", "Down", "Up", "Combined", "Predicted", "Down Predicted" });
                    DataGrid_Stats.Rows.Add(new object[] { "Total:", FormatGigs(usage.All.Down), FormatGigs(usage.All.Up), FormatGigs(usage.All.Total), FormatGigs(usage.All.TotalPredicted), FormatGigs(usage.All.DownPredicted) });
                    if (usageInfo.supportsOffPeak)
                    {
                        DataGrid_Stats.Rows.Add(new object[] { "Off Peak:", FormatGigs(usage.OffPeak.Down), FormatGigs(usage.OffPeak.Up), FormatGigs(usage.OffPeak.Total), FormatGigs(usage.OffPeak.TotalPredicted), FormatGigs(usage.OffPeak.DownPredicted) });
                        DataGrid_Stats.Rows.Add(new object[] { "Peak:", FormatGigs(usage.Peak.Down), FormatGigs(usage.Peak.Up), FormatGigs(usage.Peak.Total), FormatGigs(usage.Peak.TotalPredicted), FormatGigs(usage.Peak.DownPredicted) });
                    }
                    break;
                case mod_base.ErrorState.HTTP:
                    DataGrid_Stats.Rows.Add(new object[] { "Error getting", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage data:", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "Unable to get", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage page", "" });
                    break;
                case mod_base.ErrorState.Match:
                    DataGrid_Stats.Rows.Add(new object[] { "Error getting", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage data:", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "Unable to parse", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage page", "" });
                    break;
                case mod_base.ErrorState.Username:
                    DataGrid_Stats.Rows.Add(new object[] { "Error getting", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage data:", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "Invalid username", "" });
                    break;
                case mod_base.ErrorState.Startup:
                    break;
                default:
                    DataGrid_Stats.Rows.Add(new object[] { "Error getting", "" });
                    DataGrid_Stats.Rows.Add(new object[] { "usage data", "" });
                    break;
            }
        }

        private void ShowMainWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void sysTrayIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void Button_Colour_Click(object sender, EventArgs e)
        {
            colorDialog.Color = sysTray.foreColour;
            colorDialog.ShowDialog();
            Text_Colour.ForeColor = colorDialog.Color;
            sysTray.foreColour = colorDialog.Color;
            SetBackgroundColour(sysTray.backColour);
            Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("colour", sysTray.foreColour.ToArgb(), RegistryValueKind.DWord);
            UpdateDisplay();
        }

        private void Button_Background_Click(object sender, EventArgs e)
        {
            colorDialog.Color = sysTray.backColour;
            colorDialog.ShowDialog();
            sysTray.backColour = colorDialog.Color;
            SetBackgroundColour(sysTray.backColour);
            Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("backColour", sysTray.backColour.ToArgb(), RegistryValueKind.DWord);
            UpdateDisplay();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sysTrayIcon.Dispose();
        }

        private void Timer_Update_Tick(object sender, EventArgs e)
        {
            GetUpdate();
        }

        private void Textbox_Username_Leave(object sender, EventArgs e)
        {
            ValidateUsername(true);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetUpdate();
        }

        private void settingsInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(appName + "\r\nCopyright 2014 by Adam Zey, Darryl Tam\r\n\nCapSavvy is a bandwidth monitoring tool for customers of TekSavvy Solutions Inc.'s DSL services. It allows customers to keep better track of their monthly bandwidth usage for the purpose of planning their usage around their monthly cap.\r\n\r\nUpdated versions of this application can be found at http://dslreports.com/forum/teksavvy\r\n\r\nNo bananas, timbits, or Linux ISOs were consumed during the making of this application.\r\n\r\nThis program is covered by the GPL v2.0 license. A copy is included with the source.", appName);
        }

        private void RadioButton_TotalPredicted_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TotalPredicted.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 0, RegistryValueKind.DWord);
            }
        }

        private void RadioButton_UpDown_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_UpDown.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 1, RegistryValueKind.DWord);
            }
        }

        private void RadioButton_Total_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Total.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 2, RegistryValueKind.DWord);
            }
        }

        private void RadioButton_DownUp_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_DownUp.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 3, RegistryValueKind.DWord);
            }
        }

        private void RadioButton_DownPredicted_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_DownPredicted.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 4, RegistryValueKind.DWord);
            }
        }

        private void RadioButton_Down_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Down.Checked)
            {
                UpdateDisplay();
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("view", 5, RegistryValueKind.DWord);
            }
        }

        private void Checkbox_Transparent_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_Transparent.Checked)
            {
                Button_Background.Enabled = false;
                SetBackgroundColour(Color.Transparent);
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("transparent", 1, RegistryValueKind.DWord);
                sysTray.transparent = true;
            }
            else
            {
                Button_Background.Enabled = true;
                SetBackgroundColour(sysTray.backColour);
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("transparent", 0, RegistryValueKind.DWord);
                sysTray.transparent = false;
            }

            UpdateDisplay();
        }

        private void SetBackgroundColour(Color color)
        {
            if (Checkbox_Transparent.Checked)
            {
                if (Convert.ToInt32(sysTray.foreColour.R) + Convert.ToInt32(sysTray.foreColour.G) + Convert.ToInt32(sysTray.foreColour.B) > 382)
                    Text_Colour.BackColor = Color.Black;
                else
                    Text_Colour.BackColor = Color.White;
            }
            else
            {
                Text_Colour.BackColor = color;
            }
        }

        private void Checkbox_Peak_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();

            if (Checkbox_Peak.Checked)
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("viewUsage", 1, RegistryValueKind.DWord);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("SOFTWARE\\CapSavvy").SetValue("viewUsage", 0, RegistryValueKind.DWord);
            }
        }
    }
}